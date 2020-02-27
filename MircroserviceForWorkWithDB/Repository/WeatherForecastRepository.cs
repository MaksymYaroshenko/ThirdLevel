using Microsoft.Extensions.Logging;
using MircroserviceForWorkWithDB.Config;
using MircroserviceForWorkWithDB.Database;
using MircroserviceForWorkWithDB.Database.Entities;
using MircroserviceForWorkWithDB.WeatherDataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;

namespace MircroserviceForWorkWithDB.Repository
{
    public class WeatherForecastRepository
    {
        readonly DatabaseContext db;

        public WeatherForecastRepository()
        {
            db = new DatabaseContext();
        }

        public void GetWeatherData()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole()
                    .AddEventLog();
            });
            ILogger logger = loggerFactory.CreateLogger<WeatherForecastRepository>();
            List<string> cities = new List<string> { "Sumy", "Kyiv", "Dnipro", "Odesa", "Lviv", "Kharkiv" };
            string IDOWeather = Constants.OPEN_WEATHER_APPID;
            RestClient client;
            RestRequest request;
            foreach (var city in cities)
            {
                logger.LogInformation($"Started making request for {city}'s data");
                client = new RestClient($"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&APPID={IDOWeather}");
                request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                    var weatherResponse = content.ToObject<WeatherData>();
                    City viewModel = new City();
                    try
                    {
                        if (weatherResponse != null)
                        {
                            viewModel.Name = weatherResponse.Name;
                            viewModel.Humidity = weatherResponse.Main.Humidity;
                            viewModel.Pressure = weatherResponse.Main.Pressure;
                            viewModel.Temp = weatherResponse.Main.Temp;
                            viewModel.Weather = weatherResponse.Weather[0].Main;
                            viewModel.Wind = weatherResponse.Wind.Speed;
                            db.City.Add(viewModel);
                            db.SaveChanges();
                            logger.LogInformation($"Data for {city} was saved");
                        }
                    }
                    catch
                    {
                        logger.LogError($"Weather for {city} wasn't saved in Database");
                    }
                }
                else
                {
                    logger.LogError($"Couldn't get data for {city}");
                }
            }
        }
    }
}

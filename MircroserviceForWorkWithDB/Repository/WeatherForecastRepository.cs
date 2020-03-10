using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MircroserviceForWorkWithDB.Config;
using MircroserviceForWorkWithDB.Database;
using MircroserviceForWorkWithDB.Database.Entities;
using MircroserviceForWorkWithDB.WeatherDataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly.CircuitBreaker;
using RestSharp;
using System;

namespace MircroserviceForWorkWithDB.Repository
{
    public class WeatherForecastRepository
    {
        private readonly DatabaseContext db;
        private static ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole()
                .AddEventLog();
        });
        private ILogger logger = loggerFactory.CreateLogger<WeatherForecastRepository>();

        public WeatherForecastRepository()
        {
            db = new DatabaseContext();
        }

        public void GetWeatherData()
        {
            var cities = ConfigurationManager.AppSetting.GetSection("City:CitiesArray").Get<string[]>();
            string IDOWeather = Constants.OPEN_WEATHER_APPID;
            RestClient client;
            RestRequest request;
            foreach (var city in cities)
            {
                try
                {
                    client = new RestClient(ConfigurationManager.AppSetting["OpenWeatherApi:WeatherRequestFirstPart"] + $"{city}" + ConfigurationManager.AppSetting["OpenWeatherApi:WeatherRequestSecondPart"] + $"{IDOWeather}");
                    request = new RestRequest(Method.GET);
                    IRestResponse response = client.Execute(request);

                    if (response.IsSuccessful)
                    {
                        var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                        var weatherResponse = content.ToObject<WeatherData>();
                        City viewModel = new City();
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
                        }
                    }
                }
                catch (BrokenCircuitException)
                {
                    HandleBrokenCircuitException();
                }
            }
        }

        private void HandleBrokenCircuitException()
        {
            logger.LogError("Failed to get data from OpenWeatherApi");
        }
    }
}

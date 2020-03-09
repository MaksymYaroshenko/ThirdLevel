using Microsoft.Extensions.Configuration;
using MircroserviceForWorkWithDB.Config;
using MircroserviceForWorkWithDB.Database;
using MircroserviceForWorkWithDB.Database.Entities;
using MircroserviceForWorkWithDB.WeatherDataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

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
            var cities = ConfigurationManager.AppSetting.GetSection("City:CitiesArray").Get<string[]>();
            string IDOWeather = Constants.OPEN_WEATHER_APPID;
            RestClient client;
            RestRequest request;
            foreach (var city in cities)
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
        }
    }
}

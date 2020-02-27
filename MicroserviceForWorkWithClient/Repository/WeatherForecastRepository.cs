using MicroserviceForWorkWithClient.Models;
using Microsoft.Extensions.Logging;
using MircroserviceForWorkWithDB.WeatherDataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace MicroserviceForWorkWithClient.Repository
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        public City GetWeather(string city)
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
            logger.LogInformation($"Started getting data for {city} from database");
            var client = new RestClient($"http://localhost:50000/api/weather/{city}");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                logger.LogInformation($"Got data for {city} from database");
                return content.ToObject<City>();
            }
            logger.LogError($"Couldn't get response for {city}");
            return null;
        }
    }
}

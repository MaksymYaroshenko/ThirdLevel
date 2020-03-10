using MicroserviceForWorkWithClient.Models;
using Microsoft.Extensions.Logging;
using MircroserviceForWorkWithClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly.CircuitBreaker;
using RestSharp;

namespace MicroserviceForWorkWithClient.Repository
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        static ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
             builder
                 .AddFilter("Microsoft", LogLevel.Warning)
                 .AddFilter("System", LogLevel.Warning)
                 .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                 .AddConsole()
                 .AddEventLog();
        });
        ILogger logger = loggerFactory.CreateLogger<WeatherForecastRepository>();

        public City GetWeather(string city)
        {
            logger.LogInformation($"Started getting data for {city} from database");
            try
            {
                var client = new RestClient(ConfigurationManager.AppSetting["MicroserviceForWorkWithDB:SearchCityRequest"] + $"/{city}");
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                    logger.LogInformation($"Got data for {city} from database");
                    return content.ToObject<City>();
                }
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuitException();
            }
            logger.LogError($"Couldn't get response for {city}");
            return null;
        }
        private void HandleBrokenCircuitException()
        {
            logger.LogError("Basket.api is in a circuit-opened mode");
        }
    }
}

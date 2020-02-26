using MicroserviceForWorkWithClient.Models;
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
            var client = new RestClient($"http://localhost:50000/api/weather/{city}");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                return content.ToObject<City>();
            }
            return null;
        }
    }
}

using MicroserviceForWorkWithClient.Models;
using MircroserviceForWorkWithDB.WeatherDataModels;

namespace MicroserviceForWorkWithClient.Repository
{
    interface IWeatherForecastRepository
    {
        City GetWeather(string city);
    }
}

using MicroserviceForWorkWithClient.Models;
using MicroserviceForWorkWithClient.Repository;
using Xunit;

namespace MicroserviceForWorkWithClient.Tests
{
    public class WeatherForecastRepositoryTests
    {
        [Fact]
        public void GetWeatherReturnsNull()
        {
            WeatherForecastRepository controller = new WeatherForecastRepository();
            var result = controller.GetWeather(string.Empty);
            Assert.Null(result);
        }

        [Fact]
        public void GetWeatherReturnsNotNull()
        {
            WeatherForecastRepository controller = new WeatherForecastRepository();
            var result = controller.GetWeather("Sumy");
            Assert.NotNull(result);
        }

        [Fact]
        public void GetWeatherReturnsTypeViewResult()
        {
            WeatherForecastRepository controller = new WeatherForecastRepository();
            var result = controller.GetWeather("Sumy");
            Assert.IsType<City>(result);
        }

    }
}

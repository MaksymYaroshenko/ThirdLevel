using MicroserviceForWorkWithClient.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace MicroserviceForWorkWithClient.Tests
{
    public class CityControllerTests
    {
        readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                .AddConsole()
                .AddEventLog();
        });

        [Fact]
        public void SearchCityResultNotNull()
        {
            CityController controller = new CityController(_loggerFactory);
            ViewResult result = controller.SearchCity() as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void CityWeatherResultNotNull()
        {
            CityController controller = new CityController(_loggerFactory);
            ViewResult result = controller.CityWeather() as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void CityWeatherReturnsTypeViewResult()
        {
            CityController controller = new CityController(_loggerFactory);
            ViewResult result = controller.CityWeather() as ViewResult;
            Assert.IsType<ViewResult>(result);
        }
    }
}

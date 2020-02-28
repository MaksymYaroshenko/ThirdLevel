using Microsoft.Extensions.Logging;
using MircroserviceForWorkWithDB.Controllers;
using MircroserviceForWorkWithDB.Database.Entities;
using Xunit;

namespace MicroserviceForWorkWithDB.Tests
{
    public class WeatherControllerTests
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
        public void GetNotNull()
        {
            WeatherController controller = new WeatherController(_loggerFactory);
            City city = controller.Get("Sumy") as City;
            Assert.NotNull(city);
        }

        [Fact]
        public void GetNull()
        {
            WeatherController controller = new WeatherController(_loggerFactory);
            City city = controller.Get(string.Empty) as City;
            Assert.Null(city);
        }

    }
}

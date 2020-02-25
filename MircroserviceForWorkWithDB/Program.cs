using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MircroserviceForWorkWithDB.Repository;
using System.Threading.Tasks;

namespace MircroserviceForWorkWithDB
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WeatherForecastRepository weatherForecastRepository = new WeatherForecastRepository();
            await Task.Run(() => weatherForecastRepository.GetWeatherData());
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

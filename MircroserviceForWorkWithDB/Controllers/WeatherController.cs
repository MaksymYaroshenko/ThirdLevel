using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MircroserviceForWorkWithDB.Database;
using MircroserviceForWorkWithDB.Database.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MircroserviceForWorkWithDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        DatabaseContext db;
        private readonly ILogger _logger;

        public WeatherController(ILoggerFactory logFactory)
        {
            db = new DatabaseContext();
            _logger = logFactory.CreateLogger<WeatherController>();
        }

        [HttpGet("{city}", Name = "Get")]
        public City Get(string city)
        {
            _logger.LogInformation($"Started finding {city}'s data");
            var list = new List<City>();
            foreach (var cityName in db.City)
            {
                if (cityName.Name == city)
                    list.Add(cityName);
            }
            if (list.Count > 0)
            {
                _logger.LogInformation($"Return {city}'s data");
                return list.Last();
            }
            else
            {
                _logger.LogError("City wasn't find in the database");
                return null;
            }
        }

        [HttpGet]
        public List<string> GetCities()
        {
            List<string> allCities = new List<string>();
            foreach (var b in db.City)
            {
                allCities.Add(b.Name);
            }
            if (allCities.Count > 0)
            {
                var uniqeCities = allCities.Distinct().ToList();
                return uniqeCities;
            }
            else
            {
                return null;
            }
        }
    }
}

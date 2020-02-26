using Microsoft.AspNetCore.Mvc;
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

        public WeatherController()
        {
            db = new DatabaseContext();
        }

        [HttpGet("{city}", Name = "Get")]
        public City Get(string city)
        {
            var list = new List<City>();
            foreach (var cityName in db.City)
            {
                if (cityName.Name == city)
                    list.Add(cityName);
            }
            if (list.Count > 0)
                return list.Last();
            else
                return null;
        }
    }
}

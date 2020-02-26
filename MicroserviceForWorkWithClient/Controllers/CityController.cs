using MicroserviceForWorkWithClient.Models;
using MicroserviceForWorkWithClient.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceForWorkWithClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        // GET: api/City
        [HttpGet]
        public IActionResult SearchCity()
        {
            ViewBag.Title = "Select City";
            var viewModel = new SearchCity();
            return View(viewModel);
        }

        // GET: api/City/5
        public IActionResult Get(string City)
        {
            return View();
        }

        // POST: api/City
        [HttpPost]
        public IActionResult CityWeather()
        {
            string city = HttpContext.Request.Form["CityName"].ToString();
            WeatherForecastRepository weatherForecastRepository = new WeatherForecastRepository();
            City weatherData = weatherForecastRepository.GetWeather(city);
            ViewBag.Title = "Selected City";
            return View(weatherData);
        }
    }
}

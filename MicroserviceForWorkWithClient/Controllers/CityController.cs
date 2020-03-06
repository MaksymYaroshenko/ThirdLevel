using MicroserviceForWorkWithClient.Models;
using MicroserviceForWorkWithClient.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MicroserviceForWorkWithClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly ILogger _logger;

        public CityController(ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger<CityController>();
        }

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
            try
            {
                string city = HttpContext.Request.Form["CityName"].ToString();
                WeatherForecastRepository weatherForecastRepository = new WeatherForecastRepository();
                City weatherData = weatherForecastRepository.GetWeather(city);
                if (weatherData != null)
                {
                    ViewBag.Title = "Selected City";
                    _logger.LogInformation($"Repsonse for {city}");
                    return View(weatherData);
                }
                else
                {
                    ViewBag.Title = "Selected City";
                    _logger.LogError("Weather data is null");
                    return View("CityNotFound");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Request for selected city exception: " + ex.ToString());
                return View("CityNotFound");
            }
        }
    }
}

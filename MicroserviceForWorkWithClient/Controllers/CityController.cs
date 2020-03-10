using MicroserviceForWorkWithClient.Models;
using MicroserviceForWorkWithClient.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MircroserviceForWorkWithClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly.CircuitBreaker;
using RestSharp;
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
            try
            {
                var client = new RestClient(ConfigurationManager.AppSetting["MicroserviceForWorkWithDB:SearchCityRequest"]);
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    _logger.LogInformation("Response is successful");
                    var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                    if (content != null)
                    {
                        var viewModel = new SearchCity();
                        foreach (var cityName in content.Root)
                            viewModel.CitiesList.Add(cityName.ToString());
                        if (viewModel.CitiesList.Count > 0)
                        {
                            _logger.LogInformation("Got cities list");
                            ViewBag.Title = "Select City";
                            return View(viewModel);
                        }
                        else
                        {
                            _logger.LogError("Cities list is empty");
                            ViewBag.Title = "Error 404...";
                            return View("EmptyCitiesList");
                        }
                    }
                    else
                    {
                        _logger.LogError("Response is null");
                        ViewBag.Title = "Error 404...";
                        return View("EmptyCitiesList");
                    }
                }
                else
                {
                    _logger.LogError("Response was not successful");
                    ViewBag.Title = "Error 404..";
                    return View("EmptyCitiesList");
                }
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuitException();
            }
            ViewBag.Title = "Error 404...";
            return View("EmptyCitiesList");
        }
        private void HandleBrokenCircuitException()
        {
            _logger.LogError("Basket.api is in a circuit-opened mode");
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
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuitException();
                ViewBag.Title = "Error 404...";
                return View("EmptyCitiesList");
            }
            catch (Exception ex)
            {
                _logger.LogError("Request for selected city exception: " + ex.ToString());
                return View("CityNotFound");
            }
        }
    }
}

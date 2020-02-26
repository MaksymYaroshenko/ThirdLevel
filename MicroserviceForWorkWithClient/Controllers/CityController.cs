using MicroserviceForWorkWithClient.Models;
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
            ViewBag.Title = "Page with automobiles";
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
        public IActionResult SearchCityPost()
        {
            return View();
        }
    }
}

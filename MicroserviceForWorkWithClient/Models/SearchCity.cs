using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicroserviceForWorkWithClient.Models
{
    public class SearchCity
    {
        [Display(Name = "City Name")]
        public string CityName { get; set; }

        public List<string> CitiesList = new List<string>();
    }
}

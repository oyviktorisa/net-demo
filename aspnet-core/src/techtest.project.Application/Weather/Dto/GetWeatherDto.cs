using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace techtest.project.Weather.Dto
{
    public class GetWeatherDto
    {
        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace techtest.project.Weather.Dto
{
    public class WeatherDto
    {
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public double WindSpeed { get; set; }
        public double WindDegree { get; set; }
        public double Visibility { get; set; }
        public string SkyConditions { get; set; }
        public double TemperatureC { get; set; }
        public double TemperatureF { get; set; }
        public double RelativeHumidity { get; set; }
        public double Pressure { get; set; }
    }
}

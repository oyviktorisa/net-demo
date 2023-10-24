using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techtest.project.Authorization.Roles;
using techtest.project.Authorization.Users;
using techtest.project.Authorization;
using techtest.project.Roles.Dto;
using techtest.project.Weather.Dto;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace techtest.project.Weather
{
    public class WeatherService : projectAppServiceBase, IWeatherService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base()
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<WeatherDto> GetCurrentWeather(GetWeatherDto dto)
        {
            if(string.IsNullOrEmpty(dto.Country) || string.IsNullOrEmpty(dto.City))
            {
                throw new ArgumentNullException();
            }

            string api = _configuration.GetSection("API").Value;
            string apiKey = _configuration.GetSection("APIKey").Value;

            WeatherDto weatherDto = null;

            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient();
                var weatherResponse = await httpClient.GetAsync(api + dto.City + "," + dto.Country + "&APPID=" + apiKey);

                var weatherJSON = JObject.Parse(await weatherResponse.Content.ReadAsStringAsync());

                if (weatherResponse.StatusCode==HttpStatusCode.NotFound)
                {
                    throw new EntityNotFoundException(weatherJSON.SelectToken("message").ToString());
                } else if(weatherResponse.StatusCode!=HttpStatusCode.OK) {
                    throw new AbpException(weatherJSON.SelectToken("message").ToString());
                }

                Logger.Info(weatherJSON.ToString());

                weatherDto = new WeatherDto();

                weatherDto.Location = weatherJSON.SelectToken("name").ToString();
                weatherDto.Time = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                weatherDto.Time = weatherDto.Time.AddSeconds(double.Parse(weatherJSON.SelectToken("dt").ToString())).ToLocalTime();
                weatherDto.WindSpeed = double.Parse(weatherJSON.SelectToken("$.wind.speed").ToString());
                weatherDto.WindDegree = double.Parse(weatherJSON.SelectToken("$.wind.deg").ToString());
                weatherDto.Visibility = double.Parse(weatherJSON.SelectToken("visibility").ToString());
                weatherDto.SkyConditions = weatherJSON.SelectToken("$.weather[0].main").ToString();
                double temp = double.Parse(weatherJSON.SelectToken("$.main.temp").ToString());
                weatherDto.TemperatureF = Math.Round((temp - 273.15) * 9 / 5 + 32, 1);
                weatherDto.TemperatureC = Math.Round(5.0 / 9.0 * (weatherDto.TemperatureF - 32),1);
                weatherDto.RelativeHumidity = double.Parse(weatherJSON.SelectToken("$.main.humidity").ToString());
                weatherDto.Pressure = double.Parse(weatherJSON.SelectToken("$.main.pressure").ToString());

                return weatherDto;

            } catch (Exception ex)
            {
                if(ex.GetType() == typeof(EntityNotFoundException)) { 
                    throw new EntityNotFoundException(ex.Message);
                }

                throw new AbpException(ex.Message);
            }
        }

        public ListResultDto<LocationDto> GetLocation(string parent)
        {
            List<LocationDto> locs = new List<LocationDto>();
            locs.Add(new LocationDto { Parent = null, Name = "Indonesia" });
            locs.Add(new LocationDto { Parent = null, Name = "Malaysia" });
            locs.Add(new LocationDto { Parent = null, Name = "Singapore" });
            locs.Add(new LocationDto { Parent = "Indonesia", Name = "Jakarta" });
            locs.Add(new LocationDto { Parent = "Indonesia", Name = "Medan" });
            locs.Add(new LocationDto { Parent = "Indonesia", Name = "Bandung" });
            locs.Add(new LocationDto { Parent = "Malaysia", Name = "Kuala Lumpur" });
            locs.Add(new LocationDto { Parent = "Malaysia", Name = "Penang" });
            locs.Add(new LocationDto { Parent = "Malaysia", Name = "Johor" });
            locs.Add(new LocationDto { Parent = "Singapore", Name = "Singapore" });
            locs.Add(new LocationDto { Parent = "Singapore", Name = "Serangoon" });
            locs.Add(new LocationDto { Parent = "Singapore", Name = "Jurong Town" });

            if (string.IsNullOrEmpty(parent))
            {
                List<LocationDto> countries = locs.Where(x => x.Parent == null).ToList();

                return new ListResultDto<LocationDto>(){
                    Items = countries
                };
            } else
            {
                List<LocationDto> cities = locs.Where(x => x.Parent == parent).ToList();
                if(cities.Count == 0)
                {
                    throw new EntityNotFoundException();
                }

                return new ListResultDto<LocationDto>()
                {
                    Items = cities
                };
            }
        }
    }
}

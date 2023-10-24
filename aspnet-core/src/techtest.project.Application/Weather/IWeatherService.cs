using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using techtest.project.Roles.Dto;
using techtest.project.Sessions.Dto;
using techtest.project.Weather.Dto;

namespace techtest.project.Weather
{
    public interface IWeatherService : IApplicationService
    {
        ListResultDto<LocationDto> GetLocation(string parent);
        Task<WeatherDto> GetCurrentWeather(GetWeatherDto dto);
    }
}

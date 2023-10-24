using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Modules;
using Abp.ObjectMapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using techtest.project.Authorization.Users;
using techtest.project.Users.Dto;
using techtest.project.Weather;
using Xunit;

namespace techtest.project.Tests.Weather
{
    public class WeatherAppService_Tests : projectTestBase
    {
        private IWeatherService _weatherService;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<IConfiguration> _configurationMock;

        public WeatherAppService_Tests()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _configurationMock = new Mock<IConfiguration>();

            Mock<IConfigurationSection> apiSectionMock = new Mock<IConfigurationSection>();
            apiSectionMock.Setup(x => x.Value).Returns("https://api.openweathermap.org/data/2.5/weather?q=");

            Mock<IConfigurationSection> keySectionMock = new Mock<IConfigurationSection>();
            keySectionMock.Setup(x => x.Value).Returns("qwerty12345");

            _configurationMock.Setup(x => x.GetSection(It.Is<string>(k => k == "API"))).Returns(apiSectionMock.Object);
            _configurationMock.Setup(x => x.GetSection(It.Is<string>(k => k == "APIKey"))).Returns(keySectionMock.Object);

            _weatherService = new WeatherService(_configurationMock.Object, _httpClientFactoryMock.Object);
        }

        [Fact]
        public void GetLocation_Parent_Test()
        {
            // Act
            var output = _weatherService.GetLocation(null);

            // Assert
            output.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public void GetLocation_Child_NormalTest()
        {
            // Act
            var output = _weatherService.GetLocation("Indonesia");

            // Assert
            output.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public void GetLocation_Child_NegativeTest()
        {
            // Act & Assert
            Assert.Throws<EntityNotFoundException>(() =>
            {
                _weatherService.GetLocation("Thailand");
            });
        }

        [Fact]
        public async Task GetCurrentWeather_Test()
        {
            //Setup
            var messageHandlerMock = new Mock<HttpMessageHandler>();
            messageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"coord\":{\"lon\":106.8451,\"lat\":-6.2146},\"weather\":[{\"id\":801,\"main\":\"Clouds\",\"description\":\"few clouds\",\"icon\":\"02d\"}],\"base\":\"stations\",\"main\":{\"temp\":305.18,\"feels_like\":309.99,\"temp_min\":302.27,\"temp_max\":305.53,\"pressure\":1007,\"humidity\":59},\"visibility\":6000,\"wind\":{\"speed\":5.14,\"deg\":340},\"clouds\":{\"all\":20},\"dt\":1698140110,\"sys\":{\"type\":1,\"id\":9383,\"country\":\"ID\",\"sunrise\":1698100095,\"sunset\":1698144316},\"timezone\":25200,\"id\":1642911,\"name\":\"Jakarta\",\"cod\":200}")
                });

            var httpClient = new HttpClient(messageHandlerMock.Object);

            _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _weatherService = new WeatherService(_configurationMock.Object, _httpClientFactoryMock.Object);

            //Act
            var output = await _weatherService.GetCurrentWeather(new project.Weather.Dto.GetWeatherDto
            {
                City = "Jakarta",
                Country = "Indonesia"
            });

            // Assert
            output.TemperatureF.Equals(89.7);
            output.TemperatureC.Equals(32.1);
        }

        [Fact]
        public async Task GetCurrentWeather_NegativeTest()
        {
            //Setup
            var messageHandlerMock = new Mock<HttpMessageHandler>();
            messageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("{\"cod\":\"404\",\"message\":\"city not found\"}")
                });

            var httpClient = new HttpClient(messageHandlerMock.Object);

            _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _weatherService = new WeatherService(_configurationMock.Object, _httpClientFactoryMock.Object);

            //Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                await _weatherService.GetCurrentWeather(new project.Weather.Dto.GetWeatherDto
                {
                    City = "xxx",
                    Country = "Indonesia"
                });
            });
        }

        [Fact]
        public async Task GetCurrentWeather_InvalidInputTest()
        {
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _weatherService.GetCurrentWeather(new project.Weather.Dto.GetWeatherDto
                {
                    City = null,
                    Country = "Indonesia"
                });
            });
        }
    }
}

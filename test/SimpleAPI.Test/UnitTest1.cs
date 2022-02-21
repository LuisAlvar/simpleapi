using System;
using Xunit;
using SimpleAPI.Controllers;
using Microsoft.AspNetCore.Mvc.Core;
using System.Linq;

namespace SimpleAPI.Test
{
    public class UnitTest1
    {
        WeatherForecast _Forecast = new WeatherForecast();

        [Fact]
        public void TestOneItem()
        {

            var rng = new Random();

            _Forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = SimpleAPI.Controllers.WeatherForecastController
                    .Summaries[rng.Next(SimpleAPI.Controllers.WeatherForecastController.Summaries.Length)]
            }).FirstOrDefault();
            
            Assert.IsType<DateTime>(_Forecast.Date);
            Assert.IsType<int>(_Forecast.TemperatureC);
            Assert.IsType<int>(_Forecast.TemperatureF);
            Assert.IsType<string>(_Forecast.Summary);
            Assert.NotEmpty(_Forecast.Summary);
            Assert.NotEmpty(_Forecast.TemperatureC.ToString());
        }

    }
}

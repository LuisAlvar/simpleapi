using System;
using Xunit;
using SimpleAPI.Controllers;

namespace SimpleAPI.Test
{
    public class UnitTest1
    {
        WeatherForecastController _controller = new WeatherForecastController();

        [Fact]
        public void GetReturnsMyForecast()
        {
            var returnValue = _controller.GetFirst();

            Assert.IsType<DateTime>(returnValue.Date);
            Assert.IsType<int>(returnValue.TemperatureC);
            Assert.IsType<int>(returnValue.TemperatureF);
            Assert.IsType<string>(returnValue.Summary);
        }
    }
}

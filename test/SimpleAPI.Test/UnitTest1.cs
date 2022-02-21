using System;
using Xunit;
using SimpleAPI.Controllers;
using Microsoft.AspNetCore.Mvc.Core;

namespace SimpleAPI.Test
{
    public class UnitTest1
    {
        WeatherForecastController _controller = new WeatherForecastController();

        [Fact]
        public void TestOneItem()
        {
            var obj = _controller.GetFirst();
            Assert.IsType<DateTime>(obj.Date);
            Assert.IsType<int>(obj.TemperatureC);
            Assert.IsType<int>(obj.TemperatureF);
            Assert.IsType<string>(obj.Summary);
        }

    }
}

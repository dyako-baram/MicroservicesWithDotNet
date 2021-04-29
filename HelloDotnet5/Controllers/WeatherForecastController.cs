using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelloDotnet5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherClient _cleint;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,WeatherClient cleint)
        {
            _logger = logger;
            _cleint = cleint;
        }

        [HttpGet]
        [Route("{city}")]
        public async Task<WeatherForecast> Get(string city)
        {
           var foreCast=await _cleint.GetCurrentWeatherAsync(city);
            return new WeatherForecast{
                Summary=foreCast.weather[0].description,
                TemperatureC=(int)foreCast.main.temp,
                Date=DateTimeOffset.FromUnixTimeSeconds(foreCast.dt).DateTime

           };
        }
    }
}

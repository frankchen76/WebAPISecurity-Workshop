using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherAPI.Extensions;

namespace WeatherAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FullWeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<FullWeatherForecastController> _logger;

        public FullWeatherForecastController(ILogger<FullWeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            this.HttpContext.ValidateAppRole("Weather.Read.Basic", "Weather.Read.All", "Weather.Read.Full");
            var rng = new Random();
            return Enumerable.Range(1, 8).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                Caller = this.User?.Identity?.Name
            })
            .ToArray();
        }
    }
}

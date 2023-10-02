using Microsoft.AspNetCore.Mvc;
using Weather.Data;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherContext _weatherContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherContext weatherContext)
        {
            _logger = logger;
            _weatherContext = weatherContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var weathers = _weatherContext.WeatherForecasts.ToList();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            //try
            //{
            //    return _weatherContext.WeatherForecasts.ToList().Select(w => new WeatherForecast
            //    {
            //        Date = w.Date,
            //        TemperatureC = w.TemperatureC,
            //        Summary = w.Summary
            //    })
            //    .ToArray();
            //}
            //catch (Exception e)
            //{
            //    _logger.LogError("error while retrieve data", e.Message);
            //    throw;
            //}
        }
    }
}
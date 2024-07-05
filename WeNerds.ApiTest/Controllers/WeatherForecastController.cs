using Microsoft.AspNetCore.Mvc;
using WeNerds.Filters.Implementations;
using WeNerds.Services.Interfaces;

namespace WeNerds.ApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof (WeResultHandlerFilter))]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeNotificationService _notificationService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeNotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            
            //_notificationService.BadRequest("test", "test");
            _notificationService.NotFound("test2", "test2");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}

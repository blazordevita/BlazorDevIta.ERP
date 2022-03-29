using BlazorDevIta.ERP.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDevIta.ERP.BlazorWasm.Server.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecastListItem> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastListItem
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
            })
            .ToArray();
        }

        [HttpPost]
        public IActionResult Post(WeatherForecastListItem model)
        {
            if (ModelState.IsValid)
            {
                // Salviamo i nostri dati
                return Ok(); // Created
            }
            return BadRequest(model);
        }
    }
}
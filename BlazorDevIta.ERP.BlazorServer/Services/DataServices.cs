using BlazorDevIta.ERP.Shared;
using BlazorDevIta.UI.Services;

namespace BlazorDevIta.ERP.BlazorServer.Services
{
    public class DataServices : IDataServices
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<List<WeatherForecast?>> GetWeatherForecastsAsync()
        {
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToList<WeatherForecast?>();

            return Task.FromResult(result);
        }
    }
}

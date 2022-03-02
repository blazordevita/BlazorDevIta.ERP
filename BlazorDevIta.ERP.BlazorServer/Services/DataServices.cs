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

        public Task<WeatherForecast[]?> GetWeatherForecastsAsync()
        {
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();

            return Task.FromResult((WeatherForecast[]?)result);
        }
    }
}

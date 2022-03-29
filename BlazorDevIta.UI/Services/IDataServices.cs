using BlazorDevIta.ERP.Shared;

namespace BlazorDevIta.UI.Services
{
    public interface IDataServices
    {
        Task<List<WeatherForecastListItem?>> GetWeatherForecastsAsync();

        Task<WeatherForecastDetails?> GetWeatherForecastByIdAsync(int id);
    }
}

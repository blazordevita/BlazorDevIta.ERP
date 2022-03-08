using BlazorDevIta.ERP.Shared;

namespace BlazorDevIta.UI.Services
{
    public interface IDataServices
    {
        Task<List<WeatherForecast?>> GetWeatherForecastsAsync();
    }
}

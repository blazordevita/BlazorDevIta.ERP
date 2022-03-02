using BlazorDevIta.ERP.Shared;

namespace BlazorDevIta.UI.Services
{
    public interface IDataServices
    {
        Task<WeatherForecast[]?> GetWeatherForecastsAsync();
    }
}

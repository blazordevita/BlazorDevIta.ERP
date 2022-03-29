using BlazorDevIta.ERP.Shared;
using BlazorDevIta.UI.Services;
using System.Net.Http.Json;

namespace BlazorDevIta.ERP.BlazorWasm.Client.Services
{
    public class DataServices : IDataServices
    {
        private readonly HttpClient _http;

        public DataServices(HttpClient http)
        {
            _http = http;
        }

        public Task<WeatherForecastDetails?> GetWeatherForecastByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<WeatherForecastListItem?>> GetWeatherForecastsAsync()
        {
            return _http.GetFromJsonAsync<List<WeatherForecastListItem?>>("WeatherForecast");
        }
    }
}

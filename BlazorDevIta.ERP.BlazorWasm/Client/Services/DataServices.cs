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

        public Task<List<WeatherForecast?>> GetWeatherForecastsAsync()
        {
            return _http.GetFromJsonAsync<List<WeatherForecast?>>("WeatherForecast");
        }
    }
}

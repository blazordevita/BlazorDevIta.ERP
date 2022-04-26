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

		public Task Create(WeatherForecastDetails details)
		{
			return _http.PostAsJsonAsync($"WeatherForecast", details);
		}

		public Task Delete(int id)
		{
			return _http.DeleteAsync($"WeatherForecast/{id}");
		}

		public Task<WeatherForecastDetails?> GetWeatherForecastByIdAsync(int id)
		{
			return _http.GetFromJsonAsync<WeatherForecastDetails?>($"WeatherForecast/{id}")!;
		}

		public Task<List<WeatherForecastListItem?>> GetWeatherForecastsAsync()
		{
			return _http.GetFromJsonAsync<List<WeatherForecastListItem?>>("WeatherForecast")!;
		}

		public Task Update(WeatherForecastDetails details)
		{
			return _http.PutAsJsonAsync($"WeatherForecast/{details.Id}", details);
		}
	}
}
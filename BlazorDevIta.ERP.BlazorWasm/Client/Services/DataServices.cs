using BlazorDevIta.ERP.Infrastructure.DataTypes;
using BlazorDevIta.UI.Services;
using System.Net.Http.Json;

namespace BlazorDevIta.ERP.BlazorWasm.Client.Services
{
	public class DataServices<ListItemType, DetailsType, IdType> 
		: IDataServices<ListItemType, DetailsType, IdType>
		where DetailsType : BaseDetails<IdType>
		where ListItemType : BaseListItem<IdType>
	{
		private readonly HttpClient _http;
        private readonly IConfiguration _configuration;

        public DataServices(HttpClient http, IConfiguration configuration)
		{
			_http = http;
			_configuration = configuration;
		}

		public Task CreateAsync(DetailsType details)
		{
			var baseUrl = getBaseUrl<DetailsType>();
			return _http.PostAsJsonAsync($"{baseUrl}", details);
		}

		public Task DeleteAsync(IdType id)
		{
			var baseUrl = getBaseUrl<DetailsType>();
			return _http.DeleteAsync($"{baseUrl}/{id}");
		}

		public Task<DetailsType?> GetByIdAsync(IdType id)
		{
			var baseUrl = getBaseUrl<DetailsType>();
			return _http.GetFromJsonAsync<DetailsType?>($"{baseUrl}/{id}")!;
		}

		public Task<Page<ListItemType, IdType>> GetAllAsync(PageParameters parameters)
		{
			var baseUrl = getBaseUrl<ListItemType>();
			return _http.GetFromJsonAsync<Page<ListItemType, IdType>>(
				$"{baseUrl}?OrderBy={parameters.OrderBy}&OrderByDirection={parameters.OrderByDirection}&Page={parameters.Page}")!;
		}

		public Task UpdateAsync(DetailsType details)
		{
			var baseUrl = getBaseUrl<DetailsType>();
			return _http.PutAsJsonAsync($"{baseUrl}/{details.Id}", details);
		}

		private string getBaseUrl<T>()
        {
			var baseUrl = _configuration[$"ApiUrls:{typeof(T).Name}"];
			if (baseUrl == null) throw new Exception($"ApiUrls:{typeof(T).Name} not configured");
			return baseUrl;
		}
	}
}
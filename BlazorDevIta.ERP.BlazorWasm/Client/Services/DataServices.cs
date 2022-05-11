using BlazorDevIta.ERP.Infrastructure.DataTypes;
using BlazorDevIta.UI.Services;
using System.Net.Http.Json;

namespace BlazorDevIta.ERP.BlazorWasm.Client.Services;

public class DataServices<ListItemType, DetailsType, IdType> 
	: IDataServices<ListItemType, DetailsType, IdType>
	where DetailsType : BaseDetails<IdType>
	where ListItemType : BaseListItem<IdType>
{
	private readonly HttpClient _http;

	public string epCreate { get; init; }
	public string epDelete { get; init; }
	public string epGetById { get; init; }
	public string epUpdate { get; init; }

	public string epGetAll { get; init; }

	public DataServices(HttpClient http, IConfiguration configuration)
	{
		_http = http;
		configuration.GetSection($"DataServices:{typeof(DetailsType).Name}").Bind(this);
		configuration.GetSection($"DataServices:{typeof(ListItemType).Name}").Bind(this);

		if (string.IsNullOrEmpty(epCreate))
			throw new Exception($"DataServices:{nameof(epCreate)} not configured");
		if (string.IsNullOrEmpty(epDelete))
			throw new Exception($"DataServices:{nameof(epDelete)} not configured");
		if (string.IsNullOrEmpty(epGetById))
			throw new Exception($"DataServices:{nameof(epGetById)} not configured");
		if (string.IsNullOrEmpty(epUpdate))
			throw new Exception($"DataServices:{nameof(epUpdate)} not configured");
		if (string.IsNullOrEmpty(epGetAll))
			throw new Exception($"DataServices:{nameof(epGetAll)} not configured");
	}

	public Task CreateAsync(DetailsType details)
	=> _http.PostAsJsonAsync(epCreate, details);
	

	public Task DeleteAsync(IdType id)
	=> _http.DeleteAsync($"{epDelete}/{id}");
	

	public Task<DetailsType?> GetByIdAsync(IdType id)
	=> _http.GetFromJsonAsync<DetailsType?>($"{epGetById}/{id}")!;
	

	public Task<Page<ListItemType, IdType>> GetAllAsync(PageParameters parameters)
	=> _http.GetFromJsonAsync<Page<ListItemType, IdType>>(
			$"{epGetAll}?OrderBy={parameters.OrderBy}&OrderByDirection={parameters.OrderByDirection}")!;
	

	public Task UpdateAsync(DetailsType details)
	=> _http.PutAsJsonAsync($"{epUpdate}/{details.Id}", details);

}

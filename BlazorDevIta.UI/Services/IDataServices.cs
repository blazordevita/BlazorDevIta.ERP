using BlazorDevIta.ERP.Shared;

namespace BlazorDevIta.UI.Services
{
	public interface IDataServices<ListItemType, DetailsType, IdType>
	{
		Task<List<ListItemType?>> GetAllAsync();

		Task<DetailsType?> GetByIdAsync(IdType id);

		Task CreateAsync(DetailsType details);

		Task UpdateAsync(DetailsType details);

		Task DeleteAsync(IdType id);
	}
}
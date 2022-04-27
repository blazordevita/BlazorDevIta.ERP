namespace BlazorDevIta.ERP.Infrastructure
{
	public interface IEntity<TKey>
	{
		TKey Id { get; set; }
	}
}
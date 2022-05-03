namespace BlazorDevIta.ERP.Infrastructure.DataTypes
{
    public abstract class BaseListItem<IdType>
    {
        public IdType? Id { get; set; }
    }
}

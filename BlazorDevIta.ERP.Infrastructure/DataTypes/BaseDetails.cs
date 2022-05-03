namespace BlazorDevIta.ERP.Infrastructure.DataTypes
{
    public abstract class BaseDetails<IdType>
    {
        public IdType? Id { get; set; }
    }
}

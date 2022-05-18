using BlazorDevIta.ERP.Infrastructure.Attributes;

namespace BlazorDevIta.ERP.Infrastructure.DataTypes
{
    public abstract class BaseListItem<IdType>
    {
        [DefaultOrderDirection(OrderDirection.Ascendent)]
        public IdType? Id { get; set; }
    }
}

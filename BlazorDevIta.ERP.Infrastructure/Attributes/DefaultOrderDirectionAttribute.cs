using BlazorDevIta.ERP.Infrastructure.DataTypes;

namespace BlazorDevIta.ERP.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class DefaultOrderDirectionAttribute : Attribute
{

    public DefaultOrderDirectionAttribute(OrderDirection direction)
    {
        Direction=direction; ;
    }

    public OrderDirection Direction { get; set; }

}
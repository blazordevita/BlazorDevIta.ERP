namespace BlazorDevIta.ERP.Infrastructure.DataTypes
{
    public class Page<ListItemType, IdType>
        where ListItemType : BaseListItem<IdType>
    {
        public string? OrderBy { get; set; }
        public OrderDirection OrderByDirection { get; set; }
        public List<ListItemType>? Items { get; set; }
    }
}

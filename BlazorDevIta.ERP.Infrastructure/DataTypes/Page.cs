namespace BlazorDevIta.ERP.Infrastructure.DataTypes
{
    public class Page<ListItemType, IdType>
        where ListItemType : BaseListItem<IdType>
    {
        public int ItemCount { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }

        public string? OrderBy { get; set; }
        public OrderDirection OrderByDirection { get; set; }
        public List<ListItemType>? Items { get; set; }
    }
}

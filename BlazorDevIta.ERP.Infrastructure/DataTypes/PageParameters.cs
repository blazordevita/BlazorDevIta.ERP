namespace BlazorDevIta.ERP.Infrastructure.DataTypes
{
    public class PageParameters
    {
        public string? OrderBy { get; set; }
        public OrderDirection OrderByDirection { get; set; }

        public static PageParameters Default 
            => new PageParameters() { OrderBy = "Id", OrderByDirection = OrderDirection.Ascendent };
    }
}

﻿namespace BlazorDevIta.ERP.Infrastructure.DataTypes
{
    public class PageParameters
    {
        public string? FilterText { get; set; }
        public int Page { get; set; } = 1;
        public string? OrderBy { get; set; }
        public OrderDirection OrderByDirection { get; set; }
    }
}

using BlazorDevIta.ERP.Infrastructure.Attributes;
using BlazorDevIta.ERP.Infrastructure.DataTypes;
using System.ComponentModel.DataAnnotations;

namespace BlazorDevIta.ERP.Shared
{
	public class WeatherForecastListItem : BaseListItem<int>
	{
		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
		[DefaultOrderDirection(OrderDirection.Descendent)]
		public DateTime Date { get; set; }

		[Display(Name = "Temp (C)")]
		[DefaultOrderDirection(OrderDirection.Ascendent)]
		public int TemperatureC { get; set; }

        [NotOrderable]
		[Display(Name = "Temp (F)")]
		public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
	}
}
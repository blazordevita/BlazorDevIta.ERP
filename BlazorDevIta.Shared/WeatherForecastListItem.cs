using BlazorDevIta.ERP.Infrastructure.Attributes;
using BlazorDevIta.ERP.Infrastructure.DataTypes;
using System.ComponentModel.DataAnnotations;

namespace BlazorDevIta.ERP.Shared
{
	public class WeatherForecastListItem : BaseListItem<int>
	{
		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
		public DateTime Date { get; set; }

		[Display(Name = "Temp (C)")]
		public int TemperatureC { get; set; }

		[Display(Name = "Temp (F)")]
        [NotOrderable]
		public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
	}
}
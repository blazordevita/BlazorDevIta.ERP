using BlazorDevIta.ERP.Infrastructure.DataTypes;
using System.ComponentModel.DataAnnotations;

namespace BlazorDevIta.ERP.Shared
{
	public class WeatherForecastDetails: BaseDetails<int>
	{
		public DateTime Date { get; set; }

		public int TemperatureC { get; set; }

		[Required]
		[MaxLength(50)]
		public string? Summary { get; set; }

		public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
	}
}
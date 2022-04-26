using System.ComponentModel.DataAnnotations;

namespace BlazorDevIta.ERP.Shared
{
	public class WeatherForecastDetails
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public int TemperatureC { get; set; }

		[Required]
		[MaxLength(50)]
		public string? Summary { get; set; }

		public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
	}
}
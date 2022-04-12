using BlazorDevIta.ERP.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BlazorDevIta.ERP.Shared
{
    public class WeatherForecastListItem
    {
        [Hidden]
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Temp (C)")]
        public int TemperatureC { get; set; }

        [Hidden]
        [Display(Name = "Temp (F)")]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
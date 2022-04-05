using BlazorDevIta.ERP.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BlazorDevIta.ERP.BlazorServer.Data
{
    public class WeatherForecast: IEntity<int>
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Summary { get; set; }
    }
}

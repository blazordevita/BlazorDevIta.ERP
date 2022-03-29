using System.ComponentModel.DataAnnotations;

namespace BlazorDevIta.ERP.BlazorServer.Data
{
    public class WeatherForecast
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Summary { get; set; }
    }
}

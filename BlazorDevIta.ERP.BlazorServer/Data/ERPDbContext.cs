using Microsoft.EntityFrameworkCore;

namespace BlazorDevIta.ERP.BlazorServer.Data
{
    public class ERPDbContext : DbContext
    {
        public ERPDbContext(DbContextOptions opt)
            : base(opt) { }

        public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();
    }
}

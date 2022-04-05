using Microsoft.EntityFrameworkCore;

namespace BlazorDevIta.ERP.BlazorServer.Data
{
    public class ERPDbContext : DbContext
    {
        public ERPDbContext(DbContextOptions opt)
            : base(opt) { }

        public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var x = await base.SaveChangesAsync(cancellationToken);
            ChangeTracker.Clear();
            return x;
        }
    }
}

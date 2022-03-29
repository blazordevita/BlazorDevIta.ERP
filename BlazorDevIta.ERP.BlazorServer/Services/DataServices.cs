using BlazorDevIta.ERP.BlazorServer.Data;
using BlazorDevIta.ERP.Shared;
using BlazorDevIta.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace BlazorDevIta.ERP.BlazorServer.Services
{
    public class DataServices : IDataServices
    {
        private readonly ERPDbContext _dbContext;

        public DataServices(ERPDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<WeatherForecastListItem?>> GetWeatherForecastsAsync()
        {
            return _dbContext.WeatherForecasts.Select(x =>
                new WeatherForecastListItem()
                {
                    Id = x.Id,
                    Date = x.Date,
                    TemperatureC = x.TemperatureC
                }).ToListAsync<WeatherForecastListItem?>();
        }

        public Task<WeatherForecastDetails?> GetWeatherForecastByIdAsync(int id)
        {
            return _dbContext.WeatherForecasts
                .Where(x=> x.Id == id)
                .Select(x =>
                    new WeatherForecastDetails()
                    {
                        Id = x.Id,
                        Date = x.Date,
                        TemperatureC = x.TemperatureC,
                        Summary = x.Summary
                    }).SingleOrDefaultAsync();
        }
    }
}

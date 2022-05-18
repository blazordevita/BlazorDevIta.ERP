using BlazorDevIta.ERP.Business.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorDevIta.ERP.BlazorWasm.Server.Extensions;

public static class WebApplicationExtensions
{
    public static async Task<IApplicationBuilder> InizializeDatabases(this WebApplication app)
    {
        using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
        {
            var serviceProvider = serviceScope.ServiceProvider;
            IConfiguration config = serviceProvider.GetRequiredService<IConfiguration>();
            var appCtx = serviceProvider.GetRequiredService<ERPDbContext>();
            
            //Applica le ultime migrazioni
            appCtx.Database.Migrate(); 

            await EnsureSetup(config, appCtx);

        }
        return app;
    }

    private static Task EnsureSetup(IConfiguration config, ERPDbContext ctx)
    {
        if (!ctx.WeatherForecasts.Any())
        {
            //Per esempio posso precaricare la tabella WeatherForecasts con valori iniziali configurati nell'appsettings

            //var weatherForecasts = config.GetSection(nameof(ctx.WeatherForecasts)).Get<IEnumerable<WeatherForecast>>();
            //foreach (var record in weatherForecasts)
            //{
            //    ctx.Add(record);
            //}
            //ctx.SaveChanges();
        }
        return Task.CompletedTask;
    }
}
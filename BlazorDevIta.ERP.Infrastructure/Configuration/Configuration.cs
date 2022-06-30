using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorDevIta.ERP.Infrastructure.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace BlazorDevIta.ERP.Infrastructure.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection AddBlazorDevItaLocalization<T>(this IServiceCollection services)
        {
            services.AddSingleton<ITextLocalizer>(x =>
                new TextLocalizer(x.GetRequiredService<IStringLocalizerFactory>(), 
                    typeof(T)));
            return services;
        }
    }
}

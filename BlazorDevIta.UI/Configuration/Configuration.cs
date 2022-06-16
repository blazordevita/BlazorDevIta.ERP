using BlazorDevIta.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDevIta.UI.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection AddBlazorDevItaUI(this IServiceCollection services)
        {
            services.AddSingleton<IConfirmService, ConfirmService>();
            return services; ;
        }
    }
}

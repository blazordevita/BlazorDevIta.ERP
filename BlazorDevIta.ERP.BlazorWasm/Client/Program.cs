using BlazorDevIta.ERP.BlazorWasm.Client.Resources;
using BlazorDevIta.ERP.BlazorWasm.Client.Services;
using BlazorDevIta.ERP.Infrastructure.Configuration;
using BlazorDevIta.UI;
using BlazorDevIta.UI.Configuration;
using BlazorDevIta.UI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped(typeof(IDataServices<,,>), typeof(DataServices<,,>));

builder.Services.AddLocalization();
builder.Services.AddBlazorDevItaLocalization<Languages>();

builder.Services.AddBlazorDevItaUI();

var app = builder.Build();

await app.UseDefaultCulture();

await app.RunAsync();
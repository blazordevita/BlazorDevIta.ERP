using BlazorDevIta.ERP.BlazorWasm.Server.Configurations;
using BlazorDevIta.ERP.BlazorWasm.Server.Extensions;
using BlazorDevIta.ERP.Business.Data;
using BlazorDevIta.ERP.Infrastructure;
using BlazorDevIta.ERP.Infrastructure.EF;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ERPDbContext>(opt =>
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("BlazorDevIta.ERP.Migrations.SqlServer")));

//builder.Services.AddDbContext<ERPDbContext>(opt =>
//    opt.UseInMemoryDatabase("BlazorDevItaDB"));

builder.Services.AddScoped<DbContext, ERPDbContext>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped(typeof(IRepository<,>), typeof(EFRepository<,>));

var app = builder.Build();

//Applica le migrazioni mancanti sul DB e inizializza le tabelle se necessario
//await app.InizializeDatabases();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
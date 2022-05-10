using AutoMapper;
using BlazorDevIta.ERP.Business.Data;
using BlazorDevIta.ERP.Infrastructure;
using BlazorDevIta.ERP.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDevIta.ERP.BlazorWasm.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : 
		CRUDController<
			WeatherForecastListItem, WeatherForecastDetails, int, WeatherForecast>
	{
		public WeatherForecastController(
			IMapper mapper,
			IRepository<WeatherForecast, int> repository,
			ILogger<WeatherForecastController> logger)
			: base(mapper, repository, logger) { }
    }
}
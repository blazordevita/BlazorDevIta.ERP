using AutoMapper;
using BlazorDevIta.ERP.Business.Data;
using BlazorDevIta.ERP.Infrastructure;
using BlazorDevIta.ERP.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

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

        protected override Expression<Func<WeatherForecast, bool>>? Filter(string filterText)
        {
			return x => x.Summary != null && x.Summary.Contains(filterText);
        }
    }
}
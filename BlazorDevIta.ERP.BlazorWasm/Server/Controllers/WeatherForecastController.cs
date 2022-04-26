using BlazorDevIta.ERP.Business.Data;
using BlazorDevIta.ERP.Infrastructure;
using BlazorDevIta.ERP.Shared;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorDevIta.ERP.BlazorWasm.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IRepository<WeatherForecast, int> _repository;

		public WeatherForecastController(
			IRepository<WeatherForecast, int> repository,
			ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
			_repository = repository;
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var entity = await _repository.GetByIdAsync(id);
			if (entity == null)
			{
				return NotFound();
			}

			await _repository.DeleteAsync(id);
			return NoContent();
		}

		[HttpGet]
		[ProducesResponseType(typeof(WeatherForecastListItem), StatusCodes.Status200OK)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Get()
		{
			var result = await _repository.GetAll()
				 .Select(x =>
					 new WeatherForecastListItem()
					 {
						 Id = x.Id,
						 Date = x.Date,
						 TemperatureC = x.TemperatureC
					 }).ToListAsync();

			return Ok(result);
		}

		[HttpGet("{id:int}")]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> GetById(int id)
		{
			var entity = await _repository.GetByIdAsync(id);
			if (entity == null)
			{
				return NotFound();
			}

			var result = new WeatherForecastDetails()
			{
				Id = entity.Id,
				Date = entity.Date,
				TemperatureC = entity.TemperatureC,
				Summary = entity.Summary
			};

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Post(WeatherForecastDetails model)
		{
			if (ModelState.IsValid)
			{
				var entity = new WeatherForecast()
				{
					Id = model.Id,
					Date = model.Date,
					TemperatureC = model.TemperatureC,
					Summary = model.Summary
				};
				await _repository.CreateAsync(entity);

				model.Id = entity.Id;

				return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
			}
			return BadRequest(model);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Put(int id, WeatherForecastDetails model)
		{
			var entity = await _repository.GetByIdAsync(id);
			if (entity == null)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				entity.Date = model.Date;
				entity.TemperatureC = model.TemperatureC;
				entity.Summary = model.Summary;
				await _repository.UpdateAsync(entity);
				return NoContent();
			}
			return BadRequest(model);
		}
	}
}
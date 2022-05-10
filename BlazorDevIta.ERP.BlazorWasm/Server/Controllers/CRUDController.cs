using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorDevIta.ERP.Infrastructure;
using BlazorDevIta.ERP.Infrastructure.DataTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorDevIta.ERP.BlazorWasm.Server.Controllers
{
	[ApiController]
	// [Route("[controller]")]
	public class CRUDController<ListItemType, DetailsType, IdType, EntityType> 
		: ControllerBase
		where ListItemType : BaseListItem<IdType>
		where DetailsType : BaseDetails<IdType>
		where EntityType : class, IEntity<IdType>, new()
	{
        protected readonly IMapper _mapper;
        protected readonly ILogger<CRUDController<ListItemType, DetailsType, IdType, EntityType>> _logger;
		protected readonly IRepository<EntityType, IdType> _repository;

		public CRUDController(
			IMapper mapper,
			IRepository<EntityType, IdType> repository,
			ILogger<CRUDController<ListItemType, DetailsType, IdType, EntityType>> logger)
		{
			_mapper = mapper;
			_logger = logger;
			_repository = repository;
		}

		[HttpDelete("{id}")]
		public virtual async Task<IActionResult> Delete(IdType id)
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
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesDefaultResponseType]
		public virtual async Task<IActionResult> Get()
		{
			var result = await _repository.GetAll()
				.ProjectTo<ListItemType>(_mapper.ConfigurationProvider)
				.ToListAsync();

				 /*.Select(x =>
					 new WeatherForecastListItem()
					 {
						 Id = x.Id,
						 Date = x.Date,
						 TemperatureC = x.TemperatureC
					 }).ToListAsync();*/

			return Ok(result);
		}

		[HttpGet("{id}")]
		[ProducesDefaultResponseType]
		public virtual async Task<IActionResult> GetById(IdType id)
		{
			var entity = await _repository.GetByIdAsync(id);
			if (entity == null)
			{
				return NotFound();
			}

			var result = _mapper.Map<DetailsType>(entity);
				
				/*new WeatherForecastDetails()
			{
				Id = entity.Id,
				Date = entity.Date,
				TemperatureC = entity.TemperatureC,
				Summary = entity.Summary
			};*/

			return Ok(result);
		}

		[HttpPost]
		public virtual async Task<IActionResult> Post(DetailsType model)
		{
			if (ModelState.IsValid)
			{
				var entity = _mapper.Map<EntityType>(model);
					/*new WeatherForecast()
				{
					Id = model.Id,
					Date = model.Date,
					TemperatureC = model.TemperatureC,
					Summary = model.Summary
				};*/
				await _repository.CreateAsync(entity);

				model.Id = entity.Id;

				return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
			}
			return BadRequest(model);
		}

		[HttpPut("{id}")]
		public virtual async Task<IActionResult> Put(IdType id, DetailsType model)
		{
			var entity = await _repository.GetByIdAsync(id);
			if (entity == null)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				entity = _mapper.Map<EntityType>(model);

				/*entity.Date = model.Date;
				entity.TemperatureC = model.TemperatureC;
				entity.Summary = model.Summary;*/

				await _repository.UpdateAsync(entity);
				return NoContent();
			}
			return BadRequest(model);
		}
	}
}
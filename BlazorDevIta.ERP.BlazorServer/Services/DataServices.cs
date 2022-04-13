﻿using BlazorDevIta.ERP.Business.Data;
using BlazorDevIta.ERP.Infrastructure;
using BlazorDevIta.ERP.Shared;
using BlazorDevIta.UI.Services;

using Microsoft.EntityFrameworkCore;

namespace BlazorDevIta.ERP.BlazorServer.Services
{
	public class DataServices : IDataServices
	{
		private readonly IRepository<WeatherForecast, int> _repository;

		public DataServices(IRepository<WeatherForecast, int> repository)
		{
			_repository = repository;
		}

		public Task<List<WeatherForecastListItem?>> GetWeatherForecastsAsync()
		{
			return _repository.GetAll()
				.Select(x =>
					new WeatherForecastListItem()
					{
						Id = x.Id,
						Date = x.Date,
						TemperatureC = x.TemperatureC
					}).ToListAsync<WeatherForecastListItem?>();
		}

		public async Task<WeatherForecastDetails?> GetWeatherForecastByIdAsync(int id)
		{
			var x = await _repository.GetByIdAsync(id);
			if (x == null)
			{
				return null;
			}

			return new WeatherForecastDetails()
			{
				Id = x.Id,
				Date = x.Date,
				TemperatureC = x.TemperatureC,
				Summary = x.Summary
			};
		}

		public Task Create(WeatherForecastDetails details)
		{
			var entity = new WeatherForecast()
			{
				Date = details.Date,
				TemperatureC = details.TemperatureC,
				Summary = details.Summary
			};
			return _repository.CreateAsync(entity);
		}

		public Task Update(WeatherForecastDetails details)
		{
			var entity = new WeatherForecast()
			{
				Id = details.Id,
				Date = details.Date,
				TemperatureC = details.TemperatureC,
				Summary = details.Summary
			};
			return _repository.UpdateAsync(entity);
		}

		public Task Delete(int id)
		{
			return _repository.DeleteAsync(id);
		}
	}
}
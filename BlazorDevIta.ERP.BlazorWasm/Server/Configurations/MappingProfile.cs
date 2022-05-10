using AutoMapper;
using BlazorDevIta.ERP.Business.Data;
using BlazorDevIta.ERP.Shared;

namespace BlazorDevIta.ERP.BlazorWasm.Server.Configurations
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<WeatherForecast, WeatherForecastListItem>();

            CreateMap<WeatherForecast, WeatherForecastDetails>()
                .ReverseMap();
        }
    }
}

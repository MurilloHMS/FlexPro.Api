using AutoMapper;
using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Veiculo, VeiculoDTO>()
                .ForMember(dest => dest.ConsumoUrbanoAlcool, opt => opt.MapFrom(src => src.ConsumoUrbanoAlcool))
                .ForMember(dest => dest.ConsumoUrbanoGasolina, opt => opt.MapFrom(src => src.ConsumoUrbanoGasolina))
                .ForMember(dest => dest.ConsumoRodoviarioAlcool, opt => opt.MapFrom(src => src.ConsumoRodoviarioAlcool))
                .ForMember(dest => dest.ConsumoRodoviarioGasolina, opt => opt.MapFrom(src => src.ConsumoRodoviarioGasolina));
        }
    }
}

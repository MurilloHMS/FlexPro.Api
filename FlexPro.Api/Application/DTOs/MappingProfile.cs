using AutoMapper;
using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Veiculo, VeiculoDTO>().ReverseMap();
        }
    }
}

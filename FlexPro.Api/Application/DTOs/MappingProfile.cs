using AutoMapper;
using FlexPro.Domain.Entities;

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

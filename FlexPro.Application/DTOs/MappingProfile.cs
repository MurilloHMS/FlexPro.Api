using AutoMapper;
using FlexPro.Application.DTOs;
using FlexPro.Domain.Entities;

namespace FlexPro.Application.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Veiculo, VeiculoDTO>().ReverseMap();
        }
    }
}

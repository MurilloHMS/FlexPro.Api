using AutoMapper;
using FlexPro.Api.Application.Commands;
using FlexPro.Api.Application.DTOs;
using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Mappings
{
    public class VeiculoProfile : Profile
    {
        public VeiculoProfile()
        {
            CreateMap<Veiculo, VeiculoDTO>().ReverseMap();
            CreateMap<CreateVeiculoCommand, Veiculo>();
            CreateMap<UpdateVeiculoCommand, Veiculo>();
        }
    }
}

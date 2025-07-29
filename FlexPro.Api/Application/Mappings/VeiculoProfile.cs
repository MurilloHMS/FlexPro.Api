using AutoMapper;
using FlexPro.Api.Application.Commands.Veiculo;
using FlexPro.Api.Application.DTOs;
using FlexPro.Domain.Entities;

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

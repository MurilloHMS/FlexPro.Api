using AutoMapper;
using FlexPro.Application.DTOs;
using FlexPro.Domain.Entities;

namespace FlexPro.Application.Mappings;

public class VeiculoProfile : Profile
{
    public VeiculoProfile()
    {
        CreateMap<Veiculo, VehicleDto>().ReverseMap();
    }
}
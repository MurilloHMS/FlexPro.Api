using AutoMapper;
using FlexPro.Application.DTOs;
using FlexPro.Application.DTOs.Vehicle;
using FlexPro.Domain.Entities;

namespace FlexPro.Application.Mappings;

public class VehicleProfile : Profile
{
    public VehicleProfile()
    {
        CreateMap<Vehicle, VehicleResponseDto>();
        CreateMap<VehicleRequestDto, Vehicle>();
    }
}
using AutoMapper;
using FlexPro.Application.DTOs.Computer;
using FlexPro.Domain.Entities;

namespace FlexPro.Application.Mappings;

public class ComputerProfile : Profile
{
    public ComputerProfile()
    {
        CreateMap<ComputerRequestDto, Computador>();
        CreateMap<ComputerRequestDto, ComputerResponseDto>();
    }
}
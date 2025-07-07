using AutoMapper;
using FlexPro.Api.Application.DTOs.Computer;
using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Mappings;

public class ComputerProfile : Profile
{
    public ComputerProfile()
    {
        CreateMap<ComputerRequestDTO, Computador>();
        CreateMap<ComputerRequestDTO, ComputerResponseDTO>();
    }
}
using AutoMapper;
using FlexPro.Api.Application.DTOs.Cliente;
using FlexPro.Api.Application.DTOs.Parceiro;
using FlexPro.Domain.Entities;

namespace FlexPro.Api.Application.Mappings;

public class ParceiroProfile : Profile
{
    public ParceiroProfile()
    {
        CreateMap<ParceiroRequestDTO, Parceiro>();
        CreateMap<Parceiro, ParceiroResponseDTO>();
    }
}
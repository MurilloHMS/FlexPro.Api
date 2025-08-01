using AutoMapper;
using FlexPro.Application.DTOs.Parceiro;
using FlexPro.Domain.Entities;

namespace FlexPro.Application.Mappings;

public class ParceiroProfile : Profile
{
    public ParceiroProfile()
    {
        CreateMap<ParceiroRequestDto, Parceiro>();
        CreateMap<Parceiro, ParceiroResponseDto>();
    }
}
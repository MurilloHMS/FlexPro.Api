using AutoMapper;
using FlexPro.Api.Application.DTOs;
using FlexPro.Application.DTOs;
using FlexPro.Domain.Entities;

namespace FlexPro.Application.Mappings;

public class AbastecimentoProfile : Profile
{
    public AbastecimentoProfile()
    {
        CreateMap<Abastecimento, AbastecimentoDto>().ReverseMap();
    }
}
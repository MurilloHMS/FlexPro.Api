using AutoMapper;
using FlexPro.Api.Application.Commands.Abastecimento;
using FlexPro.Api.Application.DTOs;
using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Mappings;

public class AbastecimentoProfile : Profile
{
    public AbastecimentoProfile()
    {
        CreateMap<Abastecimento, AbastecimentoDTO>().ReverseMap();
        CreateMap<CreateAbastecimentoCommand, Abastecimento>();
        CreateMap<UpdateAbastecimentoCommand, Abastecimento>();
    }
}
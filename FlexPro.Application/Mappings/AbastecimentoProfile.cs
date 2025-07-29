using AutoMapper;
using FlexPro.Api.Application.Commands.Abastecimento;
using FlexPro.Api.Application.DTOs;
using FlexPro.Domain.Entities;

namespace FlexPro.Application.Mappings;

public class AbastecimentoProfile : Profile
{
    public AbastecimentoProfile()
    {
        CreateMap<Abastecimento, AbastecimentoDTO>().ReverseMap();
        CreateMap<CreateAbastecimentoCommand, Abastecimento>();
        CreateMap<UpdateAbastecimentoCommand, Abastecimento>();
    }
}
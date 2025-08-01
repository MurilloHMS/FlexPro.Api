using AutoMapper;
using FlexPro.Application.DTOs.Contato;
using FlexPro.Domain.Entities;

namespace FlexPro.Application.Mappings;

public class ContatoProfile : Profile
{
    public ContatoProfile()
    {
        CreateMap<ContatoRequestDto, Contato>();
        CreateMap<Contato, ContatoResponseDto>();
    }
}
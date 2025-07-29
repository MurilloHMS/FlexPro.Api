using AutoMapper;
using FlexPro.Api.Application.DTOs.Contato;
using FlexPro.Domain.Entities;

namespace FlexPro.Api.Application.Mappings;

public class ContatoProfile : Profile
{
    public ContatoProfile()
    {
        CreateMap<ContatoRequestDTO, Contato>();
        CreateMap<Contato, ContatoResponseDTO>();
    }
}
using AutoMapper;
using FlexPro.Api.Application.DTOs.Categoria;
using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Mappings;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<Categoria, CategoriaResponseDTO>();
        CreateMap<CategoriaRequestDTO, Categoria>();
    }
}
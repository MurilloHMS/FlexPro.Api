using AutoMapper;
using FlexPro.Application.DTOs.Categoria;
using FlexPro.Domain.Entities;

namespace FlexPro.Application.Mappings;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<Categoria, CategoriaResponseDto>();
        CreateMap<CategoriaRequestDto, Categoria>();
    }
}
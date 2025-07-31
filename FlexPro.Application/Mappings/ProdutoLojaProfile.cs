using AutoMapper;
using FlexPro.Application.DTOs.ProdutoLoja;
using FlexPro.Domain.Entities;

namespace FlexPro.Api.Application.Mappings;

public class ProdutoLojaProfile :  Profile
{
    public ProdutoLojaProfile()
    {
        // Request DTO → Entity
        CreateMap<ProdutoLojaRequestDto, ProdutoLoja>();
        CreateMap<EmbalagemRequestDto, Embalagem>();

        // Entity → Response DTO
        CreateMap<ProdutoLoja, ProdutoLojaResponseDto>();
        CreateMap<Embalagem, EmbalagemResponseDto>();

        CreateMap<DepartamentoRequestDto, Departamento>();
        CreateMap<Departamento, DepartamentoResponseDto>();
    }
}
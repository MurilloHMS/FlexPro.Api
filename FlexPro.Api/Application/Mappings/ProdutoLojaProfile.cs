using AutoMapper;
using FlexPro.Api.Application.DTOs.ProdutoLoja;
using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Mappings;

public class ProdutoLojaProfile :  Profile
{
    public ProdutoLojaProfile()
    {
        // Request DTO → Entity
        CreateMap<ProdutoLojaRequestDTO, ProdutoLoja>();
        CreateMap<EmbalagemRequestDTO, Embalagem>();

        // Entity → Response DTO
        CreateMap<ProdutoLoja, ProdutoLojaResponseDTO>();
        CreateMap<Embalagem, EmbalagemResponseDTO>();

        CreateMap<DepartamentoRequestDTO, Departamento>();
        CreateMap<Departamento, DepartamentoResponseDTO>();
    }
}
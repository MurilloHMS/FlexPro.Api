using AutoMapper;
using FlexPro.Application.DTOs.FuelSupply;
using FlexPro.Domain.Entities;

namespace FlexPro.Application.Mappings;

public class AbastecimentoProfile : Profile
{
    public AbastecimentoProfile()
    {
        CreateMap<Abastecimento, FuelSupplyResponse>();
        CreateMap<FuelSupplyRequest, Abastecimento>();
    }
}
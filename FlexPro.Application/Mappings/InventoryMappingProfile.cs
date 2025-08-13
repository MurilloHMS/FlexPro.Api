using AutoMapper;
using FlexPro.Application.DTOs.Inventory;
using FlexPro.Domain.Entities;

namespace FlexPro.Application.Mappings;

public class InventoryMappingProfile : Profile
{
    public InventoryMappingProfile()
    {
        CreateMap<InventoryProducts, InventoryProductDto>()
            .ForMember(dest => dest.MinimumStock, opt => opt.MapFrom(src => src.MinimumStock))
            .ForMember(dest => dest.Movements, opt => opt.MapFrom(src => src.Movements));

        CreateMap<InventoryMovement, InventoryMovementDto>();
    }
}
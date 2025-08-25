using AutoMapper;
using FlexPro.Application.DTOs.Inventory;
using FlexPro.Infrastructure.Repositories;
using MediatR;

namespace FlexPro.Application.UseCases.Inventory.GetAllProducts;

public class GetAllInventoryProductsHandler(InventoryRepository repository, IMapper mapper) : IRequestHandler<GetAllInventoryProductsQuery, List<InventoryProductDto>>
{
    public async Task<List<InventoryProductDto>> Handle(GetAllInventoryProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await repository.GetAllAsync();
        return mapper.Map<List<InventoryProductDto>>(products);
    }
}
using FlexPro.Application.DTOs.Inventory;
using MediatR;

namespace FlexPro.Application.UseCases.Inventory.GetAllProducts;

public record GetAllInventoryProductsQuery : IRequest<List<InventoryProductDto>>;
using FlexPro.Application.DTOs.Inventory;
using FlexPro.Application.UseCases.Inventory.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<InventoryProductDto>>> GetAllProducts()
    {
        var result = await mediator.Send(new GetAllInventoryProductsQuery());
        return Ok(result);
    }
}
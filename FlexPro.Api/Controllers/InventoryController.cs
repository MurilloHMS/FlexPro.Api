using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController
{
    private readonly IMediator _mediator;

    public InventoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
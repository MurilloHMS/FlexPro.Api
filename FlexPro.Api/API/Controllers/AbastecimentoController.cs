using FlexPro.Api.Application.Queries.Abastecimento;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AbastecimentoController : ControllerBase
{
    private readonly IMediator _mediator;

    public AbastecimentoController(IMediator mediator)
    {
        _mediator = mediator;           
    }

    [HttpGet]
    public async Task<ActionResult> Getall()
    {
        var result = await _mediator.Send(new GetAllAbastecimentoQuery());
        return Ok(result);
    }
}
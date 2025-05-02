using FlexPro.Api.Application.Commands.CalculoTransportadora;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalculoTransportadorasController : ControllerBase
{
    private readonly Mediator _mediator;

    public CalculoTransportadorasController(Mediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("calcular")]
    public async Task<IActionResult> CalcularAlfaTransportes(List<IFormFile> files)
    {
        var response = await _mediator.Send(new CalcularDadosTransportadoraCommand(files));
        return Ok(response);
    }
}
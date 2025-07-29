using FlexPro.Api.Application.Commands.Informativo;
using FlexPro.Api.Application.DTOs.Informativo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InformativoController : ControllerBase
{
    private readonly IMediator _mediator;

    public InformativoController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("upload/nfe")]
    public async Task<IActionResult> UploadNfeData(IFormFile file)
    {
        var response = await _mediator.Send(new UploadDadosNfeCommand(file));
        return response;
    }

    [HttpPost("upload/os")]
    public async Task<IActionResult> UploadOsData(IFormFile file)
    {
        var response = await _mediator.Send(new UploadDadosOsCommand(file));
        return response;      
    }

    [HttpPost("upload/pecasTrocadas")]
    public async Task<IActionResult> UploadPecasTrocadas(IFormFile file)
    {
        var response = await _mediator.Send(new UploadDadosPecasTrocadasCommand(file));
        return response;  
    }

    [HttpPost("calcular")]
    public async Task<IActionResult> GenerateMetrics([FromBody]InformativoRequestDTO dados)
    {
        var response = await _mediator.Send(new CalcularDadosInformativoCommand(dados));
        return response;
    }
}
using AutoMapper;
using FlexPro.Api.Application.Commands.Parceiro;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParceiroController : ControllerBase
{
    private readonly IMediator _mediator;

    public ParceiroController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var request = await _mediator.Send(new CreateParceiroListBySheetCommand(file));
        return request != null ? Ok("Parceiros criados com sucesso") : BadRequest("Ocorreu um erro ao criar a lista de parceiros");
    }
}
using FlexPro.Api.Application.Commands.Contato;
using FlexPro.Api.Application.DTOs.Contato;
using FlexPro.Api.Application.Queries.Contato;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContatoController :  ControllerBase
{
    private readonly IMediator _mediator;

    public ContatoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllContato()
    {
        var contatos = await _mediator.Send(new GetAllContatoQuery());
        return contatos.Any() ? Ok(contatos) : NotFound();
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> CreateContato([FromBody] ContatoRequestDTO request)
    {
        var contato = await _mediator.Send(new CreateContatoCommand(request));
        return contato != null ? Ok("Em Breve um de nossos consultores entrará em contato!") : BadRequest("Ocorreu um erro ao solicitar o contato. Por favor utilize um dos contatos ao lado.");
    }
}
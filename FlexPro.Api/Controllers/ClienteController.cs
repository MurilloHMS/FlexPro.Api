using FlexPro.Api.Application.Commands.Cliente;
using FlexPro.Api.Application.Queries.Cliente;
using FlexPro.Application.DTOs.Cliente;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClienteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClienteRequestDto dto)
    {
        var result = await _mediator.Send(new CreateClienteCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ClienteRequestDto dto)
    {
        await _mediator.Send(new UpdateClienteCommand(id, dto));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(ClienteRequestDto cliente)
    {
        await _mediator.Send(new DeleteClienteCommand(cliente));
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var cliente = await _mediator.Send(new GetClienteByIdQuery(id));
        return Ok(cliente);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<ClienteResponseDto> response = await _mediator.Send(new GetAllClienteQuery());
        return response != null ? Ok(response) : NotFound();
    }
}
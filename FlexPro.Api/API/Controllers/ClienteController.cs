using FlexPro.Api.Application.Commands.Cliente;
using FlexPro.Api.Application.DTOs.Cliente;
using FlexPro.Api.Application.Queries.Cliente;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.API.Controllers;

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
    public async Task<IActionResult> Create([FromBody] ClienteRequestDTO dto)
    {
        var result = await _mediator.Send(new CreateClienteCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id,[FromBody] ClienteRequestDTO dto)
    {
        await _mediator.Send(new UpdateClienteCommand(id, dto));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteClienteCommand(id));
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
        IEnumerable<ClienteResponseDTO> response = await _mediator.Send(new GetAllClienteQuery());
        return response != null ? Ok(response) : NotFound();
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromBody] IFormFile file)
    {
        var request = await _mediator.Send(new CreateClienteListBySheetsCommand(file));
        return request != null ? Ok("Clientes criados com sucesso") : BadRequest("Ocorreu um erro ao criar a lista de clientes");
    }
}
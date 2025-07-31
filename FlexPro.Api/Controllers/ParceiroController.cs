using FlexPro.Api.Application.Commands.Parceiro;
using FlexPro.Api.Application.Queries.Parceiro;
using FlexPro.Application.DTOs.Parceiro;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Controllers;

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
        return request != null
            ? Ok("Parceiros criados com sucesso")
            : BadRequest("Ocorreu um erro ao criar a lista de parceiros");
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(ParceiroRequestDto dto)
    {
        var request = await _mediator.Send(new CreateParceiroCommand(dto));
        return request;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, ParceiroRequestDto dto)
    {
        var request = await _mediator.Send(new UpdateParceiroCommand(dto, id));
        return request;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var request = await _mediator.Send(new GetAllParceiroQuery());
        return Ok(request);
    }
}
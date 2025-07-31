using FlexPro.Api.Application.Commands.Computer;
using FlexPro.Api.Application.Queries.Computer;
using FlexPro.Application.DTOs.Computer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComputerController : ControllerBase
{
    private readonly IMediator _mediator;

    public ComputerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> PostComputerAsync([FromBody] ComputerRequestDto dto)
    {
        var response = await _mediator.Send(new CreateComputerCommand(dto));
        return response;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllComputerAsync()
    {
        var response = await _mediator.Send(new GetAllComputerQuery());
        return response;
    }
    
}
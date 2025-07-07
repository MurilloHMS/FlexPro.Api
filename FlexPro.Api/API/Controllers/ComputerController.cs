using FlexPro.Api.Application.Commands.Computer;
using FlexPro.Api.Application.DTOs.Computer;
using FlexPro.Api.Application.Queries.Computer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.API.Controllers;

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
    public async Task<IActionResult> PostComputerAsync([FromBody] ComputerRequestDTO dto)
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
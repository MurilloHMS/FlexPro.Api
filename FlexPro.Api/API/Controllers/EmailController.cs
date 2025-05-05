using FlexPro.Api.Application.Commands.Email;
using FlexPro.Api.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmailController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendEmailAsync(Email emailData)
    {
        var result = await _mediator.Send(new SendEmailCommand(emailData));
        return result;
    }
    
    [HttpPost("send/informativos")]
    public async Task<IActionResult> SendInformativosAsync(IEnumerable<Informativo> informativos)
    {
        var result = await _mediator.Send(new SendInformativoCommand(informativos));
        return result;
    }
}
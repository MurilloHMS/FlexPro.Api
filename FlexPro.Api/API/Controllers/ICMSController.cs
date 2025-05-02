using FlexPro.Api.Application.Commands.CalculoICMS;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ICMSController : ControllerBase
{
    private readonly IMediator _mediator;

    public ICMSController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("calcular")]
    public async Task<IActionResult> CalcularICMS(List<IFormFile> files)
    {
        var memoryStream = await _mediator.Send(new CalculoIcmsCommand(files));
        return File(memoryStream,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ICMS-{DateTime.Now:dd-MM-yyyy}.xlsx");
    }
}
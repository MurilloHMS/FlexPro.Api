using FlexPro.Application.UseCases.Nfe.CalculateIcms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IcmsController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("calcular")]
    public async Task<IActionResult> CalcularIcms(List<IFormFile> files)
    {
        var command = new CalculateIcmsCommand(files);
        var result = await mediator.Send(command);
        return File(result.FileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"ICMS-{DateTime.Now:dd-MM-yyyy}.xlsx");
    }
}
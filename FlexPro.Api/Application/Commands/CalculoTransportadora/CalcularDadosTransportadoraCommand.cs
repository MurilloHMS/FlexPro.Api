using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.CalculoTransportadora;

public class CalcularDadosTransportadoraCommand : IRequest<IActionResult>
{
    public CalcularDadosTransportadoraCommand(List<IFormFile> files)
    {
        Files = files;
    }

    public List<IFormFile> Files { get; }
}
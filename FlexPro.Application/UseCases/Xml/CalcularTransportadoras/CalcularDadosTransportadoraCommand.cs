using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Xml.CalcularTransportadoras;

public class CalcularDadosTransportadoraCommand : IRequest<IActionResult>
{
    public CalcularDadosTransportadoraCommand(List<IFormFile> files)
    {
        Files = files;
    }

    public List<IFormFile> Files { get; }
}
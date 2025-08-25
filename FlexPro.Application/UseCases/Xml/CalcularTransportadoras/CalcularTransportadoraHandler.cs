using FlexPro.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Xml.CalcularTransportadoras;

public class CalcularTransportadoraHandler : IRequestHandler<CalcularDadosTransportadoraCommand, IActionResult>
{
    private readonly ICalculoTransportadoraService _service;

    public CalcularTransportadoraHandler(ICalculoTransportadoraService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Handle(CalcularDadosTransportadoraCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _service.CalcularAsync(request.Files);
        return new OkObjectResult(response);
    }
}
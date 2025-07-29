using FlexPro.Api.Application.Commands.Informativo;
using FlexPro.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.Informativo;

public class CalcularDadosInformativoHandler : IRequestHandler<CalcularDadosInformativoCommand, IActionResult>
{
    private readonly InformativoService _service;

    public CalcularDadosInformativoHandler(InformativoService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Handle(CalcularDadosInformativoCommand request,
        CancellationToken cancellationToken)
    {
        if( request.informativoRequest == null) return new BadRequestObjectResult("Dados não foram enviados corretamente");

        IEnumerable<FlexPro.Domain.Models.Informativo> informativos = await _service.CreateInfoData(request.informativoRequest.InformativoNFes, request.informativoRequest.informativoOs, request.informativoRequest.InformativoPecasTrocadas, request.informativoRequest.Month);

        return informativos.Any()
            ? new OkObjectResult(informativos)
            : new BadRequestObjectResult("Não foi possivel obter os dados");
    }
}
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
        if (request.InformativoRequest == null)
            return new BadRequestObjectResult("Dados não foram enviados corretamente");

        var informativos =
            await _service.CreateInfoData(request.InformativoRequest.InformativoNFes,
                request.InformativoRequest.informativoOs, request.InformativoRequest.InformativoPecasTrocadas,
                request.InformativoRequest.Month);

        return informativos.Any()
            ? new OkObjectResult(informativos)
            : new BadRequestObjectResult("Não foi possivel obter os dados");
    }
}
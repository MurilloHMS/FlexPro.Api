using FlexPro.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Newsletter.Calculate;

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
        var informativos =
            await _service.CreateInfoData(request.InformativoRequest.InformativoNFes,
                request.InformativoRequest.InformativoOs, request.InformativoRequest.InformativoPecasTrocadas,
                request.InformativoRequest.Month);

        return informativos.Any()
            ? new OkObjectResult(informativos)
            : new BadRequestObjectResult("Não foi possivel obter os dados");
    }
}
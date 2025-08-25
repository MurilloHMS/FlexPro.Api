using FlexPro.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.FuelSupply.GetGeneralMetrics;

public class GetGeralMetricsHandler : IRequestHandler<GetGeralMetricsQuery, IActionResult>
{
    private readonly AbastecimentoService _abastecimentoService;

    public GetGeralMetricsHandler(AbastecimentoService abastecimentoService)
    {
        _abastecimentoService = abastecimentoService;
    }

    public async Task<IActionResult> Handle(GetGeralMetricsQuery request, CancellationToken cancellationToken)
    {
        var retorno = await _abastecimentoService.CalcularAbastecimentoGeral(request.Data);
        return retorno != null! ? new OkObjectResult(retorno) : new NotFoundResult();
    }
}
using FlexPro.Api.Application.Queries.Abastecimento;
using FlexPro.Api.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.Abastecimento;

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
        return retorno != null ? new OkObjectResult(retorno) : new NotFoundResult();
    }
}
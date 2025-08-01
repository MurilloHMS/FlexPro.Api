using FlexPro.Api.Application.Queries.Abastecimento;
using FlexPro.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.Abastecimento;

public class GetSetorMetricsHandler : IRequestHandler<GetSetorMetricsQuery, IActionResult>
{
    private readonly AbastecimentoService _abastecimentoService;

    public GetSetorMetricsHandler(AbastecimentoService service)
    {
        _abastecimentoService = service;
    }

    public async Task<IActionResult> Handle(GetSetorMetricsQuery request, CancellationToken cancellationToken)
    {
        var retorno = await _abastecimentoService.CalcularAbastecimentoSetor(request.Date);
        return retorno != null! ? new OkObjectResult(retorno) : new NotFoundResult();
    }
}
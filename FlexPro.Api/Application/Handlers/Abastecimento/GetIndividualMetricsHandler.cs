using FlexPro.Api.Application.Queries.Abastecimento;
using FlexPro.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Handlers.Abastecimento
{
    public class GetIndividualMetricsHandler : IRequestHandler<GetIndividualMetricsQuery, IActionResult>
    {
        private readonly AbastecimentoService _service;

        
        public GetIndividualMetricsHandler(AbastecimentoService service)
        {
            _service = service;
        }

        
        public async Task<IActionResult> Handle(GetIndividualMetricsQuery request, CancellationToken cancellationToken)
        {
            var retorno = await _service.CalcularAbastecimentoIndividual(request.Date);
            return retorno != null ? new OkObjectResult(retorno) : new NotFoundResult();
        }
    }
}
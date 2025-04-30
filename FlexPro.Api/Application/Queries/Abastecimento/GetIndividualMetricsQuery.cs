using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Queries.Abastecimento;

public class GetIndividualMetricsQuery : IRequest<IActionResult>
{
    public DateTime Date { get; }
    
    public GetIndividualMetricsQuery(DateTime date)
    {
        Date = date;
    }
}
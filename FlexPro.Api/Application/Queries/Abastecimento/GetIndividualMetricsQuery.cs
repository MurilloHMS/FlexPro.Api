using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Queries.Abastecimento;

public class GetIndividualMetricsQuery : IRequest<IActionResult>
{
    public GetIndividualMetricsQuery(DateTime date)
    {
        Date = date;
    }

    public DateTime Date { get; }
}
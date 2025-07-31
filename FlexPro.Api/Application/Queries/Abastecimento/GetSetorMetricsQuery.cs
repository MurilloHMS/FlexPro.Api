using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Queries.Abastecimento;

public class GetSetorMetricsQuery : IRequest<IActionResult>
{
    public GetSetorMetricsQuery(DateTime date)
    {
        Date = date;
    }

    public DateTime Date { get; }
}
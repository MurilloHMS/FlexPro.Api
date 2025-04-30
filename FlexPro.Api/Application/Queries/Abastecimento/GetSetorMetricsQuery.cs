using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Queries.Abastecimento;

public class GetSetorMetricsQuery : IRequest<IActionResult>
{
    public DateTime Date { get; }
    
    public GetSetorMetricsQuery(DateTime date)
    {
        Date = date;
    }
}
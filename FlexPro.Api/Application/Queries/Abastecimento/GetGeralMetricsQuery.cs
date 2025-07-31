using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Queries.Abastecimento;

public class GetGeralMetricsQuery : IRequest<IActionResult>
{
    public GetGeralMetricsQuery(DateTime data)
    {
        Data = data;
    }

    public DateTime Data { get; set; }
}
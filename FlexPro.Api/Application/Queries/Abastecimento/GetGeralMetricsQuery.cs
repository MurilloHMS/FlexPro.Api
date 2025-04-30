using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Queries.Abastecimento;

public class GetGeralMetricsQuery : IRequest<IActionResult>
{
    public DateTime Data { get; set; }

    public GetGeralMetricsQuery(DateTime data)
    {
        Data = data;
    }
}
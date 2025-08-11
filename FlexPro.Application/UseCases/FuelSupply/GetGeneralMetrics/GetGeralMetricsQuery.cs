using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.FuelSupply.GetGeneralMetrics;

public sealed record GetGeralMetricsQuery(DateTime Data) : IRequest<IActionResult>;
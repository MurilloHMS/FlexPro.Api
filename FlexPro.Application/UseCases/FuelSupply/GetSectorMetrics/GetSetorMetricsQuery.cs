using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.FuelSupply.GetSectorMetrics;

public sealed record GetSetorMetricsQuery(DateTime Date) : IRequest<IActionResult>;
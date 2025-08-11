using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.FuelSupply.GetIndividualMetrics;

public sealed record GetIndividualMetricsQuery(DateTime Date) : IRequest<IActionResult>;
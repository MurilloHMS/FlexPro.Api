using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Queries.Computer;

public record GetAllComputerQuery : IRequest<IActionResult>;
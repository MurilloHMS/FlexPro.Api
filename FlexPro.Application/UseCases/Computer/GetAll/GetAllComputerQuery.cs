using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Computer.GetAll;

public record GetAllComputerQuery : IRequest<IActionResult>;
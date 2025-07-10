using FlexPro.Api.Application.DTOs.Computer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Computer;

public record CreateComputerCommand(ComputerRequestDTO dto) : IRequest<IActionResult>;

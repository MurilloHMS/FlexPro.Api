using FlexPro.Application.DTOs.Computer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Computer.Create;

public record CreateComputerCommand(ComputerRequestDto Dto) : IRequest<IActionResult>;
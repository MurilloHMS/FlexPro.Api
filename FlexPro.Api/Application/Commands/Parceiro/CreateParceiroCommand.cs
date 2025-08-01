using FlexPro.Application.DTOs.Parceiro;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Parceiro;

public record CreateParceiroCommand(ParceiroRequestDto Dto) : IRequest<IActionResult>
{
}
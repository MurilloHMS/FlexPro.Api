using FlexPro.Application.DTOs.Parceiro;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Partners.Create;

public record CreateParceiroCommand(ParceiroRequestDto Dto) : IRequest<IActionResult>
{
}
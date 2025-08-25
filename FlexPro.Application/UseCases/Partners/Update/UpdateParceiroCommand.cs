using FlexPro.Application.DTOs.Parceiro;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Partners.Update;

public record UpdateParceiroCommand(ParceiroRequestDto Dto, int Id) : IRequest<IActionResult>
{
}
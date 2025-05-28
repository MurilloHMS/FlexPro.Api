using FlexPro.Api.Application.DTOs.Parceiro;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Parceiro;

public record UpdateParceiroCommand(ParceiroRequestDTO dto, int id) :  IRequest<IActionResult> { }
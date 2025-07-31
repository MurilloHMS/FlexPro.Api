using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Parceiro;

public record CreateParceiroListBySheetCommand(IFormFile File) : IRequest<IActionResult>;
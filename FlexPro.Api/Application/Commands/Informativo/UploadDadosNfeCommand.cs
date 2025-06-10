using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Informativo;

public record UploadDadosNfeCommand(IFormFile file) : IRequest<IActionResult>;
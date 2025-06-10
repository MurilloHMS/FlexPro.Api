using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Informativo;

public record UploadDadosOsCommand(IFormFile file) : IRequest<IActionResult>;
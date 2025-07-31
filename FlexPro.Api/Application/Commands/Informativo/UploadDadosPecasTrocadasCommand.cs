using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Informativo;

public record UploadDadosPecasTrocadasCommand(IFormFile File) : IRequest<IActionResult>;
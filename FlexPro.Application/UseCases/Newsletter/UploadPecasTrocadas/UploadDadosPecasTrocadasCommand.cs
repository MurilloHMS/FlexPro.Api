using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Newsletter.UploadPecasTrocadas;

public record UploadDadosPecasTrocadasCommand(IFormFile File) : IRequest<IActionResult>;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Newsletter.UploadNfeData;

public record UploadDadosNfeCommand(IFormFile File) : IRequest<IActionResult>;
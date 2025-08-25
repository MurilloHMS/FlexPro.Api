using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Newsletter.UploadOsData;

public record UploadDadosOsCommand(IFormFile File) : IRequest<IActionResult>;
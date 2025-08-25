using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.FuelSupply.Upload;

public sealed record UploadAbastecimentoCommand(IFormFile File) : IRequest<IActionResult>;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Partners.CreateBySheet;

public record CreateParceiroListBySheetCommand(IFormFile File) : IRequest<IActionResult>;
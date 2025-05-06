using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Cliente;

public record CreateClienteListBySheetsCommand(IFormFile file) : IRequest<IActionResult>;
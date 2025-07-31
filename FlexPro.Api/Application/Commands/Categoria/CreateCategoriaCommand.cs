using FlexPro.Api.Application.DTOs.Categoria;
using FlexPro.Application.DTOs.Categoria;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Categoria;

public record CreateCategoriaCommand(CategoriaRequestDto Dto) : IRequest<IActionResult>;
using FlexPro.Api.Application.DTOs.Categoria;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.Categoria;

public record CreateCategoriaCommand(CategoriaRequestDTO dto) : IRequest<IActionResult>;
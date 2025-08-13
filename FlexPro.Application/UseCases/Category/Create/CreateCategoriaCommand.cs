using FlexPro.Application.DTOs.Categoria;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.Category.Create;

public record CreateCategoriaCommand(CategoriaRequestDto Dto) : IRequest<IActionResult>;
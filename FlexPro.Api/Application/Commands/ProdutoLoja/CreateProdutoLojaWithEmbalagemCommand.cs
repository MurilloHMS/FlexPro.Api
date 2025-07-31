using FlexPro.Application.DTOs.ProdutoLoja;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.ProdutoLoja;

public record CreateProdutoLojaWithEmbalagemCommand(ProdutoLojaRequestDto Dto) : IRequest<IActionResult>;
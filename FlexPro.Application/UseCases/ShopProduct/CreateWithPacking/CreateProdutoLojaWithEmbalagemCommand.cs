using FlexPro.Application.DTOs.ProdutoLoja;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.ShopProduct.CreateWithPacking;

public record CreateProdutoLojaWithEmbalagemCommand(ProdutoLojaRequestDto Dto) : IRequest<IActionResult>;
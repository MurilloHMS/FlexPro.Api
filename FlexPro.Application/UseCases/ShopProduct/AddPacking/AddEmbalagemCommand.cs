using FlexPro.Application.DTOs.ProdutoLoja;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.ShopProduct.AddPacking;

public record AddEmbalagemCommand(int ProdutoLojaId, List<EmbalagemRequestDto> Embalagens) : IRequest<IActionResult>;
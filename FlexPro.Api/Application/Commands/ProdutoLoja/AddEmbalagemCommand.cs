using FlexPro.Api.Application.DTOs.ProdutoLoja;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.ProdutoLoja;

public record AddEmbalagemCommand(int ProdutoLojaId, List<EmbalagemRequestDTO> Embalagens) : IRequest<IActionResult>;
using FlexPro.Api.Application.DTOs.ProdutoLoja;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.ProdutoLoja;

public record AddDepartamentoCommand(int produtoLojaId, List<DepartamentoRequestDTO> departamentos) : IRequest<IActionResult>;
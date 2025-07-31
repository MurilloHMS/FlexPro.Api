using FlexPro.Api.Application.DTOs.ProdutoLoja;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Commands.ProdutoLoja;

public record AddDepartamentoCommand(int ProdutoLojaId, List<DepartamentoRequestDTO> Departamentos) : IRequest<IActionResult>;
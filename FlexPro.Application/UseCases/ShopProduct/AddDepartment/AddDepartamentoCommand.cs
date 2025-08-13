using FlexPro.Application.DTOs.ProdutoLoja;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Application.UseCases.ShopProduct.AddDepartment;

public record AddDepartamentoCommand(int ProdutoLojaId, List<DepartamentoRequestDto> Departamentos)
    : IRequest<IActionResult>;
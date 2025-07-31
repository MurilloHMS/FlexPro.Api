using FlexPro.Api.Application.Commands.ProdutoLoja;
using FlexPro.Api.Application.Queries.Produtos;
using FlexPro.Application.DTOs.ProdutoLoja;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProdutoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("ProdutoLoja")]
    public async Task<IActionResult> PostProdutoLoja([FromBody] ProdutoLojaRequestDto dto)
    {
        var response = await _mediator.Send(new CreateProdutoLojaWithEmbalagemCommand(dto));
        return response;
    }

    [HttpPost("ProdutoLoja/{id}/Embalagens")]
    public async Task<IActionResult> PostEmbalagens(int id, List<EmbalagemRequestDto> dto)
    {
        var response = await _mediator.Send(new AddEmbalagemCommand(id, dto));
        return response;
    }
    [AllowAnonymous]
    [HttpPost("ProdutoLoja/{id}/Departamentos")]
    public async Task<IActionResult> PostDepartamentos(int id, List<DepartamentoRequestDto> dto)
    {
        var response = await _mediator.Send(new AddDepartamentoCommand(id, dto));
        return response;
    }
    
    [AllowAnonymous]
    [HttpGet("ProdutoLoja")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllProductsQuery());
        return response.Any() ?  Ok(response) : NotFound();
    }
}
using FlexPro.Api.Application.Commands.ProdutoLoja;
using FlexPro.Api.Application.DTOs.ProdutoLoja;
using FlexPro.Api.Application.Queries.Produtos;
using FlexPro.Api.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.API.Controllers;

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
    public async Task<IActionResult> PostProdutoLoja([FromBody] ProdutoLojaRequestDTO dto)
    {
        var response = await _mediator.Send(new CreateProdutoLojaWithEmbalagemCommand(dto));
        return response;
    }

    [HttpPost("ProdutoLoja/{id}/Embalagens")]
    public async Task<IActionResult> PostEmbalagens(int id, List<EmbalagemRequestDTO> dto)
    {
        var response = await _mediator.Send(new AddEmbalagemCommand(id, dto));
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
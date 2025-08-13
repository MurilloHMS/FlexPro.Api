using FlexPro.Application.DTOs.Categoria;
using FlexPro.Application.UseCases.Category.Create;
using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMediator _mediator;

    public CategoriaController( ICategoriaRepository repository, IMediator mediator)
    {
        _categoriaRepository = repository;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
    {
        var category = await _categoriaRepository.GetAllAsync();
        return category == null ? NotFound() : Ok(category);
    }

    // TODO: Migrate getById to Mediator
    [HttpGet("{id}")]
    public async Task<ActionResult<Categoria>> GetCategoria(int id)
    {
        var category = await _categoriaRepository.GetByIdAsync(id);
        return category == null ? NotFound() : Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> PostCategoria(CategoriaRequestDto category)
    {
        var response = await _mediator.Send(new CreateCategoriaCommand(category));
        return response;
    }

    // TODO: Migrate Put category to Mediator
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(int id, Categoria category)
    {
        if (id != category.Id) return BadRequest();

        try
        {
            await _categoriaRepository.SaveOrUpdate(category);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _categoriaRepository.GetByIdAsync(id) == null) return NotFound();

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Categoria categoria)
    {
        await _categoriaRepository.DeleteAsync(categoria);
        return NoContent();
    }
}
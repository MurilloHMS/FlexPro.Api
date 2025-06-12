using FlexPro.Api.Application.Commands.Categoria;
using FlexPro.Api.Application.DTOs.Categoria;
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using FlexPro.Api.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly AppDbContext _context;
    private readonly IMediator _mediator;

    public CategoriaController(AppDbContext context, ICategoriaRepository repository,  IMediator mediator)
    {
        _context = context;
        _categoriaRepository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
    {
        var category = await _categoriaRepository.GetAllAsync();
        return category == null ? NotFound() : Ok(category);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Categoria>> GetCategoria(int id)
    {
        var category = await _categoriaRepository.GetByIdAsync(id);
        return category == null ? NotFound() : Ok(category);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostCategoria(CategoriaRequestDTO category)
    {
        var response = await _mediator.Send(new CreateCategoriaCommand(category));
        return response;
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(int id, Categoria category)
    {
        if(id != category.Id)
        {
            return BadRequest();
        }

        try
        {
            await _categoriaRepository.SaveOrUpdate(category);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _categoriaRepository.GetByIdAsync(id) == null)
            {
                return NotFound();
            }
            else
            {
                throw;
            }
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
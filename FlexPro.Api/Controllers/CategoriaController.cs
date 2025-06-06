using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using FlexPro.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly AppDbContext _context;

    public CategoriaController(AppDbContext context)
    {
        _context = context;
        _categoriaRepository = new CategoriaRepository(_context);
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
    public async Task<ActionResult<Categoria>> PostCategoria(Categoria category)
    {
        await _categoriaRepository.SaveOrUpdate(category);
        return CreatedAtAction(nameof(GetCategoria), new { id = category.Id }, category);
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
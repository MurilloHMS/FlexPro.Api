using FlexPro.Api.Data;
using FlexPro.Api.Interfaces;
using FlexPro.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly AppDbContext _context;

    public CategoriaController(ICategoriaRepository categoriaRepository, AppDbContext context)
    {
        _context = context;
        _categoriaRepository = categoriaRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
    {
        var category = await _categoriaRepository.GetAll();
        return category == null ? NotFound() : Ok(category);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Categoria>> GetCategoria(int id)
    {
        var category = await _categoriaRepository.GetById(id);
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
            if (await _categoriaRepository.GetById(id) == null)
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
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoriaRepository.Delete(id);
        return NoContent();
    }
    
}
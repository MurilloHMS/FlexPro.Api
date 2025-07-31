using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using FlexPro.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// TODO: Migrate this to Mediator Pattern
namespace FlexPro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReceitaController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IReceitaRepository _repository;

    public ReceitaController(AppDbContext context)
    {
        _context = context;
        _repository = new ReceitaRepository(_context);
    }

    [HttpGet]
    public async Task<ActionResult<List<Receita>>> GetReceita()
    {
        var receipt = await _repository.GetAll();
        return receipt == null ? NotFound() : Ok(receipt);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Receita>> GetReceita(int id)
    {
        var receipt = await _repository.GetById(id);
        return receipt == null ? NotFound() : Ok(receipt);
    }

    [HttpPost]
    public async Task<ActionResult> PostReceita(Receita receipt)
    {
        await _repository.SaveOrUpdate(receipt);
        return CreatedAtAction(nameof(GetReceita), new { id = receipt.Id }, receipt);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutReceita(int id, Receita receipt)
    {
        if (id != receipt.Id) return BadRequest();

        try
        {
            await _repository.SaveOrUpdate(receipt);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _repository.GetById(id) == null) return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteReceita(int id)
    {
        var receipt = await _repository.GetById(id);
        if (receipt == null) return NotFound();

        await _repository.Delete(receipt);
        return NoContent();
    }
}
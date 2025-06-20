using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using FlexPro.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// TODO: Migrate this to Mediator Pattern
namespace FlexPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntidadeController : ControllerBase
    {
        private readonly IEntidadeRepository _repository;
        private readonly AppDbContext _context;

        public EntidadeController(AppDbContext context)
        {
            _context = context;
            _repository = new EntidadeRepository(_context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entidade>>> GetEntidades()
        {
            var entities = await _repository.GetAllAsync();
            return entities == null ? NotFound() : Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Entidade>> GetEntidades(int id)
        {
            var entities = await _repository.GetByIdAsync(id);
            return entities == null ? NotFound() : Ok(entities);
        }

        [HttpPost]
        public async Task<ActionResult<Entidade>> PostEntidades(Entidade entities)
        {
            await _repository.InsertOrUpdateAsync(entities, x => x.Id == entities.Id);
            return CreatedAtAction(nameof(GetEntidades), new { id = entities.Id }, entities);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutEntidades(int id, Entidade entidade)
        {
            if (id != entidade.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.InsertOrUpdateAsync(entidade);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _repository.GetByIdAsync(id) == null)
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
        public async Task<ActionResult> DeleteEntidade(int id)
        {
            await _repository.DeleteById(id);
            return NoContent();
        }
    }
}

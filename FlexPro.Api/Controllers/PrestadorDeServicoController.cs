using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using FlexPro.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// TODO: Migrate this to Mediator Pattern
namespace FlexPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestadorDeServicoController : ControllerBase
    {
        private readonly IPrestadorDeServicoRepository _repository;
        private readonly AppDbContext _context;

        public PrestadorDeServicoController(AppDbContext context)
        {
            _context = context;
            _repository = new PrestadorDeServicoRepository(_context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrestadorDeServico>>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            return entities == null ? NotFound() : Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrestadorDeServico>> GetById(int id)
        {
            var entities = await _repository.GetByIdAsync(id);
            return entities == null ? NotFound() : Ok(entities);
        }

        [HttpPost]
        public async Task<ActionResult<PrestadorDeServico>> PostEntidades(PrestadorDeServico entities)
        {
            await _repository.InsertOrUpdateAsync(entities, x => x.Id == entities.Id);
            return CreatedAtAction(nameof(GetById), new { id = entities.Id }, entities);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutEntidades(int id, PrestadorDeServico entidade)
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
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteById(id);
            return NoContent();
        }
    }
}
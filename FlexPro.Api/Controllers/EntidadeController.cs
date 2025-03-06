using FlexPro.Api.Data;
using FlexPro.Api.Interfaces;
using FlexPro.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntidadeController : ControllerBase
    {
        private readonly IEntidadeRepository _repository;
        private readonly AppDbContext _context;

        public EntidadeController(IEntidadeRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entidade>>> GetEntidades()
        {
            var entities = await _repository.GetAll();
            return entities == null ? NotFound() : Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Entidade>> GetEntidades(int id)
        {
            var entities = await _repository.GetById(id);
            return entities == null ? NotFound() : Ok(entities);
        }

        [HttpPost]
        public async Task<ActionResult<Entidade>> PostEntidades(Entidade entities)
        {
            await _repository.SaveOrUpdate(entities);
            return CreatedAtAction(nameof(GetEntidades), new { id = entities.ID }, entities);
        }

        [HttpPut("id")]
        public async Task<ActionResult> PutEntidades(int id, Entidade entidade)
        {
            if (id != entidade.ID)
            {
                return BadRequest();
            }

            try
            {
                await _repository.SaveOrUpdate(entidade);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _repository.GetById(id) == null)
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

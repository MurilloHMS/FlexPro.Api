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
    public class RevisaoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRevisaoRepository _repository;

        public RevisaoController(AppDbContext context)
        {
            _context = context;
            _repository = new RevisaoRepository(_context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Revisao>>> GetRevisao()
        {
            var revisao = await _repository.GetAllAsync();
            return revisao == null ? NotFound() : Ok(revisao);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Revisao>> GetRevisao(int id)
        {
            var revisao = await _repository.GetByIdAsync(id);
            return revisao == null ? NotFound() : Ok(revisao);
        }

        [HttpGet("veiculo/{id}")]
        public async Task<ActionResult<Revisao>> GetRevisaoByVehicleId(int id)
        {
            var revisao = await _repository.GetByVehicleId(id);
            return revisao.Any() && revisao != null ? Ok(revisao) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Revisao>> PostRevisao(Revisao revisao)
        {
            await _repository.UpdateOrInsert(revisao);
            return CreatedAtAction(nameof(GetRevisao), new { id = revisao.Id }, revisao);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutRevisao(int id, Revisao revisao)
        {
            if (id != revisao.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateOrInsert(revisao);
            }
            catch (DbUpdateException)
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
        public async Task<ActionResult> DeleteRevisao(int id)
        {
            var revisao = await _repository.GetByIdAsync(id);
            if (revisao == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(revisao);
            return NoContent();
        }
    }
}
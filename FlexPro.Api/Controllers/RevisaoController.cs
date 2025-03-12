using FlexPro.Api.Data;
using FlexPro.Api.Interfaces;
using FlexPro.Api.Models;
using FlexPro.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var revisao = await _repository.GetAll();
            return revisao == null ? NotFound() : Ok(revisao);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Revisao>> GetRevisao(int id)
        {
            var revisao = await _repository.GetById(id);
            return revisao == null ? NotFound() : Ok(revisao);
        }

        [HttpPost]
        public async Task<ActionResult<Revisao>> PostRevisao(Revisao revisao)
        {
            await _repository.UpdateOrInsert(revisao);
            return CreatedAtAction(nameof(GetRevisao), new { id = revisao.Id }, revisao);
        }

        [HttpPut]
        public async Task<ActionResult> PutRevisao(Revisao revisao)
        {
            if (revisao == null) { return BadRequest(); }

            try
            {
                await _repository.UpdateOrInsert(revisao);
            }
            catch (DbUpdateException)
            {
                if (await _repository.GetById(revisao.Id) == null)
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

        [HttpDelete]
        public async Task<ActionResult> DeleteRevisao(Revisao revisao)
        {
            if (revisao == null || revisao.Id == 0) { return BadRequest(); }
            await _repository.Delete(revisao);
            return NoContent();
        }
    }
}

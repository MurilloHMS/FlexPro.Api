using FlexPro.Api.Data;
using FlexPro.Api.Interfaces;
using FlexPro.Api.Models;
using FlexPro.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

namespace FlexPro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
            _produtoRepository = new ProdutoRepository(_context);
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> GetProduto()
        {
            var product = await _produtoRepository.GetAll();
            return product == null ? NotFound() : Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var product = await _produtoRepository.GetById(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpGet("receitas/{idReceita}")]
        public async Task<ActionResult<List<Produto>>> GetProdutoByReceitaId(int idReceita)
        {
            var product = await _produtoRepository.GetByIdReceita(idReceita);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> PostProduct(Produto product)
        {
            await _produtoRepository.SaveOrUpdate(product);
            return CreatedAtAction(nameof(GetProduto), new {id = product.Id}, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id,  Produto product)
        {
            if(id != product.Id)
            {
                return BadRequest();
            }

            try
            {
                await _produtoRepository.SaveOrUpdate(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _produtoRepository.GetById(id) == null)
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
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _produtoRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            await _produtoRepository.Delete(product);
            return NoContent();
        }
    }
}

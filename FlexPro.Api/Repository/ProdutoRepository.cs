using FlexPro.Api.Data;
using FlexPro.Api.Interfaces;
using FlexPro.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            var products = await _context.Produto.ToListAsync();
            return products ?? Enumerable.Empty<Produto>();
        }

        public async Task<Produto> GetById(int id)
        {
            var product = await _context.Produto.FirstOrDefaultAsync(x => x.Id == id);
            return product ?? null;
        }

        public async Task<IEnumerable<Produto>> GetByIdReceita(int id)
        {
            var products = await _context.Produto.Where(x => x.IdReceita == id).ToListAsync();
            return products ?? Enumerable.Empty<Produto>();
        }

        public async Task SaveOrUpdate(Produto produto)
        {
            var productFounded = await _context.Produto.FirstOrDefaultAsync(x => x.Id == produto.Id);
            if (productFounded != null)
            {
                _context.Entry(productFounded).CurrentValues.SetValues(produto);
            }
            else
            {
                _context.Produto.Add(produto);
            }
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Produto produto)
        {
            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }
}

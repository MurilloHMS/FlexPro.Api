using FlexPro.Api.Data;
using FlexPro.Api.Interfaces;
using FlexPro.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context) 
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            var category = _context.Categoria.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                _context.Categoria.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            var categories = await _context.Categoria.ToListAsync();
            return categories ?? Enumerable.Empty<Categoria>();
        }

        public async Task<Categoria> GetById(int id)
        {
            var category = await _context.Categoria.FirstOrDefaultAsync(c => c.Id == id);
            return category ?? null;
        }

        public async Task<Categoria> GetByName(string name)
        {
            var category = await _context.Categoria.FirstOrDefaultAsync(c => c.Nome == name);
            return category ?? null;
        }

        public async Task SaveOrUpdate(Categoria category)
        {
            var categoryFounded = _context.Categoria.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (categoryFounded != null)
            {
                _context.Entry(categoryFounded).CurrentValues.SetValues(category);
            }
            else
            {
                _context.Categoria.Add(category);
            }
            await _context.SaveChangesAsync();
        }
    }
}

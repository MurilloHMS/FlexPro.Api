using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Infrastructure.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context) { }

        public async Task<Categoria> GetByNameAsync(string name)
        {
            var category = await _dbSet.FirstOrDefaultAsync(c => c.Nome == name);
            return category ?? null;
        }

        public async Task SaveOrUpdate(Categoria category)
        {
            var categoryFounded = await _dbSet.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (categoryFounded != null)
            {
                _context.Entry(categoryFounded).CurrentValues.SetValues(category);
            }
            else
            {
                _dbSet.Add(category);
            }
            await _context.SaveChangesAsync();
        }
    }
}

using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Infrastructure.Repositories;

public class CategoriaRepository(AppDbContext context) : Repository<Categoria>(context), ICategoriaRepository
{
    private readonly DbSet<Categoria> _dbSet = context.Set<Categoria>();

    public async Task<Categoria> GetByNameAsync(string name)
    {
        var category = await _dbSet.FirstOrDefaultAsync(c => c.Nome == name);
        return category ?? null;
    }

    public async Task SaveOrUpdate(Categoria category)
    {
        var categoryFounded = await _dbSet.FirstOrDefaultAsync(x => x.Id == category.Id);
        if (categoryFounded != null)
            context.Entry(categoryFounded).CurrentValues.SetValues(category);
        else
            _dbSet.Add(category);
        await context.SaveChangesAsync();
    }
}
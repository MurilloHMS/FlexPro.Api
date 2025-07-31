using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Infrastructure.Repositories;

public class VeiculoRepository(AppDbContext context) : Repository<Veiculo>(context), IVeiculoRepository
{
    private readonly DbSet<Veiculo> _dbSet = context.Set<Veiculo>();

    public async Task<Veiculo?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Nome == name);
    }
}
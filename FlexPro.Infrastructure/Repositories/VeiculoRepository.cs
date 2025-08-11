using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Infrastructure.Repositories;

public class VeiculoRepository(AppDbContext context) : Repository<Vehicle>(context), IVeiculoRepository
{
    private readonly DbSet<Vehicle> _dbSet = context.Set<Vehicle>();

    public async Task<Vehicle?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Nome == name);
    }
}
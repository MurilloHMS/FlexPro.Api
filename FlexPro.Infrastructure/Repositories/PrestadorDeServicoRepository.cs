using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Infrastructure.Repositories;

public class PrestadorDeServicoRepository(AppDbContext context)
    : Repository<PrestadorDeServico>(context), IPrestadorDeServicoRepository
{
    private readonly DbSet<PrestadorDeServico> _dbSet = context.Set<PrestadorDeServico>();

    public async Task DeleteById(int id)
    {
        var entidade = await context.PrestadorDeServico.FirstOrDefaultAsync(x => x.Id == id);
        if (entidade != null)
        {
            context.PrestadorDeServico.Remove(entidade);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<PrestadorDeServico>> GetAllAsync()
    {
        var entities = await context.PrestadorDeServico.ToListAsync();
        return entities ?? Enumerable.Empty<PrestadorDeServico>();
    }

    public async Task<PrestadorDeServico?> GetByIdAsync(int id)
    {
        var entidade = await context.PrestadorDeServico.FirstOrDefaultAsync(x => x.Id == id);
        return entidade ?? null;
    }
}
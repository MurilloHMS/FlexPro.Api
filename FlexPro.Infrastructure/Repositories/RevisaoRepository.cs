using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Infrastructure.Repositories;

public class RevisaoRepository(AppDbContext context) : Repository<Revisao>(context), IRevisaoRepository
{
    private readonly DbSet<Revisao> _dbSet = context.Set<Revisao>();


    public async Task<Revisao?> GetByName(string name)
    {
        var revisao = await _dbSet.FirstOrDefaultAsync(x => x.Motorista == name);
        return revisao;
    }

    public async Task<IEnumerable<Revisao>> GetByVehicleId(int vehicleId)
    {
        var revisao = await _dbSet.Where(x => x.VeiculoId == vehicleId).ToListAsync();
        return revisao.Any() ? revisao : Enumerable.Empty<Revisao>();
    }

    public async Task UpdateOrInsert(Revisao revisao)
    {
        var revisaoFounded = await _dbSet.FirstOrDefaultAsync(x => x.Id == revisao.Id);
        if (revisaoFounded != null)
            context.Entry(revisaoFounded).CurrentValues.SetValues(revisao);
        else
            context.Revisao.Add(revisao);

        await context.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        var revisao = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        if (revisao != null)
        {
            context.Remove(revisao);
            await context.SaveChangesAsync();
        }
    }
}
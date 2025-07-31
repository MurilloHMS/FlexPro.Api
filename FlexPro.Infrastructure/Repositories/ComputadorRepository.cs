using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Infrastructure.Repositories;

public class ComputadorRepository(AppDbContext context) : Repository<Computador>(context), IComputadorRepository
{
    private readonly DbSet<Computador> _dbSet = context.Set<Computador>();

    public async Task InsertAcessoRemoto(int id, AcessoRemoto model)
    {
        var computer = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        if (computer != null)
        {
            computer.AcessosRemotos?.Add(model);
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteAcessoRemoto(int id, AcessoRemoto model)
    {
        var computer = await _dbSet.Include(c => c.AcessosRemotos).FirstOrDefaultAsync(x => x.Id == id);
        if (computer != null)
        {
            var acesso = computer.AcessosRemotos?.FirstOrDefault(x => x.Id == model.Id);
            if (acesso != null)
            {
                computer.AcessosRemotos?.Remove(acesso);
                await context.SaveChangesAsync();
            }
        }
    }


    public async Task UpdateAcessoRemoto(AcessoRemoto model)
    {
        var acessoRemoto = await context.AcessoRemoto.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (acessoRemoto != null)
        {
            context.Entry(acessoRemoto).CurrentValues.SetValues(model);
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteAcessoRemotoAsync(int id, AcessoRemoto model)
    {
        var acesso = await context.AcessoRemoto
            .FirstOrDefaultAsync(x => x.Id == model.Id && x.IdComputador == id);

        if (acesso != null)
        {
            context.AcessoRemoto.Remove(acesso);
            await context.SaveChangesAsync();
        }
    }
}
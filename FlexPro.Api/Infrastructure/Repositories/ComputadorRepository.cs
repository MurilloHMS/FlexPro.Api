using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Infrastructure.Repositories;

public class ComputadorRepository : Repository<Computador>,  IComputadorRepository
{
    public ComputadorRepository(AppDbContext context) :base(context) { }

    public async Task InsertAcessoRemoto(int id, AcessoRemoto model)
    {
        var computer = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        if (computer != null)
        {
            computer.AcessosRemotos.Add(model);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAcessoRemoto(int id, AcessoRemoto model)
    {
        var computer = await _dbSet.Include(c => c.AcessosRemotos).FirstOrDefaultAsync(x => x.Id == id);
        if (computer != null)
        {
            var acesso = computer.AcessosRemotos.FirstOrDefault(x => x.Id == model.Id);
            if (acesso != null)
            {
                computer.AcessosRemotos.Remove(acesso);
                await _context.SaveChangesAsync();
            }
        }
    }
    
    public async Task DeleteAcessoRemotoAsync(int id, AcessoRemoto model)
    {
        var acesso = await _context.AcessoRemoto
            .FirstOrDefaultAsync(x => x.Id == model.Id && x.IdComputador == id);

        if (acesso != null)
        {
            _context.AcessoRemoto.Remove(acesso);
            await _context.SaveChangesAsync();
        }
    }


    public async Task UpdateAcessoRemoto(AcessoRemoto model)
    {
        var acessoRemoto = await _context.AcessoRemoto.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (acessoRemoto != null)
        {
            _context.Entry(acessoRemoto).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();
        }
    }
}
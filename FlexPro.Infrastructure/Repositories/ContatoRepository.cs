using FlexPro.Domain.Entities;
using FlexPro.Domain.Enums;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Infrastructure.Repositories;

public class ContatoRepository(AppDbContext context) : Repository<Contato>(context), IContatoRepository
{
    private readonly DbSet<Contato> _dbSet = context.Set<Contato>();

    public async Task InsertOrUpdateContatoAsync(Contato contato)
    {
        var contatoFounded = await _dbSet.FirstOrDefaultAsync(x => x.Id == contato.Id);
        if (contatoFounded != null)
            context.Entry(contato).CurrentValues.SetValues(contato);
        else
            _dbSet.Add(contato);
        await context.SaveChangesAsync();
    }

    public async Task<List<Contato>> GetContatosByStatusAsync(StatusContatoE status)
    {
        var contatos = await context.Contato.Where(x => x.StatusContato == status).ToListAsync();
        return contatos;
    }

    public async Task<List<Contato>> GetContatosByTipoAsync(TipoContatoE tipo)
    {
        var contatos = await context.Contato.Where(x => x.TipoContato == tipo).ToListAsync();
        return contatos;
    }
}
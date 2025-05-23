using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Infrastructure.Repositories;

public class ContatoRepository : Repository<Contato>, IContatoRepository
{
    public ContatoRepository(AppDbContext context) : base(context) { }

    public async Task InsertOrUpdateContatoAsync(Contato contato)
    {
        var ContatoFounded = await _dbSet.FirstOrDefaultAsync(x => x.Id == contato.Id);
        if (ContatoFounded != null)
        {
            _context.Entry(contato).CurrentValues.SetValues(contato);
        }
        else
        {
            _dbSet.Add(contato);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<List<Contato>> GetContatosByStatusAsync(StatusContato_e status)
    {
        var contatos = await _context.Contato.Where(x => x.StatusContato == status).ToListAsync();
        return contatos;
    }

    public async Task<List<Contato>> GetContatosByTipoAsync(TipoContato_e tipo)
    {
        var contatos = await _context.Contato.Where(x => x.TipoContato == tipo).ToListAsync();
        return contatos;
    }
}
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Infrastructure.Repositories;

public class ContatoRepository : IContatoRepository
{
    private readonly AppDbContext _context;
    public ContatoRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Contato>> GetContatosAsync()
    {
        var contatosLista = await _context.Contato.ToListAsync();
        return contatosLista;
    }

    public async Task<Contato> GetContatoAsync(int id)
    {
        var contato = await _context.Contato.FirstOrDefaultAsync(x => x.Id == id);
        return contato!;
    }

    public async Task InsertOrUpdateContatoAsync(Contato contato)
    {
        var ContatoFounded = await _context.Contato.FirstOrDefaultAsync(x => x.Id == contato.Id);
        if (ContatoFounded != null)
        {
            _context.Entry(contato).CurrentValues.SetValues(contato);
        }
        else
        {
            _context.Contato.Add(contato);
        }
        await _context.SaveChangesAsync();
    }

    public async Task DeleteContatoAsync(Contato contato)
    {
        _context.Contato.Remove(contato);
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
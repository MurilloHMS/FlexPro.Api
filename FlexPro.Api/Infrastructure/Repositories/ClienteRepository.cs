using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Cliente>> GetAll()
    {
        List<Cliente> clientes = await _context.Cliente.ToListAsync();
        return clientes ?? new List<Cliente>();
    }

    public async Task<Cliente> GetById(int id)
    {
        Cliente cliente = _context.Cliente.FirstOrDefault(x => x.Id == id);
        return cliente!;
    }

    public async Task<Cliente> GetByEmail(string email)
    {
        Cliente cliente = _context.Cliente.FirstOrDefault(x => x.Email == email);
        return cliente!;
    }

    public async Task<Cliente> GetByName(string nome)
    {
        Cliente cliente = _context.Cliente.FirstOrDefault(x => x.Nome == nome);
        return cliente!;
    }

    public async Task UpdateOrInsert(Cliente cliente)
    {
        Cliente clienteEncontrado = await _context.Cliente.FirstOrDefaultAsync(x => x.Id == cliente.Id);
        if (clienteEncontrado != null)
        {
            _context.Entry(clienteEncontrado).CurrentValues.SetValues(cliente);
        }
        else
        {
            _context.Cliente.Add(cliente);
        }
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        Cliente cliente = await _context.Cliente.FirstOrDefaultAsync(x => x.Id == id);
        if (cliente != null)
        {
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
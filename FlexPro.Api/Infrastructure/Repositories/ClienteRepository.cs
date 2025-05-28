using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Infrastructure.Repositories;

public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    public ClienteRepository(AppDbContext context) : base(context) { }
    

    public async Task<Cliente> GetByEmail(string email)
    {
        Cliente cliente = _dbSet.FirstOrDefault(x => x.Email == email);
        return cliente!;
    }

    public async Task<Cliente> GetByName(string nome)
    {
        Cliente cliente = _dbSet.FirstOrDefault(x => x.Nome == nome);
        return cliente!;
    }

    public async Task UpdateOrInsert(Cliente cliente)
    {
        Cliente clienteEncontrado = await _dbSet.FirstOrDefaultAsync(x => x.Id == cliente.Id);
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

    public async Task IncludeClienteByRange(List<Cliente> clientes)
    {
        await _dbSet.AddRangeAsync(clientes);
        await _context.SaveChangesAsync();
    }
}
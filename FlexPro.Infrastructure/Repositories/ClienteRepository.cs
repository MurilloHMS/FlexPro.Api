using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Infrastructure.Repositories;

public class ClienteRepository(AppDbContext context) : Repository<Cliente>(context), IClienteRepository
{
    private readonly DbSet<Cliente> _dbSet = context.Set<Cliente>();


    public async Task<Cliente> GetByEmail(string email)
    {
        var cliente = _dbSet.FirstOrDefault(x => x.Email == email);
        return cliente!;
    }

    public async Task<Cliente> GetByName(string nome)
    {
        var cliente = _dbSet.FirstOrDefault(x => x.Nome == nome);
        return cliente!;
    }

    public async Task UpdateOrInsert(Cliente cliente)
    {
        var clienteEncontrado = await _dbSet.FirstOrDefaultAsync(x => x.Id == cliente.Id);
        if (clienteEncontrado != null)
            context.Entry(clienteEncontrado).CurrentValues.SetValues(cliente);
        else
            context.Cliente.Add(cliente);
        await context.SaveChangesAsync();
    }

    public async Task IncludeClienteByRange(List<Cliente> clientes)
    {
        await _dbSet.AddRangeAsync(clientes);
        await context.SaveChangesAsync();
    }
}
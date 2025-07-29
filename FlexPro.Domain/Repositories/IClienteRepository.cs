using FlexPro.Domain.Entities;

namespace FlexPro.Domain.Repositories;

public interface IClienteRepository : Api.Application.Interfaces.IRepository<Cliente>
{
    Task<Cliente> GetByEmail(string email);
    Task<Cliente> GetByName(string nome);
    Task UpdateOrInsert(Cliente cliente);
    Task IncludeClienteByRange(List<Cliente> clientes);
}
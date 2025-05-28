using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Interfaces;

public interface IClienteRepository : IRepository<Cliente>
{
    Task<Cliente> GetByEmail(string email);
    Task<Cliente> GetByName(string nome);
    Task UpdateOrInsert(Cliente cliente);
    Task IncludeClienteByRange(List<Cliente> clientes);
}
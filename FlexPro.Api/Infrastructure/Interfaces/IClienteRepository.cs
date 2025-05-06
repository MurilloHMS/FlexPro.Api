using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> GetAll();
    Task<Cliente> GetById(int id);
    Task<Cliente> GetByEmail(string email);
    Task<Cliente> GetByName(string nome);
    Task UpdateOrInsert(Cliente cliente);
    Task Delete(int id);
    Task IncludeClienteByRange(List<Cliente> clientes);
}
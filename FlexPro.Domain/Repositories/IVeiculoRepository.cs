using FlexPro.Domain.Entities;

namespace FlexPro.Domain.Repositories;

public interface IVeiculoRepository : IRepository<Veiculo>
{
    Task<Veiculo?> GetByNameAsync(string name);
}
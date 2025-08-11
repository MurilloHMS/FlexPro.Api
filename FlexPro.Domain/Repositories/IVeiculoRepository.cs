using FlexPro.Domain.Entities;

namespace FlexPro.Domain.Repositories;

public interface IVeiculoRepository : IRepository<Vehicle>
{
    Task<Vehicle?> GetByNameAsync(string name);
}
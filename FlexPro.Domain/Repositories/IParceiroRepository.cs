using FlexPro.Domain.Entities;

namespace FlexPro.Domain.Repositories;

public interface IParceiroRepository : IRepository<Parceiro>
{
    Task IncludeParceiroByRangeAsync(List<Parceiro> parceiros);
    Task<Parceiro> GetByNameAsync(string nome);
}
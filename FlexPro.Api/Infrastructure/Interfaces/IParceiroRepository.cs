using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Interfaces;

public interface IParceiroRepository : IRepository<Parceiro>
{
    Task IncludeParceiroByRangeAsync(List<Parceiro> parceiros);
    Task<Parceiro> GetByNameAsync(string nome);
}
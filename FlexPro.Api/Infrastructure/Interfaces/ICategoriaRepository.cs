using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<Categoria> GetByNameAsync(string name);
        Task SaveOrUpdate(Categoria categoria);
    }
}

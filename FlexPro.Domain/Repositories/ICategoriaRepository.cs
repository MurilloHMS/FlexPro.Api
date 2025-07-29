using FlexPro.Domain.Entities;

namespace FlexPro.Domain.Repositories
{
    public interface ICategoriaRepository : Api.Application.Interfaces.IRepository<Categoria>
    {
        Task<Categoria> GetByNameAsync(string name);
        Task SaveOrUpdate(Categoria categoria);
    }
}

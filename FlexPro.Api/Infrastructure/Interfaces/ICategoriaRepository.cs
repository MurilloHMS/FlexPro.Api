using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAll();
        Task<Categoria> GetById(int id);
        Task<Categoria> GetByName(string name);
        Task SaveOrUpdate(Categoria categoria);
        Task Delete(int id);
    }
}

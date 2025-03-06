using FlexPro.Api.Models;

namespace FlexPro.Api.Interfaces
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

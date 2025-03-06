using FlexPro.Api.Models;

namespace FlexPro.Api.Interfaces
{
    public interface IEntidadeRepository
    {
        Task<IEnumerable<Entidade>> GetAll();
        Task<Entidade> GetById(int id);
        Task SaveOrUpdate(Entidade entidade);
        Task DeleteById(int id);
        Task Delete(Entidade entidade);
    }
}

using FlexPro.Api.Models;

namespace FlexPro.Api.Interfaces
{
    public interface IReceitaRepository
    {
        Task<IEnumerable<Receita>> GetAll();
        Task<Receita> GetById(int id);
        Task SaveOrUpdate(Receita receita);
        Task Delete(Receita receita);
    }
}

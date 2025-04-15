using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Interfaces
{
    public interface IReceitaRepository
    {
        Task<IEnumerable<Receita>> GetAll();
        Task<Receita> GetById(int id);
        Task SaveOrUpdate(Receita receita);
        Task Delete(Receita receita);
    }
}

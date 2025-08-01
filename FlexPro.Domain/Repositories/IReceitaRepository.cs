using FlexPro.Domain.Entities;

namespace FlexPro.Domain.Repositories;

public interface IReceitaRepository
{
    Task<IEnumerable<Receita>> GetAll();
    Task<Receita?> GetById(int id);
    Task SaveOrUpdate(Receita receita);
    Task Delete(Receita receita);
}
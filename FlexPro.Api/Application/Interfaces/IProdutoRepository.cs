using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(int id);
        Task<IEnumerable<Produto>> GetByIdReceita(int id);
        Task SaveOrUpdate(Produto produto);
        Task Delete(Produto produto);
    }
}

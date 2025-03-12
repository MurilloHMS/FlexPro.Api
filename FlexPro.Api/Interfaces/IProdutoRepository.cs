using FlexPro.Api.Models;

namespace FlexPro.Api.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(int id);
        Task SaveOrUpdate(Produto produto);
        Task Delete(Produto produto);
    }
}

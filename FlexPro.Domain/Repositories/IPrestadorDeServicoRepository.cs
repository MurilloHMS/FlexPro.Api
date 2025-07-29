
using FlexPro.Domain.Entities;

namespace FlexPro.Domain.Repositories
{
    public interface IPrestadorDeServicoRepository : IRepository<PrestadorDeServico>
    {
        Task DeleteById(int id);
    }
}

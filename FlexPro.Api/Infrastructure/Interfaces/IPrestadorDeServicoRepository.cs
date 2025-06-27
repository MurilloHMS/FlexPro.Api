using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Interfaces
{
    public interface IPrestadorDeServicoRepository : IRepository<PrestadorDeServico>
    {
        Task DeleteById(int id);
    }
}

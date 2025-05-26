using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Interfaces
{
    public interface IEntidadeRepository : IRepository<Entidade>
    {
        Task DeleteById(int id);
    }
}

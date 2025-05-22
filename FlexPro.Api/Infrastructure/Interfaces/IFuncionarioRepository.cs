using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Interfaces
{
    public interface IFuncionarioRepository : IRepository<Funcionario>
    {
        Task SaveOrUpdate(Funcionario funcionario);
        Task DeleteById(int id);
    }
}

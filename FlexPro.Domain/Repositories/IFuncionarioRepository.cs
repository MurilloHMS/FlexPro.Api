using FlexPro.Domain.Entities;

namespace FlexPro.Domain.Repositories
{
    public interface IFuncionarioRepository : Api.Application.Interfaces.IRepository<Funcionario>
    {
        Task SaveOrUpdate(Funcionario funcionario);
        Task DeleteById(int id);
    }
}

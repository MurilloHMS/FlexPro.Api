using FlexPro.Domain.Entities;

namespace FlexPro.Domain.Repositories
{
    public interface IFuncionarioRepository : IRepository<Funcionario>
    {
        Task SaveOrUpdate(Funcionario funcionario);
        Task DeleteById(int id);
    }
}

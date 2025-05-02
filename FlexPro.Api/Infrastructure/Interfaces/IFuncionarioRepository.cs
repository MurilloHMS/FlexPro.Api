using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<IEnumerable<Funcionario>> GetAll();
        Task<Funcionario> GetById(int id);
        Task SaveOrUpdate(Funcionario funcionario);
        Task DeleteById(int id);
        Task Delete(Funcionario funcionario);
    }
}

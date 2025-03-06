using FlexPro.Api.Models;

namespace FlexPro.Api.Interfaces
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

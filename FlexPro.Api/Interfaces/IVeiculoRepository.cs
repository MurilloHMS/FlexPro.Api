using FlexPro.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Interfaces
{
    public interface IVeiculoRepository
    {
        Task<IEnumerable<Veiculo>> GetAll();
        Task<Veiculo> GetById(int id);
        Task<Veiculo> GetByName(string name);
        Task UpdateOrInsert(Veiculo vehicle);
        Task Delete(Veiculo vehicle);
    }
}

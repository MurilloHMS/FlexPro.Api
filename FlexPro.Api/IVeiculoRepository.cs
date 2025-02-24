using FlexPro.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api
{
    public interface IVeiculoRepository
    {
        Task<IEnumerable<Veiculo>> GetAll();
        Task<Veiculo> GetById(int id);
        Task<Veiculo> GetByName(string name);
        Task<Veiculo> UpdateOrInsert(Veiculo vehicle);
        Task<Veiculo> Delete(Veiculo vehicle);
    }
}

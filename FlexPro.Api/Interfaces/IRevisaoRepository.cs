using FlexPro.Api.Models;

namespace FlexPro.Api.Interfaces
{
    public interface IRevisaoRepository
    {
        Task<IEnumerable<Revisao>> GetAll();
        Task<Revisao> GetById(int id);
        Task<Revisao> GetByName(string name);
        Task<IEnumerable<Revisao>> GetByVehicleId(int vehicleId);
        Task UpdateOrInsert(Revisao revisao);
        Task DeleteById(int id);
        Task Delete(Revisao revisao);
    }
}

using FlexPro.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Interfaces
{
    public interface IVeiculoRepository : IRepository<Veiculo>
    {
       Task<Veiculo> GetByNameAsync(string name);
    }
}

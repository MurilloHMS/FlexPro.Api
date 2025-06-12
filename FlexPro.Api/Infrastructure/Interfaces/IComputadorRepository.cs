using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Interfaces;

public interface IComputadorRepository : IRepository<Computador>
{
    Task InsertAcessoRemoto(int id, AcessoRemoto model);
    Task DeleteAcessoRemoto(int id,  AcessoRemoto model);
    Task UpdateAcessoRemoto(AcessoRemoto model);
}
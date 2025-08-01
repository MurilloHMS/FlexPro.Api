using FlexPro.Domain.Entities;

namespace FlexPro.Domain.Repositories;

public interface IComputadorRepository : IRepository<Computador>
{
    Task InsertAcessoRemoto(int id, AcessoRemoto model);
    Task DeleteAcessoRemoto(int id, AcessoRemoto model);
    Task UpdateAcessoRemoto(AcessoRemoto model);
}
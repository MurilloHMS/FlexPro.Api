using FlexPro.Domain.Entities;
using FlexPro.Domain.Enums;

namespace FlexPro.Domain.Repositories;

public interface IContatoRepository : IRepository<Contato>
{
    Task InsertOrUpdateContatoAsync(Contato contato);
    Task<List<Contato>> GetContatosByStatusAsync(StatusContato_e status);
    Task<List<Contato>> GetContatosByTipoAsync(TipoContato_e tipo);
}
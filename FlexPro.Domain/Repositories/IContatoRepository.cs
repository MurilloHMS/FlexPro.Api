using FlexPro.Domain.Entities;
using FlexPro.Domain.Enums;

namespace FlexPro.Domain.Repositories;

public interface IContatoRepository : IRepository<Contato>
{
    Task InsertOrUpdateContatoAsync(Contato contato);
    Task<List<Contato>> GetContatosByStatusAsync(StatusContatoE status);
    Task<List<Contato>> GetContatosByTipoAsync(TipoContatoE tipo);
}
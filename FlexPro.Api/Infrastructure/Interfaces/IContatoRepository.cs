using FlexPro.Api.Domain;
using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Interfaces;

public interface IContatoRepository : IRepository<Contato>
{
    Task InsertOrUpdateContatoAsync(Contato contato);
    Task<List<Contato>> GetContatosByStatusAsync(StatusContato_e status);
    Task<List<Contato>> GetContatosByTipoAsync(TipoContato_e tipo);
}
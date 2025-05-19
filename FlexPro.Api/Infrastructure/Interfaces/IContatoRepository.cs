using FlexPro.Api.Domain;
using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Interfaces;

public interface IContatoRepository
{
    Task<List<Contato>> GetContatosAsync();
    Task<Contato> GetContatoAsync(int id);
    Task InsertOrUpdateContatoAsync(Contato contato);
    Task DeleteContatoAsync(Contato contato);
    Task<List<Contato>> GetContatosByStatusAsync(StatusContato_e status);
    Task<List<Contato>> GetContatosByTipoAsync(TipoContato_e tipo);
}
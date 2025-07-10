using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.DTOs.Computer;

public class ComputerResponseDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public bool Interno { get; set; }
    public string Marca { get; set; }
    
    public virtual ICollection<AcessoRemoto> AcessosRemotos { get; set; }
    public virtual Especificacoes Especificacoes { get; set; }
}
using FlexPro.Domain.Entities;

namespace FlexPro.Api.Application.DTOs.Computer;

public class ComputerRequestDTO
{
    public string Nome { get; set; }
    public bool Interno { get; set; }
    public string Marca { get; set; }
    
    public virtual ICollection<AcessoRemoto> AcessosRemotos { get; set; }
    public virtual Especificacoes Especificacoes { get; set; }
}
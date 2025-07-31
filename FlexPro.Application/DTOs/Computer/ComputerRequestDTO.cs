using FlexPro.Domain.Entities;

namespace FlexPro.Application.DTOs.Computer;

public class ComputerRequestDto
{
    public string Nome { get; set; } = string.Empty;
    public bool Interno { get; set; }
    public string Marca { get; set; } = string.Empty;

    public virtual ICollection<AcessoRemoto>? AcessosRemotos { get; set; }
    public virtual Especificacoes? Especificacoes { get; set; }
}
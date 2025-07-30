namespace FlexPro.Domain.Entities;

public class Computador : Equipamento
{
    public bool Interno { get; set; }
    public string Marca { get; set; } = string.Empty;
    
    public virtual ICollection<AcessoRemoto>? AcessosRemotos { get; set; }
    public virtual Especificacoes? Especificacoes { get; set; }
}
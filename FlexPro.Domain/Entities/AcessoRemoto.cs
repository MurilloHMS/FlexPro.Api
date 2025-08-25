using FlexPro.Domain.Abstractions;
using FlexPro.Domain.Enums;

namespace FlexPro.Domain.Entities;

public class AcessoRemoto : Entity
{
    public string? Usuario { get; set; }
    public string Senha { get; set; } = string.Empty;
    public TipoAcessoRemotoE TipoAcessoRemoto { get; set; }
    public string Conexao { get; set; } = string.Empty;

    public int IdComputador { get; set; }
    public virtual Computador? Computador { get; set; }
}
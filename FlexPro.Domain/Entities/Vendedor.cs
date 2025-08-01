using FlexPro.Domain.Enums;

namespace FlexPro.Domain.Entities;

public class Vendedor : Entidade
{
    public HierarquiaE Hierarquia { get; set; }
    public string Gerente { get; set; } = string.Empty;
    public string Departamento { get; set; } = string.Empty;
    public TipoVendedorE TipoVendedor { get; set; }
}
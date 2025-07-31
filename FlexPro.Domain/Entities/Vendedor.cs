using FlexPro.Domain.Enums;

namespace FlexPro.Domain.Entities;

public class Vendedor : Entidade
{
    public Hierarquia_e Hierarquia { get; set; }
    public string Gerente { get; set; } = string.Empty;
    public string Departamento { get; set; } = string.Empty;
    public TipoVendedor_e TipoVendedor { get; set; }
}
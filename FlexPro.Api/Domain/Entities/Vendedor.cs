namespace FlexPro.Api.Domain.Entities;

public class Vendedor : Entidade
{
    public Hierarquia_e Hierarquia {get; set;}
    public string Gerente {get; set;}
    public string Departamento {get; set;}
    public TipoVendedor_e TipoVendedor {get; set;}
}
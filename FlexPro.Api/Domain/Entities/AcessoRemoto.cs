namespace FlexPro.Api.Domain.Entities;

public class AcessoRemoto
{
    public int Id { get; set; }
    public string? Usuario { get; set; }
    public string Senha { get; set; }
    public TipoAcessoRemoto TipoAcessoRemoto { get; set; }
    public string Conexao { get; set; }
    
    public int IdComputador  { get; set; }
    public virtual Computador  Computador { get; set; }
}
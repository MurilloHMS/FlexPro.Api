using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FlexPro.Domain.Enums;

namespace FlexPro.Domain.Entities;

public class AcessoRemoto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Usuario { get; set; }
    public string Senha { get; set; } = string.Empty;
    public TipoAcessoRemoto TipoAcessoRemoto { get; set; }
    public string Conexao { get; set; } = string.Empty;
    
    public int IdComputador  { get; set; }
    public virtual Computador? Computador { get; set; }
}
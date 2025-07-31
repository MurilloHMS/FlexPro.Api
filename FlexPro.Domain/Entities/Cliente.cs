using FlexPro.Domain.Enums;

namespace FlexPro.Domain.Entities;

public class Cliente : Entidade
{
    // Cliente Vindo do site
    public StatusContatoE Status { get; set; }
    public string Contato { get; set; } = string.Empty;
    public FormasDeContatoE MeioDeContato { get; set; }
}
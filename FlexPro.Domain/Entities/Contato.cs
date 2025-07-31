using FlexPro.Domain.Abstractions;
using FlexPro.Domain.Enums;

namespace FlexPro.Domain.Entities;

public class Contato : Entity
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public TipoContatoE TipoContato { get; set; }
    public string? Outro { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public string NomeEmpresa { get; set; } = string.Empty;
    public StatusContatoE StatusContato { get; set; }
    public DateTime DataSolicitadoContato { get; set; }
}
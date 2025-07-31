using FlexPro.Domain.Abstractions;
using FlexPro.Domain.Enums;

namespace FlexPro.Domain.Entities;

public class Contato : Entity
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public TipoContato_e TipoContato { get; set; }
    public string? outro { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public string NomeEmpresa { get; set; } = string.Empty;
    public StatusContato_e StatusContato { get; set; }
    public DateTime DataSolicitadoContato { get; set; }
}
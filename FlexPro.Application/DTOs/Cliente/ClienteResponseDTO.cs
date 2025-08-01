using FlexPro.Domain.Enums;

namespace FlexPro.Application.DTOs.Cliente;

public class ClienteResponseDto
{
    public int Id { get; set; }
    public string CodigoSistema { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public StatusContatoE Status { get; set; }
    public string Contato { get; set; } = string.Empty;
    public FormasDeContatoE MeioDeContato { get; set; }
}
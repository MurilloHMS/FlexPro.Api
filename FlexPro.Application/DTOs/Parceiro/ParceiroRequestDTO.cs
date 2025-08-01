namespace FlexPro.Application.DTOs.Parceiro;

public class ParceiroRequestDto
{
    public string Nome { get; set; } = string.Empty;
    public string? CodigoSistema { get; set; }
    public string? Email { get; set; }
    public bool Ativo { get; set; }
    public string? RazaoSocial { get; set; }
    public bool RecebeEmail { get; set; }
}
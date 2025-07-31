namespace FlexPro.Domain.Models;

public class DadosNotasFiscais
{
    public string? Fornecedor { get; set; } = string.Empty;
    public string? NumeroNota { get; set; } = string.Empty;
    public DateTime DataNota { get; set; }
    public string? Produto { get; set; } = string.Empty;
    public decimal ValorUnitario { get; set; }
    public decimal ValorTotal { get; set; }
    public string? Cfop { get; set; } = string.Empty;
}
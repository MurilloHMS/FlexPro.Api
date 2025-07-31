namespace FlexPro.Domain.Entities;

public class BoaSolucao
{
    public string CodigoProduto { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Quantidade { get; set; }
    public decimal Valor { get; set; }
    public decimal ValorTotal { get; set; }
    public bool? IsValid { get; set; }
}
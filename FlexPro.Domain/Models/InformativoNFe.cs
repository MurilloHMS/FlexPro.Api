namespace FlexPro.Domain.Models;

public class InformativoNFe
{
    public int NumeroNFe { get; set; }
    public DateTime Data { get; set; }
    public string CodigoCliente { get; set; } = string.Empty;
    public string NomeDoCliente { get; set; } = string.Empty;
    public string CodigoProduto { get; set; } = string.Empty;
    public string NomeDoProduto { get; set; } = string.Empty;
    public char TipoDeUnidade { get; set; }
    public double Quantidade { get; set; }
    public decimal ValorTotalComImpostos { get; set; }
}
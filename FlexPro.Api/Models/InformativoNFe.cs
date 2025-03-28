namespace FlexPro.Api.Models
{
    public class InformativoNFe
    {
        public int NumeroNFe { get; set; }
        public DateTime Data {  get; set; }
        public string CodigoCliente { get; set; }
        public string NomeDoCliente { get; set; }
        public string CodigoProduto { get; set; }
        public string NomeDoProduto { get; set; }
        public char TipoDeUnidade { get; set; }
        public double Quantidade { get; set; }
        public decimal ValorTotalComImpostos { get; set; }
    }
}

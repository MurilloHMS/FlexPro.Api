namespace FlexPro.Api.Domain.Models
{
    public class Informativo
    {
        public string CodigoCliente { get; set; }
        public string NomeDoCliente { get; set; }
        public DateTime Data {  get; set; }
        public string Mes { get; set; }
        public int QuantidadeDeProdutos { get; set; }
        public double QuantidadeDeLitros { get; set; }
        public int QuantidadeDeVisitas { get; set; }
        public int QuantidadeNotasEmitidas { get; set; }
        public int MediaDiasAtendimento { get; set; }
        public string ProdutoEmDestaque { get; set; }
        public decimal FaturamentoTotal { get; set; }
        public decimal ValorDePeçasTrocadas { get; set; }
        public string? Status { get; set; }
        public bool ClienteCadastrado { get; set; }
        public string EmailCliente { get; set; }
    }
}

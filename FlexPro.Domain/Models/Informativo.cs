namespace FlexPro.Domain.Models
{
    public class Informativo
    {
        public string CodigoCliente { get; set; } = string.Empty;
        public string NomeDoCliente { get; set; }  = string.Empty;
        public DateTime Data {  get; set; }
        public string Mes { get; set; }  = string.Empty;
        public int QuantidadeDeProdutos { get; set; }
        public double QuantidadeDeLitros { get; set; }
        public int QuantidadeDeVisitas { get; set; }
        public int QuantidadeNotasEmitidas { get; set; }
        public int MediaDiasAtendimento { get; set; }
        public string ProdutoEmDestaque { get; set; }  = string.Empty;
        public decimal FaturamentoTotal { get; set; }
        public decimal ValorDePeçasTrocadas { get; set; }
        public string? Status { get; set; }
        public bool ClienteCadastrado { get; set; }
        public string EmailCliente { get; set; }  = string.Empty;
    }
}

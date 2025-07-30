using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities
{
    public class Revisao : Entity
    {
        public DateTime? Data { get; set; }
        public int Kilometragem { get; set; }
        public string NotaFiscal { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Motorista { get; set; } = string.Empty;
        public string? Observacao { get; set; }
        // Chaves estrangeiras
        public int LocalId { get; set; } // Propriedade para a chave estrangeira
        public int VeiculoId { get; set; } // Propriedade para a chave estrangeira

        // Propriedades de navegação
        public virtual PrestadorDeServico? Local { get; set; }
        public virtual Veiculo? Veiculo { get; set; }
    }
}
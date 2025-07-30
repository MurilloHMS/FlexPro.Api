using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities
{
    public class Receita : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public double Embalagem { get; set; }
        public decimal ValorMaoDeObra { get; set; }
        public double Caixas { get; set; }
        
        public virtual ICollection<ReceitaMateriaPrima>? ReceitaMateriaPrima { get; set; }
    }
}
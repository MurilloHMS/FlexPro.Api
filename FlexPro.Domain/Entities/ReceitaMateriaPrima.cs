using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities;

public class ReceitaMateriaPrima : Entity
{
    public int ReceitaId { get; set; }
    public virtual Receita? Receita { get; set; }
    
    public int MateriaPrimaId { get; set; }
    public virtual MateriaPrima? MateriaPrima { get; set; }
    
    public decimal QuantidadeFormula { get; set; }
}
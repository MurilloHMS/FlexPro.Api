namespace FlexPro.Domain.Entities;

public class ReceitaMateriaPrima
{
    public int ReceitaId { get; set; }
    public virtual Receita? Receita { get; set; }
    
    public int MateriaPrimaId { get; set; }
    public virtual MateriaPrima? MateriaPrima { get; set; }
    
    public decimal QuantidadeFormula { get; set; }
}
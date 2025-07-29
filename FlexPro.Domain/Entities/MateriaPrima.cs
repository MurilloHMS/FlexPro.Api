using FlexPro.Domain.Enums;

namespace FlexPro.Domain.Entities;

public class MateriaPrima : Produto
{
    public decimal? QuantidadeProducao { get; set; }
    public TipoEstoque_e TipoEstoque { get; set; }
    public TipoMateriaPrima_e TipoMateriaPrima { get; set; }
    
    public virtual ICollection<ReceitaMateriaPrima>? ReceitaMateriaPrima { get; set; }
}
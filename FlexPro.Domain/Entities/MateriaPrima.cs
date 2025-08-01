using FlexPro.Domain.Enums;

namespace FlexPro.Domain.Entities;

public class MateriaPrima : Produto
{
    public decimal? QuantidadeProducao { get; set; }
    public TipoEstoqueE TipoEstoque { get; set; }
    public TipoMateriaPrimaE TipoMateriaPrima { get; set; }

    public virtual ICollection<ReceitaMateriaPrima>? ReceitaMateriaPrima { get; set; }
}
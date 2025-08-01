using FlexPro.Domain.Abstractions;
using FlexPro.Domain.Enums;

namespace FlexPro.Domain.Entities;

public class Embalagem : Entity
{
    public int Quantidade { get; set; }
    public TipoEmbalagemE TipoEmbalagem { get; set; }
    public int Tamanho { get; set; }

    public int ProdutoLojaId { get; set; }
    public virtual ProdutoLoja? ProdutoLoja { get; set; }
}
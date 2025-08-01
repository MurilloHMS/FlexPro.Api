using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Entities;

public class Departamento : Entity
{
    public string Nome { get; set; } = string.Empty;

    public int ProdutoLojaId { get; set; }
    public virtual ICollection<ProdutoLoja>? ProdutosLoja { get; set; }
}
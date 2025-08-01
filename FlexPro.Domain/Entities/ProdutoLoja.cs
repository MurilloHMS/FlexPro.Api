namespace FlexPro.Domain.Entities;

public class ProdutoLoja : Produto
{
    public string Descricao { get; set; } = string.Empty;
    public string Cor { get; set; } = string.Empty;
    public string Diluicao { get; set; } = string.Empty;
    public byte[]? Imagem { get; set; }

    public virtual ICollection<Embalagem>? Embalagems { get; set; }
    public virtual ICollection<Departamento>? Departamentos { get; set; }
}
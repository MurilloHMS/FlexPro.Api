namespace FlexPro.Api.Domain.Entities;

public class ProdutoLoja : Produto
{
    public string Descricao { get; set; }
    public string Cor { get; set; }
    public string Diluicao { get; set; }
    public byte[] Imagem { get; set; }
    
    public virtual ICollection<Embalagem> Embalagems { get; set; }
    public virtual ICollection<Departamento> Departamentos { get; set; }
}
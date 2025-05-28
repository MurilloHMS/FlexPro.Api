namespace FlexPro.Api.Domain.Entities;

public class Embalagem
{
    public int Id { get; set; }
    public int Quantidade { get; set; }
    public TipoEmbalagem_e TipoEmbalagem { get; set; }
    public int Tamanho { get; set; }
    
    public int ProdutoLojaId { get; set; }
    public virtual ProdutoLoja ProdutoLoja { get; set; }
}
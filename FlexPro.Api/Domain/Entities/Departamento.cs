namespace FlexPro.Api.Domain.Entities;

public class Departamento
{
    public int Id { get; set; }
    public string Nome { get; set; }
    
    public int ProdutoLojaId { get; set; }
    public virtual ICollection<ProdutoLoja> ProdutosLoja { get; set; }
}
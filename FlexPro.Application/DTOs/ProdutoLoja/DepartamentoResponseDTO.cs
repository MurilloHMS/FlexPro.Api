namespace FlexPro.Application.DTOs.ProdutoLoja;

public class DepartamentoResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; } =  string.Empty;
    public int ProdutoLojaId { get; set; }
}
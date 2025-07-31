namespace FlexPro.Application.DTOs.ProdutoLoja;

public class ProdutoLojaResponseDto
{
    public int Id { get; set; }
    public string CodigoSistema { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Cor { get; set; } = string.Empty;
    public string Diluicao { get; set; } = string.Empty;
    public byte[]? Imagem { get; set; }
    public List<EmbalagemResponseDto>? Embalagems { get; set; }
    public List<DepartamentoResponseDto>? Departamentos { get; set; }
}
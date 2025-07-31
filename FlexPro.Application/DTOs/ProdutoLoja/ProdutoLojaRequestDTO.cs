using FlexPro.Domain.Entities;

namespace FlexPro.Application.DTOs.ProdutoLoja;

public class ProdutoLojaRequestDto
{
    public string CodigoSistema { get; set; } =  string.Empty;
    public string Nome { get; set; } =  string.Empty;
    public string Descricao { get; set; } =  string.Empty;
    public string Cor {get; set; } =  string.Empty;
    public string Diluicao { get; set; } =  string.Empty;
    public byte[]? Imagem { get; set; }
    public List<EmbalagemRequestDto>? Embalagems { get; set; }
    public List<Departamento>? Departamentos { get; set; }
    
}
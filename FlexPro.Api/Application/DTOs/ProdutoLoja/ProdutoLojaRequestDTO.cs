using FlexPro.Domain.Entities;

namespace FlexPro.Api.Application.DTOs.ProdutoLoja;

public class ProdutoLojaRequestDTO
{
    public string CodigoSistema { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Cor {get; set; }
    public string Diluicao { get; set; }
    public byte[] Imagem { get; set; }
    public List<EmbalagemRequestDTO> Embalagems { get; set; }
    public List<Departamento> Departamentos { get; set; }
    
}
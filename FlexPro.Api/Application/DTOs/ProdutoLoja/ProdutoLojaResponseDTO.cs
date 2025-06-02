using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.DTOs.ProdutoLoja;

public class ProdutoLojaResponseDTO
{
    public int Id { get; set; }
    public string CodigoSistema { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Cor {get; set; }
    public string Diluicao { get; set; }
    public byte[] Imagem { get; set; }
    public List<EmbalagemResponseDTO> Embalagems { get; set; }
    public List<DepartamentoResponseDTO> Departamentos { get; set; }
}
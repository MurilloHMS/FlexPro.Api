using FlexPro.Api.Domain;

namespace FlexPro.Api.Application.DTOs.ProdutoLoja;

public class EmbalagemRequestDTO
{
    public int Quantidade { get; set; }
    public TipoEmbalagem_e TipoEmbalagem { get; set; }
    public int Tamanho { get; set; }
}
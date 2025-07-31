using FlexPro.Domain.Enums;

namespace FlexPro.Application.DTOs.ProdutoLoja;

public class EmbalagemResponseDto
{
    public int Quantidade { get; set; }
    public TipoEmbalagemE TipoEmbalagem { get; set; }
    public int Tamanho { get; set; }

    public int ProdutoLojaId { get; set; }
}
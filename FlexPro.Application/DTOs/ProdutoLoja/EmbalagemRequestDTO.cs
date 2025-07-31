using FlexPro.Domain.Enums;

namespace FlexPro.Application.DTOs.ProdutoLoja;

public class EmbalagemRequestDto
{
    public int Quantidade { get; set; }
    public TipoEmbalagem_e TipoEmbalagem { get; set; }
    public int Tamanho { get; set; }
    public int ProdutoLojaId { get; set; }
}
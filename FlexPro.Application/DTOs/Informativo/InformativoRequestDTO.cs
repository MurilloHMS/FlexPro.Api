using FlexPro.Domain.Models;

namespace FlexPro.Application.DTOs.Informativo;

public class InformativoRequestDto
{
    public string Month { get; set; } = string.Empty;
    public List<InformativoNFe> InformativoNFes { get; set; } = null!;
    public List<InformativoPecasTrocadas> InformativoPecasTrocadas { get; set; } = null!;
    public List<InformativoOs> InformativoOs { get; set; } = null!;
}
using FlexPro.Domain.Models;

namespace FlexPro.Application.DTOs.Informativo;

public class InformativoRequestDto
{
    public string Month { get; set; } =  string.Empty;
    public List<InformativoNFe> InformativoNFes { get; set; }
    public List<InformativoPecasTrocadas> InformativoPecasTrocadas { get; set; }
    public List<InformativoOS> informativoOs { get; set; }
}
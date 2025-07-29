using FlexPro.Domain.Models;

namespace FlexPro.Api.Application.DTOs.Informativo;

public class InformativoRequestDTO
{
    public string Month { get; set; }
    public List<InformativoNFe> InformativoNFes { get; set; }
    public List<InformativoPecasTrocadas> InformativoPecasTrocadas { get; set; }
    public List<InformativoOS> informativoOs { get; set; }
}
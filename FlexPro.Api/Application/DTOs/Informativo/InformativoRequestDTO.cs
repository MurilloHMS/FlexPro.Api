using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.DTOs.Informativo;

public class InformativoRequestDTO
{
    public List<InformativoNFe> InformativoNFes { get; set; }
    public List<InformativoPecasTrocadas> InformativoPecasTrocadas { get; set; }
    public List<InformativoOS> informativoOs { get; set; }
}
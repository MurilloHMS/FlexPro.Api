using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Interfaces;

public interface IIcmsService
{
    Task<Stream> CalcularAsync(List<IFormFile> files);
    Task<Stream> CriarPlanilha(List<ICMS> dadosICMS);
    Task<ICMS> ProcessarXML(Stream stream);
}
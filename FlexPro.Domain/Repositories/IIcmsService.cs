using FlexPro.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace FlexPro.Domain.Repositories;

public interface IIcmsService
{
    Task<Stream> CalcularAsync(List<IFormFile> files);
    Task<Stream> CriarPlanilha(List<ICMS> dadosICMS);
    Task<ICMS> ProcessarXML(Stream stream);
}
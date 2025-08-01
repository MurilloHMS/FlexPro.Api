using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Domain.Repositories;

public interface ICalculoTransportadoraService
{
    Task<(string, decimal)> ProcessarXml(Stream file);
    Task<IActionResult> CalcularAsync(List<IFormFile> files);
}
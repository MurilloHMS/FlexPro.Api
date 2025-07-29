using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace FlexPro.Domain.Repositories;

public interface ICalculoTransportadoraService
{
    Task<(string, decimal)> ProcessarXML(Stream file);
    Task<IActionResult> CalcularAsync(List<IFormFile> files);
}
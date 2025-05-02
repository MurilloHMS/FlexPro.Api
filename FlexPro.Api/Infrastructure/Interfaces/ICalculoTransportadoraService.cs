using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Application.Interfaces;

public interface ICalculoTransportadoraService
{
    Task<(string, decimal)> ProcessarXML(Stream file);
    Task<IActionResult> CalcularAsync(List<IFormFile> files);
}
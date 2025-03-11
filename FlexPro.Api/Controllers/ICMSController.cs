using System.Xml.Linq;
using FlexPro.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ICMSController : ControllerBase
{
    [HttpPost("calcular")]
    public async Task<IActionResult> CalcularICMS(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Nenhum arquivo fornecido.");
        }

        try
        {
            ICMS dados = await ProcessarXML(file);
            return Ok(dados);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Erro ao processar o arquivo XML: {e.Message}");
        }
    }

    private async Task<ICMS> ProcessarXML(IFormFile file)
    {
        using (var stream = file.OpenReadStream())
        {
            XDocument xmlDoc = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);
            XNamespace ns = xmlDoc.Root.GetDefaultNamespace();
            
            var dados = new ICMS();
            var numNfTag = xmlDoc.Descendants(ns + "ide").FirstOrDefault();
            if (numNfTag != null)
            {
                dados.nNF = numNfTag.Element(ns + "nNF")?.Value;
            }

            var icmsTag = xmlDoc.Descendants(ns + "ICMSTot").FirstOrDefault();
            if (icmsTag != null)
            {
                dados.vICMS = decimal.TryParse(icmsTag.Element(ns + "vICMS")?.Value, out var result) ? result : 0m;
                dados.vPis = decimal.TryParse(icmsTag.Element(ns + "vPIS")?.Value, out var pisResult) ? pisResult : 0m;
                dados.vCofins = decimal.TryParse(icmsTag.Element(ns + "vCOFINS")?.Value, out var cofinsResult) ? cofinsResult : 0m;
            }

            return dados;
        }
    }
    
}
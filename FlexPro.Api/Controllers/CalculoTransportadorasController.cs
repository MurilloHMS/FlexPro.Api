using System.Globalization;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CalculoTransportadorasController : ControllerBase
{
    [HttpPost("calcular")]
    public async Task<IActionResult> CalcularAlfaTransportes(List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
        {
            return BadRequest("Nenhum arquivo encontrado");
        }
        
        StringBuilder result = new StringBuilder();
        decimal totalValorPrest = 0m;

        for (int i = 0; i < files.Count; i++)
        {
            if (files[i].Length > 0)
            {
                using (var stream = files[i].OpenReadStream())
                {
                    var (resultadoArquivo, valorPrest) = await ProcessarXml(stream);
                    result.Append(resultadoArquivo);
                    if (i < files.Count - 1)
                    {
                        result.Append(" + ");
                    }

                    result.AppendLine();
                    totalValorPrest += valorPrest;
                }
            }
        }

        result.AppendLine();
        result.AppendLine($"Total: R$ {totalValorPrest}");
        return result != null ? Ok(result.ToString()) : BadRequest();
    }
    
    private async Task <(string, decimal)> ProcessarXml(Stream file)
    {
        try
        {
            XDocument xmldoc = await XDocument.LoadAsync(file, LoadOptions.None, CancellationToken.None);
            XNamespace ns = xmldoc.Root.GetDefaultNamespace();

            var vPrest = xmldoc.Descendants(ns + "vPrest").FirstOrDefault();
            var vTPrest = 0m;

            if (vPrest != null)
            {
                string? vTPrestString = vPrest.Element(ns + "vTPrest")?.Value;
                    
                vTPrest = !string.IsNullOrEmpty(vTPrestString) && decimal.TryParse(vTPrestString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal vTPrestValue)
                    ? vTPrestValue
                    : 0m;

                return ($"R$ {vTPrest} ", vTPrest);
            }
            else
            {
                return ("", 0m);
            }
        }
        catch (Exception e)
        {
            return ("", 0m);
        }
    }
}
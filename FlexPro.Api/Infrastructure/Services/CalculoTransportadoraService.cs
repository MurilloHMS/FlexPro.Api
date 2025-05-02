using System.Globalization;
using System.Text;
using System.Xml.Linq;
using FlexPro.Api.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Infrastructure.Services;

public class CalculoTransportadoraService : ICalculoTransportadoraService
{
    public async Task<(string, decimal)> ProcessarXML(Stream file)
    {
        try
        {
            XDocument xmlDoc = await XDocument.LoadAsync(file, LoadOptions.None, CancellationToken.None);
            XNamespace ns = xmlDoc.Root.GetDefaultNamespace();
            
            var vPrest = xmlDoc.Descendants(ns + "vPrest").FirstOrDefault();
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
        catch (Exception)
        {
            return ("", 0m);
        }
    }

    public async Task<IActionResult> CalcularAsync(List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
        {
            return new BadRequestObjectResult("Nenhum arquivo foi encontrado.");
        }

        StringBuilder result = new();
        decimal totalValorPrest = 0m;

        for (int i = 0; i < files.Count; i++)
        {
            if (files[i].Length > 0)
            {
                using (var stream = files[i].OpenReadStream())
                {
                    var (resultadoArquivo, valorPrest) = await ProcessarXML(stream);
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
        return result != null
            ? new OkObjectResult(result)
            : new BadRequestObjectResult("Erro ao calcular os arquivos.");
    }
}
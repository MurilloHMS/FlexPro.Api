using System.Globalization;
using System.Text;
using System.Xml.Linq;
using FlexPro.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Infrastructure.Services;

public class CalculoTransportadoraService : ICalculoTransportadoraService
{
    public async Task<(string, decimal)> ProcessarXml(Stream file)
    {
        try
        {
            var xmlDoc = await XDocument.LoadAsync(file, LoadOptions.None, CancellationToken.None);
            var ns = xmlDoc.Root?.GetDefaultNamespace();

            var vPrest = xmlDoc.Descendants(ns! + "vPrest").FirstOrDefault();
            var vTPrest = 0m;

            if (vPrest != null)
            {
                var vTPrestString = vPrest.Element(ns! + "vTPrest")?.Value;

                vTPrest = !string.IsNullOrEmpty(vTPrestString) && decimal.TryParse(vTPrestString, NumberStyles.Any,
                    CultureInfo.InvariantCulture, out var vTPrestValue)
                    ? vTPrestValue
                    : 0m;

                return ($"R$ {vTPrest} ", vTPrest);
            }

            return ("", 0m);
        }
        catch (Exception)
        {
            return ("", 0m);
        }
    }

    public async Task<IActionResult> CalcularAsync(List<IFormFile> files)
    {
        if (files.Count == 0) return new BadRequestObjectResult("Nenhum arquivo foi encontrado.");

        StringBuilder result = new();
        var totalValorPrest = 0m;

        for (var i = 0; i < files.Count; i++)
            if (files[i].Length > 0)
                using (var stream = files[i].OpenReadStream())
                {
                    var (resultadoArquivo, valorPrest) = await ProcessarXml(stream);
                    result.Append(resultadoArquivo);
                    if (i < files.Count - 1) result.Append(" + ");

                    result.AppendLine();
                    totalValorPrest += valorPrest;
                }

        result.AppendLine();
        result.AppendLine($"Total: R$ {totalValorPrest}");
        return result != null!
            ? new OkObjectResult(result)
            : new BadRequestObjectResult("Erro ao calcular os arquivos.");
    }
}
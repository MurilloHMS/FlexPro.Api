using System.Globalization;
using System.Xml.Linq;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using FlexPro.Api.Models;
using Microsoft.AspNetCore.Mvc;
using LoadOptions = System.Xml.Linq.LoadOptions;

namespace FlexPro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ICMSController : ControllerBase
{
    [HttpPost("calcular")]
    public async Task<IActionResult> CalcularICMS(List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
        {
            return BadRequest("Nenhum arquivo enviado.");
        }

        var dadosICMS = new List<ICMS>();

        foreach (var file in files)
        {
            if (Path.GetExtension(file.FileName).ToLower() != ".xml")
            {
                continue;
            }

            using (var stream = file.OpenReadStream())
            {
                var dados = await ProcessarXML(stream);
                if (dados != null)
                {
                    dadosICMS.Add(dados);
                }
            }
        }

        if (!dadosICMS.Any())
        {
            return BadRequest("Nenhuma informação valida encontrada nos arquivos");
        }
        
        var memoryStream = new MemoryStream();
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("ICMS");
            
            worksheet.Cell(1, 1).Value = "Número da NFe";
            worksheet.Cell(1, 2).Value = "Valor do ICMS";
            worksheet.Cell(1, 3).Value = "Valor do Pis";
            worksheet.Cell(1, 4).Value = "Valor do Cofins";

            int novaLinha = 2;

            foreach (var linha in dadosICMS)
            {
                worksheet.Cell(novaLinha, 1).Value = int.TryParse(linha.nNF, out var valor) ? valor : 0;
                worksheet.Cell(novaLinha, 2).Value = linha.vICMS;
                worksheet.Cell(novaLinha, 3).Value = linha.vPis;
                worksheet.Cell(novaLinha, 4).Value = linha.vCofins;
                novaLinha++;
            }

            workbook.SaveAs(memoryStream);
        }
        
        memoryStream.Seek(0, SeekOrigin.Begin);
        
        return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ICMS-{DateTime.Now:dd-MM-yyyy}.xlsx");
    }

    private async Task<ICMS> ProcessarXML(Stream stream)
    {
        try
        {
            var xmlDoc = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);
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
                string icms = icmsTag.Element(ns + "vICMS")?.Value;
                dados.vICMS = !string.IsNullOrEmpty(icms) && decimal.TryParse(icms, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result) ? result : 0m;

                string pis = icmsTag.Element(ns + "vPIS").Value;
                dados.vPis = !string.IsNullOrEmpty(pis) && decimal.TryParse(pis, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal pisResult) ? pisResult : 0m;

                string cofins = icmsTag.Element(ns + "vCOFINS").Value;
                dados.vCofins = !string.IsNullOrEmpty(cofins) && decimal.TryParse(cofins, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal cofinsResult) ? cofinsResult : 0m;
            }

            return dados;
        }
        catch (Exception e)
        {
            return null;
        }
    }
    
}
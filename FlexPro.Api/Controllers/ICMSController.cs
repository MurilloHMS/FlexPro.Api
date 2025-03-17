using ClosedXML.Excel;
using FlexPro.Api.Models;
using Microsoft.AspNetCore.Mvc;
using FlexPro.Api.Services;

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
                var dados = await IcmsService.ProcessarXML(stream);
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

}
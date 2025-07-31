using System.Globalization;
using System.Xml.Linq;
using ClosedXML.Excel;
using FlexPro.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using LoadOptions = System.Xml.Linq.LoadOptions;

namespace FlexPro.Api.Controllers;

// TODO: Migrate this to Mediator Pattern
[Route("api/[controller]")]
[ApiController]
public class ColetarDadosNFeController : ControllerBase
{
    [HttpPost("upload")]
    public async Task<ActionResult> ColetarDadosNFe(List<IFormFile> files)
    {
        if (files.Count == 0) return BadRequest("Nenhum arquivo enviado.");

        List<DadosNotasFiscais> dadosDaNotaFiscal = new();

        foreach (var file in files)
        {
            if (Path.GetExtension(file.FileName).ToLower() != ".xml") continue;

            using (var stream = file.OpenReadStream())
            {
                var dados = await ProcessarXml(stream);
                if (dados != null && dados.Any())
                    foreach (var item in dados)
                        dadosDaNotaFiscal.Add(item);
            }
        }

        if (!dadosDaNotaFiscal.Any()) return BadRequest("Nenhuma informação valida nos arquivos");

        var memoryStream = new MemoryStream();
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Dados Notas Fiscais");

            worksheet.Cell(1, 1).Value = "Fornecedor";
            worksheet.Cell(1, 2).Value = "Número NFe";
            worksheet.Cell(1, 3).Value = "Data Emissão";
            worksheet.Cell(1, 4).Value = "Produto";
            worksheet.Cell(1, 5).Value = "Valor Unitário";
            worksheet.Cell(1, 6).Value = "Valor Total";
            worksheet.Cell(1, 7).Value = "CFOP";

            var novaLinha = 2;

            foreach (var linha in dadosDaNotaFiscal)
            {
                worksheet.Cell(novaLinha, 1).Value = linha.Fornecedor;
                worksheet.Cell(novaLinha, 2).Value = int.TryParse(linha.NumeroNota, out var numNFe) ? numNFe : default;
                worksheet.Cell(novaLinha, 3).Value = linha.DataNota;
                worksheet.Cell(novaLinha, 4).Value = linha.Produto;
                worksheet.Cell(novaLinha, 5).Value = linha.ValorUnitario;
                worksheet.Cell(novaLinha, 6).Value = linha.ValorTotal;
                worksheet.Cell(novaLinha, 7).Value = linha.Cfop;

                novaLinha++;
            }

            workbook.SaveAs(memoryStream);
        }

        memoryStream.Seek(0, SeekOrigin.Begin);

        return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"DadosNFe-{DateTime.Now:dd-MM-yyyy}.xlsx");
    }

    private async Task<List<DadosNotasFiscais>?> ProcessarXml(Stream stream)
    {
        try
        {
            var xmlDoc = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);
            var ns = xmlDoc.Root?.GetDefaultNamespace();

            List<DadosNotasFiscais> dadosDaNota = new();
            var prodTag = xmlDoc.Descendants(ns! + "prod");
            if (prodTag.Any())
                foreach (var item in prodTag)
                {
                    var dados = new DadosNotasFiscais();
                    if (ns != null)
                    {
                        var numNfTag = xmlDoc.Descendants(ns + "ide").FirstOrDefault();
                        if (numNfTag != null)
                        {
                            dados.NumeroNota = numNfTag.Element(ns + "nNF")?.Value;
                            dados.DataNota = DateTime.TryParse(numNfTag.Element(ns + "dhEmi")?.Value, out var dataNota)
                                ? dataNota
                                : default;
                        }
                    
                        var emitenteTag = xmlDoc.Descendants(ns + "emit").FirstOrDefault();
                        if (emitenteTag != null) dados.Fornecedor = emitenteTag.Element(ns + "xNome")?.Value;
                    
                        var produto = item.Element(ns + "xProd")?.Value;
                        dados.Produto = !string.IsNullOrEmpty(produto) ? produto : default;
                    
                        var valorUnitario = item.Element(ns + "vUnCom")?.Value;
                        dados.ValorUnitario = !string.IsNullOrEmpty(valorUnitario) && decimal.TryParse(valorUnitario,
                            NumberStyles.Any, CultureInfo.InvariantCulture, out var unitResult)
                            ? unitResult
                            : 0m;
                    
                        var cfop = item.Element(ns + "CFOP")?.Value;
                        dados.Cfop = !string.IsNullOrEmpty(cfop) ? cfop : default;
                    
                        var valorTotalProduto = item.Element(ns + "vProd")?.Value;
                        dados.ValorTotal = !string.IsNullOrEmpty(valorTotalProduto) && decimal.TryParse(valorTotalProduto,
                            NumberStyles.Any, CultureInfo.InvariantCulture, out var valorTotalResult)
                            ? valorTotalResult
                            : 0m;
                    }
                    else
                    {
                        return null;
                    }

                    dadosDaNota.Add(dados);
                }

            return dadosDaNota;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
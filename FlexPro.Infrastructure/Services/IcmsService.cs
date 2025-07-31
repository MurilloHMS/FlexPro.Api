using System.Globalization;
using System.Xml.Linq;
using ClosedXML.Excel;
using FlexPro.Domain.Models;
using FlexPro.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using LoadOptions = System.Xml.Linq.LoadOptions;

namespace FlexPro.Infrastructure.Services;

public class IcmsService : IIcmsService
{
    public async Task<Stream> CalcularAsync(List<IFormFile> files)
    {
        if (files == null || files.Count == 0) return new MemoryStream();

        var dadosICMS = new List<ICMS>();

        foreach (var file in files)
        {
            if (Path.GetExtension(file.FileName).ToLower() != ".xml") continue;

            using (var stream = file.OpenReadStream())
            {
                var dados = await ProcessarXML(stream);
                if (dados != null) dadosICMS.Add(dados);
            }
        }

        var arquivo = await CriarPlanilha(dadosICMS);
        return arquivo;
    }

    public async Task<Stream> CriarPlanilha(List<ICMS> dadosICMS)
    {
        if (!dadosICMS.Any()) return new MemoryStream();

        var memoryStream = new MemoryStream();
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("ICMS");

            worksheet.Cell(1, 1).Value = "Número da NFe";
            worksheet.Cell(1, 2).Value = "Valor do ICMS";
            worksheet.Cell(1, 3).Value = "Valor do Pis";
            worksheet.Cell(1, 4).Value = "Valor do Cofins";

            var novaLinha = 2;

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

        return memoryStream;
    }

    public async Task<ICMS> ProcessarXML(Stream stream)
    {
        try
        {
            var xmlDoc = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);
            var ns = xmlDoc.Root.GetDefaultNamespace();

            var dados = new ICMS();
            var numNfTag = xmlDoc.Descendants(ns + "ide").FirstOrDefault();
            if (numNfTag != null) dados.nNF = numNfTag.Element(ns + "nNF")?.Value;

            var icmsTag = xmlDoc.Descendants(ns + "ICMSTot").FirstOrDefault();
            if (icmsTag != null)
            {
                var icms = icmsTag.Element(ns + "vICMS")?.Value;
                dados.vICMS =
                    !string.IsNullOrEmpty(icms) && decimal.TryParse(icms, NumberStyles.Any,
                        CultureInfo.InvariantCulture, out var result)
                        ? result
                        : 0m;

                var pis = icmsTag.Element(ns + "vPIS").Value;
                dados.vPis =
                    !string.IsNullOrEmpty(pis) && decimal.TryParse(pis, NumberStyles.Any, CultureInfo.InvariantCulture,
                        out var pisResult)
                        ? pisResult
                        : 0m;

                var cofins = icmsTag.Element(ns + "vCOFINS").Value;
                dados.vCofins = !string.IsNullOrEmpty(cofins) && decimal.TryParse(cofins, NumberStyles.Any,
                    CultureInfo.InvariantCulture, out var cofinsResult)
                    ? cofinsResult
                    : 0m;
            }

            return dados;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}
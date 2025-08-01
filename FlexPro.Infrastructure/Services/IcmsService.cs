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
        if (files.Count == 0) return new MemoryStream();

        var dadosIcms = new List<Icms?>();

        foreach (var file in files)
        {
            if (Path.GetExtension(file.FileName).ToLower() != ".xml") continue;

            using (var stream = file.OpenReadStream())
            {
                var dados = await ProcessarXml(stream);
                dadosIcms.Add(dados);
            }
        }

        var arquivo = await CriarPlanilha(dadosIcms);
        return arquivo;
    }

    public Task<Stream> CriarPlanilha(List<Icms?> dadosIcms)
    {
        if (!dadosIcms.Any()) return Task.FromResult<Stream>(new MemoryStream());

        var memoryStream = new MemoryStream();
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("ICMS");

            worksheet.Cell(1, 1).Value = "Número da NFe";
            worksheet.Cell(1, 2).Value = "Valor do ICMS";
            worksheet.Cell(1, 3).Value = "Valor do Pis";
            worksheet.Cell(1, 4).Value = "Valor do Cofins";

            var novaLinha = 2;

            foreach (var linha in dadosIcms)
            {
                worksheet.Cell(novaLinha, 1).Value = int.TryParse(linha?.NNf, out var valor) ? valor : 0;
                worksheet.Cell(novaLinha, 2).Value = linha?.VIcms;
                worksheet.Cell(novaLinha, 3).Value = linha?.VPis;
                worksheet.Cell(novaLinha, 4).Value = linha?.VCofins;
                novaLinha++;
            }

            workbook.SaveAs(memoryStream);
        }

        memoryStream.Seek(0, SeekOrigin.Begin);

        return Task.FromResult<Stream>(memoryStream);
    }

    public async Task<Icms?> ProcessarXml(Stream stream)
    {
        try
        {
            var xmlDoc = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);
            var ns = xmlDoc.Root?.GetDefaultNamespace();

            var dados = new Icms();
            var numNfTag = xmlDoc.Descendants(ns! + "ide").FirstOrDefault();
            if (numNfTag != null) dados.NNf = numNfTag.Element(ns! + "nNF")?.Value;

            var icmsTag = xmlDoc.Descendants(ns! + "ICMSTot").FirstOrDefault();
            if (icmsTag != null)
            {
                var icms = icmsTag.Element(ns! + "vICMS")?.Value;
                dados.VIcms =
                    !string.IsNullOrEmpty(icms) && decimal.TryParse(icms, NumberStyles.Any,
                        CultureInfo.InvariantCulture, out var result)
                        ? result
                        : 0m;

                var pis = icmsTag.Element(ns! + "vPIS")?.Value;
                dados.VPis =
                    !string.IsNullOrEmpty(pis) && decimal.TryParse(pis, NumberStyles.Any, CultureInfo.InvariantCulture,
                        out var pisResult)
                        ? pisResult
                        : 0m;

                var cofins = icmsTag.Element(ns! + "vCOFINS")?.Value;
                dados.VCofins = !string.IsNullOrEmpty(cofins) && decimal.TryParse(cofins, NumberStyles.Any,
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
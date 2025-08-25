using ClosedXML.Excel;
using FlexPro.Domain.Interfaces.Nfe;
using FlexPro.Domain.Models;

namespace FlexPro.Infrastructure.Services.Nfe;

public class NfeWriterService : INfeWriter
{
    public void SaveIcmsData( string outputPath, List<NfeIcmsInfo> icmsInfos)
    {
        if (!icmsInfos.Any()) throw new ArgumentException("Dados Icms Inválidos");
        
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("ICMS");

            worksheet.Cell(1, 1).Value = "Número da NFe";
            worksheet.Cell(1, 2).Value = "Valor do ICMS";
            worksheet.Cell(1, 3).Value = "Valor do Pis";
            worksheet.Cell(1, 4).Value = "Valor do Cofins";

            var novaLinha = 2;

            foreach (var linha in icmsInfos)
            {
                worksheet.Cell(novaLinha, 1).Value = int.TryParse(linha?.NNf, out var valor) ? valor : 0;
                worksheet.Cell(novaLinha, 2).Value = linha?.VIcms;
                worksheet.Cell(novaLinha, 3).Value = linha?.VPis;
                worksheet.Cell(novaLinha, 4).Value = linha?.VCofins;
                novaLinha++;
            }
            workbook.SaveAs(outputPath);
        }
    }
}
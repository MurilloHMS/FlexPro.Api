using FlexPro.Domain.Models;
using PdfSharp.Pdf.IO;
using UglyToad.PdfPig;

namespace FlexPro.Infrastructure.Services;

public class SepararPdfService
{
    public static List<SepararPdf> GetPdfByPage(string inputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
            return new List<SepararPdf>();

        var files = new List<SepararPdf>();

        using (var document = PdfDocument.Open(inputPdfPath))
        {
            var pageIndex = 0;
            foreach (var page in document.GetPages())
            {
                var textPagina = page.Text;
                var nomeFuncionario = ExtrairNomeFuncionario(textPagina);

                files.Add(new SepararPdf
                {
                    Nome = !string.IsNullOrEmpty(nomeFuncionario) ? nomeFuncionario : $"Pagina {pageIndex + 1}"
                });

                pageIndex++;
            }
        }

        return files;
    }

    public static void SeparatedPdfByPage(string inputPdfPath, string outputFolder, List<SepararPdf> lista)
    {
        if (!File.Exists(inputPdfPath) || lista == null || lista.Count == 0)
            return;

        Directory.CreateDirectory(outputFolder);

        using (var inputDocument = PdfReader.Open(inputPdfPath, PdfDocumentOpenMode.Import))
        {
            for (var pageNumber = 0; pageNumber < inputDocument.PageCount; pageNumber++)
                using (var outputDocument = new PdfSharp.Pdf.PdfDocument())
                {
                    outputDocument.AddPage(inputDocument.Pages[pageNumber]);

                    var nomeArquivo = SanitizeFileName(lista[pageNumber].Nome);
                    var outputFilePath = Path.Combine(outputFolder, $"{nomeArquivo}.pdf");

                    outputDocument.Save(outputFilePath);
                }
        }
    }

    private static string ExtrairNomeFuncionario(string text)
    {
        var key = "FL";
        var indexName = text.IndexOf(key, StringComparison.OrdinalIgnoreCase);

        if (indexName != -1)
        {
            var start = indexName + key.Length;
            var restante = text.Substring(start).Trim();
            var parts = restante.Split(' ');

            var nameParts = new List<string>();
            var foundName = false;
            foreach (var part in parts)
            {
                if (!foundName && int.TryParse(part, out _))
                    continue;

                foundName = true;

                if (int.TryParse(part, out _))
                    break;

                nameParts.Add(part);
            }

            return string.Join(' ', nameParts).Trim();
        }

        return string.Empty;
    }

    private static string SanitizeFileName(string fileName)
    {
        foreach (var c in Path.GetInvalidFileNameChars()) fileName = fileName.Replace(c.ToString(), "_");
        return fileName;
    }
}
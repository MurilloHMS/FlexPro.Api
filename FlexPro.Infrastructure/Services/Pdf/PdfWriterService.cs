using FlexPro.Domain.Interfaces.Pdf;
using FlexPro.Domain.Models;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace FlexPro.Infrastructure.Services.Pdf;

public class PdfWriterService(IFileNameSanitizer fileNameSanitizer) : IPdfWriter
{
    public void SavePages(string inputPdfPath, string outputFolder, List<PdfPageInfo> pages)
    {
        if (!File.Exists(inputPdfPath) || pages.Count == 0)
            return;

        try
        {
            Directory.CreateDirectory(outputFolder);
            using (var inputDocument = PdfReader.Open(inputPdfPath, PdfDocumentOpenMode.Import))
            {
                if(pages.Count != inputDocument.PageCount)
                    throw new ArgumentException("The number of pages does not match the number of pages in the input document.");

                for (var pageNumber = 0; pageNumber < inputDocument.PageCount; pageNumber++)
                {
                    using (var outputDocument = new PdfDocument())
                    {
                        outputDocument.AddPage(inputDocument.Pages[pageNumber]);
                        var fileName = fileNameSanitizer.Sanitize(pages[pageNumber].Name);
                        var outputFilePath = Path.Combine(outputFolder, $"{fileName}.pdf");
                        outputDocument.Save(outputFilePath);;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Erro ao salvar PDFs em: {outputFolder}", ex);
        }
    }
}
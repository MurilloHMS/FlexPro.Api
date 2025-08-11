using FlexPro.Domain.Interfaces.Pdf;
using FlexPro.Domain.Models;
using UglyToad.PdfPig;

namespace FlexPro.Infrastructure.Services.Pdf;

public class PdfReaderService(INameExtractor nameExtractor) : IPdfReader
{
    public List<PdfPageInfo> GetPdfByPage(string inputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException("Pdf file not found", inputPdfPath);

        var files = new List<PdfPageInfo>();
        try
        {
            using (var document = PdfDocument.Open(inputPdfPath))
            {
                var pageIndex = 0;
                foreach (var page in document.GetPages())
                {
                    var textPage = page.Text;
                    var employeeName = nameExtractor.ExtractName(textPage);
                    files.Add(new  PdfPageInfo
                    {
                        Name = !string.IsNullOrEmpty(employeeName) ? employeeName : $"Page_{pageIndex++}",
                    });
                    pageIndex++;
                }
            }

            return files;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error reading file {inputPdfPath}", e);
        }
    }
}
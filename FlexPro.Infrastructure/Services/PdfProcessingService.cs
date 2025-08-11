using FlexPro.Domain.Interfaces;
using FlexPro.Domain.Interfaces.Pdf;
using FlexPro.Domain.Models;

namespace FlexPro.Application.Services;

public class PdfProcessingService(IPdfReader pdfReader, IPdfWriter pdfWriter) : IPdfProcessing
{
    public List<PdfPageInfo> GetPdfByPage(string inputPdfPath)
    {
        return pdfReader.GetPdfByPage(inputPdfPath);
    }

    public void SavePages(string inputPdfPath, string outputFolder, List<PdfPageInfo> pages)
    {
        pdfWriter.SavePages(inputPdfPath, outputFolder, pages);
    }
}
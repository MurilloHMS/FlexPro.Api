using FlexPro.Domain.Models;

namespace FlexPro.Domain.Interfaces;

public interface IPdfProcessing
{
    List<PdfPageInfo> GetPdfByPage(string inputPdfPath);
    void SavePages(string inputPdfPath, string outputFolder, List<PdfPageInfo> pages);
}
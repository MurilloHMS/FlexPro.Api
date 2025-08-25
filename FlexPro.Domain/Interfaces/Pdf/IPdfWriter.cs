using FlexPro.Domain.Models;

namespace FlexPro.Domain.Interfaces.Pdf;

public interface IPdfWriter
{
    void SavePages(string inputPdfPath, string outputPdfPath, List<PdfPageInfo> pages);
}
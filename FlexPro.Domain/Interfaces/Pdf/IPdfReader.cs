using FlexPro.Domain.Models;

namespace FlexPro.Domain.Interfaces.Pdf;

public interface IPdfReader
{
    List<PdfPageInfo> GetPdfByPage(string inputPdfPath);
}
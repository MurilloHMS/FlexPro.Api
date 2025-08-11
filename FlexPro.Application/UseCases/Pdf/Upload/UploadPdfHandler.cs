using FlexPro.Domain.Interfaces;
using FlexPro.Domain.Models;
using MediatR;

namespace FlexPro.Application.UseCases.Pdf.Upload;

public class UploadPdfHandler(IPdfProcessing pdfProcessingService, IFileStorageService fileStorage) : IRequestHandler<UploadPdfCommand, List<PdfPageInfo>>
{
    public async Task<List<PdfPageInfo>> Handle(UploadPdfCommand request, CancellationToken cancellationToken)
    {
        var tempFilePath = await fileStorage.SaveTemporaryFileAsync(request.FileName);
        try
        {
            var pages = pdfProcessingService.GetPdfByPage(tempFilePath);
            return pages;

        }
        finally
        {
            fileStorage.DeleteTemporaryFiles(tempFilePath, string.Empty, string.Empty);
        }
    }
}
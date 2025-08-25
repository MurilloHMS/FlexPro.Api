using FlexPro.Domain.Interfaces;
using MediatR;

namespace FlexPro.Application.UseCases.Pdf.Save;

public class SavePdfHandler(IPdfProcessing pdfProcessingService, IFileStorageService fileStorageService) :  IRequestHandler<SavePdfCommand, SavePdfResult>
{
    public async Task<SavePdfResult> Handle(SavePdfCommand request, CancellationToken cancellationToken)
    {
        var outputFolder = Path.Combine(Path.GetTempPath(), $"Separated_{Guid.NewGuid()}");
        var inputPdfPath = Path.Combine(Path.GetTempPath(), "input.pdf");

        try
        {
            pdfProcessingService.SavePages(inputPdfPath, outputFolder, request.Pages);
            var zipBytes = await fileStorageService.CreateZipFromDirectoryAsync(outputFolder, $"PDF_Separados{Guid.NewGuid()}.zip");
            return new SavePdfResult(zipBytes);
        }
        finally
        {
            fileStorageService.DeleteTemporaryFiles(inputPdfPath, outputFolder, String.Empty);
        }
    }
}
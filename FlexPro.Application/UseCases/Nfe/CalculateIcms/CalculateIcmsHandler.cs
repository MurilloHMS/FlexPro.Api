using FlexPro.Domain.Interfaces;
using FlexPro.Domain.Interfaces.Nfe;
using MediatR;

namespace FlexPro.Application.UseCases.Nfe.CalculateIcms;

public class CalculateIcmsHandler(INfeProcessing nfeProcessing, IFileStorageService fileStorageService) : IRequestHandler<CalculateIcmsCommand, CalculateIcmsResult>
{
    public async Task<CalculateIcmsResult> Handle(CalculateIcmsCommand request, CancellationToken cancellationToken)
    {
        var outputFilePath = Path.Combine(Path.GetTempPath(), "input.xlsx");
        
        try
        {
            await nfeProcessing.GetData(request.Files, outputFilePath);
            var fileBytes = File.ReadAllBytes(outputFilePath);
            return new CalculateIcmsResult(fileBytes);
        }
        finally
        {
            fileStorageService.DeleteTemporaryFiles(outputFilePath, string.Empty, string.Empty);
        }
    }
}
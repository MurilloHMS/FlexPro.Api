using FlexPro.Domain.Interfaces.Pdf;

namespace FlexPro.Infrastructure.Services.Pdf;

public class FileNameSanitizerService : IFileNameSanitizer
{
    public string Sanitize(string fileName)
    {
        foreach (var c in Path.GetInvalidFileNameChars())
            fileName = fileName.Replace(c.ToString(), "_");
        return fileName;
    }
}
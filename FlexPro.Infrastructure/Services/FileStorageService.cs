using System.IO.Compression;
using FlexPro.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FlexPro.Infrastructure.Services;

public class FileStorageService : IFileStorageService
{
    public async Task<string> SaveTemporaryFileAsync(IFormFile file)
    {
        var tempFileName = $"{Guid.NewGuid()}.pdf";
        var tempFilePath = Path.Combine(Path.GetTempPath(), tempFileName);

        using (var stream = new FileStream(tempFilePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        
        return tempFileName;
    }

    public Task<byte[]> CreateZipFromDirectoryAsync(string directoryPath, string zipFileName)
    {
        var zipFilePath = Path.Combine(Path.GetTempPath(), zipFileName);
        if (File.Exists(zipFilePath))
            File.Delete(zipFilePath);
        
        ZipFile.CreateFromDirectory(directoryPath, zipFilePath);
        var zipBytes = File.ReadAllBytesAsync(zipFilePath);
        return zipBytes;
    }

    public void DeleteTemporaryFiles(string filePath, string directoryPath, string zipFilePath)
    {
        if (Directory.Exists(directoryPath))
            Directory.Delete(directoryPath, true);
        if (File.Exists(filePath))
            File.Delete(filePath);
        if (File.Exists(zipFilePath))
            File.Delete(zipFilePath);
    }
}
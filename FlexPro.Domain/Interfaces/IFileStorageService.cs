using Microsoft.AspNetCore.Http;

namespace FlexPro.Domain.Interfaces;

public interface IFileStorageService
{
    Task<string> SaveTemporaryFileAsync(IFormFile file);
    Task<byte[]> CreateZipFromDirectoryAsync(string directoryPath, string zipFileName);
    void DeleteTemporaryFiles(string filePath, string directoryPath, string zipFilePath);
}
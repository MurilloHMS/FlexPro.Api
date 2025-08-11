using FlexPro.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace FlexPro.Domain.Interfaces.Nfe;

public interface INfeProcessing
{
    Task GetData(List<IFormFile> files, string outputFilePath);
}
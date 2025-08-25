using FlexPro.Domain.Interfaces.Nfe;
using FlexPro.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace FlexPro.Infrastructure.Services.Nfe;

public class NfeProcessingService(INfeReader nfeReader, INfeWriter nfeWriter) : INfeProcessing
{
    public async Task GetData(List<IFormFile> files, string outputFilePath)
    {
        var icmsData = new List<NfeIcmsInfo>();
        foreach (var file in files)
        {
            if (Path.GetExtension(file.FileName).ToLower() != ".xml") continue;

            using (var stream = file.OpenReadStream())
            {
                var infos = await nfeReader.GetIcmsInfoByXml(stream);
                icmsData.Add(infos);
            }
        }
        nfeWriter.SaveIcmsData(outputFilePath, icmsData);
    }
}
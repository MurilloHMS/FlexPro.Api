using FlexPro.Domain.Models;

namespace FlexPro.Domain.Interfaces.Nfe;

public interface INfeWriter
{
    void SaveIcmsData(string outputPath, List<NfeIcmsInfo> icmsInfos);
}
using FlexPro.Domain.Models;

namespace FlexPro.Domain.Interfaces.Nfe;

public interface INfeReader
{
    Task<NfeIcmsInfo> GetIcmsInfoByXml(Stream stream);
}
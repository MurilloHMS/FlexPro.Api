using System.Globalization;
using System.Xml.Linq;
using FlexPro.Domain.Interfaces.Nfe;
using FlexPro.Domain.Models;

namespace FlexPro.Infrastructure.Services.Nfe;

public class NfeReaderService :  INfeReader
{
    public async Task<NfeIcmsInfo> GetIcmsInfoByXml(Stream stream)
    {
        try
        {
            var xmlDoc = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);
            var ns = xmlDoc.Root?.GetDefaultNamespace();

            var dados = new NfeIcmsInfo();
            var numNfTag = xmlDoc.Descendants(ns! + "ide").FirstOrDefault();
            if (numNfTag != null) dados.NNf = numNfTag.Element(ns! + "nNF")?.Value;

            var icmsTag = xmlDoc.Descendants(ns! + "ICMSTot").FirstOrDefault();
            if (icmsTag != null)
            {
                var icms = icmsTag.Element(ns! + "vICMS")?.Value;
                dados.VIcms =
                    !string.IsNullOrEmpty(icms) && decimal.TryParse(icms, NumberStyles.Any,
                        CultureInfo.InvariantCulture, out var result)
                        ? result
                        : 0m;

                var pis = icmsTag.Element(ns! + "vPIS")?.Value;
                dados.VPis =
                    !string.IsNullOrEmpty(pis) && decimal.TryParse(pis, NumberStyles.Any, CultureInfo.InvariantCulture,
                        out var pisResult)
                        ? pisResult
                        : 0m;

                var cofins = icmsTag.Element(ns! + "vCOFINS")?.Value;
                dados.VCofins = !string.IsNullOrEmpty(cofins) && decimal.TryParse(cofins, NumberStyles.Any,
                    CultureInfo.InvariantCulture, out var cofinsResult)
                    ? cofinsResult
                    : 0m;
            }

            return dados;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("An erro as acurred to colect data");
        }
    }
}
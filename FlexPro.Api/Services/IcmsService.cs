using System.Globalization;
using System.Xml.Linq;
using FlexPro.Api.Models;

namespace FlexPro.Api.Services
{
    public class IcmsService
    {
        public static async Task<ICMS> ProcessarXML(Stream stream)
        {
            try
            {
                var xmlDoc = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);
                XNamespace ns = xmlDoc.Root.GetDefaultNamespace();

                var dados = new ICMS();
                var numNfTag = xmlDoc.Descendants(ns + "ide").FirstOrDefault();
                if (numNfTag != null)
                {
                    dados.nNF = numNfTag.Element(ns + "nNF")?.Value;
                }

                var icmsTag = xmlDoc.Descendants(ns + "ICMSTot").FirstOrDefault();
                if (icmsTag != null)
                {
                    string icms = icmsTag.Element(ns + "vICMS")?.Value;
                    dados.vICMS = !string.IsNullOrEmpty(icms) && decimal.TryParse(icms, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result) ? result : 0m;

                    string pis = icmsTag.Element(ns + "vPIS").Value;
                    dados.vPis = !string.IsNullOrEmpty(pis) && decimal.TryParse(pis, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal pisResult) ? pisResult : 0m;

                    string cofins = icmsTag.Element(ns + "vCOFINS").Value;
                    dados.vCofins = !string.IsNullOrEmpty(cofins) && decimal.TryParse(cofins, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal cofinsResult) ? cofinsResult : 0m;
                }

                return dados;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}

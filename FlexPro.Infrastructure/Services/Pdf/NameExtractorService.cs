using FlexPro.Domain.Interfaces.Pdf;

namespace FlexPro.Infrastructure.Services.Pdf;

public class NameExtractorService : INameExtractor
{
    public string ExtractName(string text)
    {
        var key = "FL";
        var indexName = text.IndexOf(key, StringComparison.OrdinalIgnoreCase);

        if (indexName != -1)
        {
            var start = indexName + key.Length;
            var end = text.Substring(start).Trim();
            var parts = end.Split(' ');
            
            var nameParts = new List<string>();
            var foundName = false;
            foreach (var part in parts)
            {
                if(!foundName && int.TryParse(part, out _))
                    continue;
                
                foundName = true;

                if (int.TryParse(part, out _))
                    break;
                
                nameParts.Add(part);
            }
            return string.Join(" ", nameParts).Trim();
        }
        return string.Empty;
    }
}
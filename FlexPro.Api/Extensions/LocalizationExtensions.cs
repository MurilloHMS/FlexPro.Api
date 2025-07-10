using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace FlexPro.Api.Extensions;

public static class LocalizationExtensions
{
    public static RequestLocalizationOptions GetLocalizationOptions()
    {
        var supportedCultures = new[] { "pt-BR" };
        return new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("pt-BR"),
            SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList(),
            SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList()
        };
    }
}
using System.Globalization;
using AspNetCore.Localization.WebApi.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace AspNetCore.Localization.WebApi.Middlewares;

public class LocalizationMiddleware
{
    private const string DefaultCulture = "zh-TW";

    public static readonly CultureInfo[] SupportedCultures =
    [
        new("zh-TW"),
        new("zh-CN"),
        new("en-US"),
    ];

    public void Configure(IApplicationBuilder app)
    {
        var options = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(DefaultCulture),
            // Formatting numbers, dates, etc.
            SupportedCultures = SupportedCultures,
            // UI strings that we have localized.
            SupportedUICultures = SupportedCultures
        };
        options.RequestCultureProviders.Clear();
        options.RequestCultureProviders.Add(new RouteCultureProvider());

        app.UseRequestLocalization(options);
    }
}

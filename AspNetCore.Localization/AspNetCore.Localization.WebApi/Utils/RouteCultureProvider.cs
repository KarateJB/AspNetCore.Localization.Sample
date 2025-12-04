using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace AspNetCore.Localization.WebApi.Utils;

public class RouteCultureProvider : RequestCultureProvider
{
    private const string DefaultCulture = "zh-TW";

    public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        string? finalCulture = null;

        try
        {
            using var routeMatcher = new RouteMatcher();
            var path = httpContext.Request.Path;
            const string template = "api/Locale/Get/{locale}";
            var routeValues = routeMatcher.Matches(template, path.Value ?? string.Empty);
            if (routeValues.TryGetValue("locale", out var culture))
            {
                finalCulture = culture as string;
            }
        }
        catch
        {
            finalCulture = DefaultCulture;
        }

        finalCulture ??= DefaultCulture;

        return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(finalCulture));
    }
}
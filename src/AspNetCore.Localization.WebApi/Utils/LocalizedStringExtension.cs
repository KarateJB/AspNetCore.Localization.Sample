using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Localization;

namespace AspNetCore.Localization.WebApi.Utils;

public static class LocalizedStringExtension
{
    private static readonly JsonSerializerOptions CamelCaseOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private static readonly JsonSerializerOptions DefaultOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public static Task<string> ToJsonStringAsync(this IEnumerable<LocalizedString> source, bool isCamelLowerCaseForKey, string prefixKey = "")
    {
        var dictionary = source.ToDictionary(x => x.Name, x => x.Value);

        var options = isCamelLowerCaseForKey ? CamelCaseOptions : DefaultOptions;
        var json = JsonSerializer.Serialize(dictionary, options);

        if (!string.IsNullOrEmpty(prefixKey))
        {
            json = $"{{\"{prefixKey}\":{json}}}";
        }

        return Task.FromResult(json);
    }
}

using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;

namespace AspNetCore.Localization.WebApi.Utils;

public class RouteMatcher : IDisposable
{
    /// <summary>
    /// Match the request path with route template
    /// </summary>
    /// <param name="routeTemplate">Route template</param>
    /// <param name="requestPath">Route path</param>
    /// <returns>RouteValueDictionary</returns>
    public RouteValueDictionary Matches(string routeTemplate, string requestPath)
    {
        var template = TemplateParser.Parse(routeTemplate);
        var matcher = new TemplateMatcher(template, GetDefaults(template));
        var values = new RouteValueDictionary();
        matcher.TryMatch(requestPath, values);
        return values;
    }

    /// <summary>
    /// Extracts the default argument values from the template
    /// </summary>
    /// <param name="parsedTemplate">Route template</param>
    /// <returns>RouteValueDictionary</returns>
    private static RouteValueDictionary GetDefaults(RouteTemplate parsedTemplate)
    {
        var result = new RouteValueDictionary();

        foreach (var parameter in parsedTemplate.Parameters)
        {
            if (parameter.DefaultValue != null && parameter.Name != null)
            {
                result.Add(parameter.Name, parameter.DefaultValue);
            }
        }

        return result;
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}

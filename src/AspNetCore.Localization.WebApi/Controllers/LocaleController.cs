using AspNetCore.Localization.Infra.Resources;
using AspNetCore.Localization.WebApi.Middlewares;
using AspNetCore.Localization.WebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AspNetCore.Localization.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocaleController(IStringLocalizer<ShareResource> localizer) : ControllerBase
{
    // Test: 
    // curl -X GET 'http://localhost:5000/api/Locale/Get/en-US'
    [Route("Get/{locale}")]
    [HttpGet]
    [MiddlewareFilter(typeof(LocalizationMiddleware))]
    public async Task<string> Get([FromRoute] string locale)
    {
        var localizedStrs = localizer.GetAllStrings(includeParentCultures: true);
        return await localizedStrs.ToJsonStringAsync(isCamelLowerCaseForKey: true);
    }
}
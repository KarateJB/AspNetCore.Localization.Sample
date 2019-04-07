using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Localization.Infra.Resources;
using AspNetCore.Localization.WebApi.Middlewares;
using AspNetCore.Localization.WebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AspNetCore.Localization.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocaleController : ControllerBase
    {
        private readonly IStringLocalizer _localizer;

        public LocaleController(IStringLocalizer<ShareResource> localizer)
        {
            this._localizer = localizer;
        }

        [Route("Get/{locale}")]
        [HttpGet]
        [MiddlewareFilter(typeof(LocalizationMiddleware))]
        public async Task<string> Get([FromRoute] string locale)
        {
            IEnumerable<LocalizedString> localizedStrs = null;
            if (LocalizationMiddleware.SupportedCultures.Any(x => x.Name.Equals(locale)))
            {
                var cultureInfo = new CultureInfo(locale);
                #region Option1.Custom localizer
                var customLocalizer = this._localizer.WithCulture(cultureInfo);
                localizedStrs = customLocalizer.GetAllStrings(includeParentCultures: true);
                #endregion


                #region Option2. Use Thread.CurrentThread
                //Thread.CurrentThread.CurrentUICulture = cultureInfo;
                //Thread.CurrentThread.CurrentCulture = cultureInfo;
                //localizedStrs = this._localizer.GetAllStrings(includeParentCultures: true);
                #endregion
            }
            else
            {
                localizedStrs = this._localizer.GetAllStrings(includeParentCultures: true);
            }

            return await localizedStrs.ToJsonStringAsync(isCamelLowerCaseForKey: true);
        }
    }
}

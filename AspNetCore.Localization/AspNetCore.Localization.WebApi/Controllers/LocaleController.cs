using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.Localization.WebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AspNetCore.Localization.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocaleController : ControllerBase
    {
        private IStringLocalizer<LocaleController> _localizer = null;
        public LocaleController(IStringLocalizer<LocaleController> localizer)
        {
            this._localizer = localizer;
        }

        [Route("Get/{locale}")]
        [HttpGet]
        public async Task<string> Get([FromRoute] string locale)
        {
            IEnumerable<LocalizedString> localizedStrs = null;
            CultureInfo cultureInfo = new CultureInfo(locale);

            #region Option1.Custom localizer
            var customLocalizer = this._localizer.WithCulture(cultureInfo);
            localizedStrs = customLocalizer.GetAllStrings(includeParentCultures: true);
            #endregion


            #region Option2. Use Thread.CurrentThread
            //Thread.CurrentThread.CurrentUICulture = cultureInfo;
            //Thread.CurrentThread.CurrentCulture = cultureInfo;
            //localizedStrs = this._localizer.GetAllStrings(includeParentCultures: true);
            #endregion

            return await localizedStrs.ToJsonStringAsync(isCamelLowerCaseForKey: true);
        }
    }
}

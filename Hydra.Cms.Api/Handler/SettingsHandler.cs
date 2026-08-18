using Hydra.Cms.Core.Interfaces;
using Hydra.Cms.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Cms.Api.Handler
{
    public static class SettingsHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_settingService"></param>
        /// <returns></returns>
        public static IResult GetSettings(
             ISiteSettingsService _settingService)
        {
            var result = _settingService.GetSettings();

            return Results.Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_settingService"></param>
        /// <returns></returns>
        public static IResult AddOrUpdateSettings(ISiteSettingsService _settingService, [FromBody] SiteSettingsModel siteSettingsModel)
        {
            var result = _settingService.AddOrUpdate(siteSettingsModel);

            return Results.Ok(result);

        }

    }
}
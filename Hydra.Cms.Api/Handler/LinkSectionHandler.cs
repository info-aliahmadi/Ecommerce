using Hydra.Cms.Core.Interfaces;
using Hydra.Cms.Core.Models;
using Hydra.Kernel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hydra.Cms.Api.Handler
{
    public static class LinkSectionHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_linkSectionService"></param>
        /// <param name="linkSectionModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(
             ILinkSectionService _linkSectionService)
        {
            var result = await _linkSectionService.GetList();

            return Results.Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_linkSectionService"></param>
        /// <param name="linkSectionModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetLinkSectionById(
            ILinkSectionService _linkSectionService,
            int linkSectionId
            )
        {
            var result = await _linkSectionService.GetById(linkSectionId);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_linkSectionService"></param>
        /// <param name="linkSectionModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddLinkSection(
            ClaimsPrincipal userClaim,
            ILinkSectionService _linkSectionService,
            [FromBody] LinkSectionModel linkSectionModel
            )
        {
            var userId = userClaim.GetUserId();
            var result = await _linkSectionService.Add(linkSectionModel);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_linkSectionService"></param>
        /// <param name="linkSectionModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateLinkSection(
            ClaimsPrincipal userClaim,
            ILinkSectionService _linkSectionService,
            [FromBody] LinkSectionModel linkSectionModel
            )
        {
            var userId = userClaim.GetUserId();
            var result = await _linkSectionService.Update(linkSectionModel);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_linkSectionService"></param>
        /// <param name="linkSectionId"></param>
        /// <returns></returns>
        public static async Task<IResult> VisibleLinkSection(ILinkSectionService _linkSectionService, int linkSectionId)
        {
            var result = await _linkSectionService.Visible(linkSectionId);

            return Results.Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_linkSectionService"></param>
        /// <param name="linkSectionId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteLinkSection(
            ILinkSectionService _linkSectionService,
            int linkSectionId
            )
        {
            var result = await _linkSectionService.Delete(linkSectionId);

            return Results.Ok(result);

        }

    }
}
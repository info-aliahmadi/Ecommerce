using Hydra.Cms.Core.Interfaces;
using Hydra.Cms.Core.Models;
using Hydra.Kernel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hydra.Cms.Api.Handler
{
    public static class LinkHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_linkService"></param>
        /// <param name="linkModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(
             ILinkService _linkService)
        {
            var result = await _linkService.GetList();

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_linkService"></param>
        /// <param name="sectionKey"></param>
        /// <returns></returns>
        public static async Task<IResult> GetLinksByKeyList(
             ILinkService _linkService,
            string sectionKey)
        {
            var result = await _linkService.GetByKeyList(sectionKey);

            return Results.Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_linkService"></param>
        /// <param name="linkModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetLinkById(
            ILinkService _linkService,
            int linkId
            )
        {
            var result = await _linkService.GetById(linkId);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_linkService"></param>
        /// <param name="linkModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddLink(
            ClaimsPrincipal userClaim,
            ILinkService _linkService,
            [FromBody] LinkModel linkModel
            )
        {
            var userId = userClaim.GetUserId();
            linkModel.UserId = userId;
            var result = await _linkService.Add(linkModel);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_linkService"></param>
        /// <param name="linkModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateLink(
            ClaimsPrincipal userClaim,
            ILinkService _linkService,
            [FromBody] LinkModel linkModel
            )
        {
            var userId = userClaim.GetUserId();
            linkModel.UserId = userId;
            var result = await _linkService.Update(linkModel);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_linkService"></param>
        /// <param name="linkModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateOrders(ClaimsPrincipal userClaim,
            ILinkService _linkService,
            [FromBody] List<LinkModel> linkModelList
            )
        {
            var userId = userClaim.GetUserId();

            var result = await _linkService.UpdateOrder(linkModelList);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_linkService"></param>
        /// <param name="linkId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteLink(
            ILinkService _linkService,
            int linkId
            )
        {
            var result = await _linkService.Delete(linkId);

            return Results.Ok(result);

        }

    }
}
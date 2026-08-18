using Hydra.Cms.Core.Interfaces;
using Hydra.Cms.Core.Models;
using Hydra.Kernel;
using Hydra.Kernel.GeneralModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hydra.Cms.Api.Handler
{
    public static class PageHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_articleService"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetPageByIdForVisitors(
            IPageService _pageService,
            int pageId
            )
        {
            var result = await _pageService.GetById(pageId);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_pageService"></param>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(
             IPageService _pageService, GridDataBound dataGrid)
        {
            var result = await _pageService.GetList(dataGrid);

            return Results.Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_pageService"></param>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetPageById(
            IPageService _pageService,
            int pageId
            )
        {
            var result = await _pageService.GetById(pageId);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_pageService"></param>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddPage(
            ClaimsPrincipal userClaim,
            IPageService _pageService,
            [FromBody] PageModel pageModel
            )
        {
            var userId = userClaim.GetUserId();
            pageModel.WriterId = userId;
            var result = await _pageService.Add(pageModel);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_pageService"></param>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdatePage(
            ClaimsPrincipal userClaim,
            IPageService _pageService,
            [FromBody] PageModel pageModel
            )
        {
            var userId = userClaim.GetUserId();
            pageModel.EditorId = userId;
            var result = await _pageService.Update(pageModel);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_pageService"></param>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeletePage(
            IPageService _pageService,
            int pageId
            )
        {
            var result = await _pageService.Delete(pageId);

            return Results.Ok(result);

        }

    }
}
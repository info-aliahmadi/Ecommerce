
using Hydra.Kernel.GeneralModels;
using Hydra.Cms.Core.Interfaces;
using Hydra.Cms.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Hydra.Kernel;

namespace Hydra.Cms.Api.Handler
{
    public static class TagHandler
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tagService"></param>
        /// <param name="tagModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(
             ITagService _tagService, GridDataBound dataGrid)
        {
            var result = await _tagService.GetList(dataGrid);

            return Results.Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tagService"></param>
        /// <param name="tagModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetAllList(
             ITagService _tagService)
        {
            var result = await _tagService.GetAllList();

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tagService"></param>
        /// <returns></returns>
        public static async Task<IResult> GetListForSelect(ITagService _tagService)
        {
            var result = await _tagService.GetListForSelect();

            return Results.Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tagService"></param>
        /// <param name="tagModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetTagById(
            ITagService _tagService,
            int tagId
            )
        {
            var result = await _tagService.GetById(tagId);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tagService"></param>
        /// <param name="tagModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddTag(ClaimsPrincipal userClaim,
            ITagService _tagService,
            [FromBody] TagModel tagModel
            )
        {
            var userId = userClaim.GetUserId();
            var result = await _tagService.Add(tagModel);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tagService"></param>
        /// <param name="tagModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateTag(ClaimsPrincipal userClaim,
            ITagService _tagService,
            [FromBody] TagModel tagModel
            )
        {
            var userId = userClaim.GetUserId();
            var result = await _tagService.Update(tagModel);

            return Results.Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tagService"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteTag(ClaimsPrincipal userClaim,
            ITagService _tagService,
            int tagId
            )
        {
            var userId = userClaim.GetUserId();
            var result = await _tagService.Delete(tagId);

            return Results.Ok(result);

        }

    }
}
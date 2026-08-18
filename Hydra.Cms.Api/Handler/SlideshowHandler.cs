
using Hydra.Cms.Core.Interfaces;
using Hydra.Cms.Core.Models;
using Hydra.Kernel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hydra.Cms.Api.Handler
{
    public static class SlideshowHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_slideshowService"></param>
        /// <returns></returns>
        public static async Task<IResult> GetPublishedSlideshow(ISlideshowService _slideshowService)
        {
            var result = await _slideshowService.GetPublishedSlideshow();

            return Results.Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_slideshowService"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(ISlideshowService _slideshowService)
        {
            var result = await _slideshowService.GetList();

            return Results.Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_slideshowService"></param>
        /// <param name="slideshowModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetSlideshowById(
            ISlideshowService _slideshowService,
            int slideshowId
            )
        {
            var result = await _slideshowService.GetById(slideshowId);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_slideshowService"></param>
        /// <param name="slideshowModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddSlideshow(ClaimsPrincipal userClaim,
            ISlideshowService _slideshowService,
            [FromBody] SlideshowModel slideshowModel
            )
        {
            var userId = userClaim.GetUserId();
            slideshowModel.UserId = userId;
            var result = await _slideshowService.Add(slideshowModel);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_slideshowService"></param>
        /// <param name="slideshowModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateOrders(ClaimsPrincipal userClaim,
            ISlideshowService _slideshowService,
            [FromBody] List<SlideshowModel> slideshowModelList
            )
        {
            var userId = userClaim.GetUserId();
            foreach (var slideshowModel in slideshowModelList.Where(x => x.IsVisible))
            {
                slideshowModel.UserId = userId;
            }

            var result = await _slideshowService.UpdateOrder(slideshowModelList);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_slideshowService"></param>
        /// <param name="slideshowModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateSlideshow(ClaimsPrincipal userClaim,
            ISlideshowService _slideshowService,
            [FromBody] SlideshowModel slideshowModel
            )
        {
            var userId = userClaim.GetUserId();
            slideshowModel.UserId = userId;
            var result = await _slideshowService.Update(slideshowModel);

            return Results.Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_slideshowService"></param>
        /// <param name="slideshowId"></param>
        /// <returns></returns>
        public static async Task<IResult> VisibleSlideshow(ISlideshowService _slideshowService, int slideshowId)
        {
            var result = await _slideshowService.Visible(slideshowId);

            return Results.Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_slideshowService"></param>
        /// <param name="slideshowId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteSlideshow(ISlideshowService _slideshowService, int slideshowId)
        {
            var result = await _slideshowService.Delete(slideshowId);

            return Results.Ok(result);

        }

    }
}
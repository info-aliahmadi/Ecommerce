
using Hydra.Kernel.Extension;
using Hydra.Cms.Core.Interfaces;
using Hydra.Cms.Core.Models;
using Hydra.Kernel.GeneralModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Hydra.Kernel;

namespace Hydra.Cms.Api.Handler
{
    public static class TopicHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_topicService"></param>
        /// <returns></returns>
        public static async Task<IResult> GetTopicsHierarchy(ITopicService _topicService)
        {
            var result = await _topicService.GetHierarchy();

            return Results.Ok(result);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_topicService"></param>
        /// <returns></returns>
        public static async Task<IResult> GetListForSelect(ITopicService _topicService)
        {
            var result = await _topicService.GetListForSelect();

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_topicService"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(ITopicService _topicService)
        {
            var result = await _topicService.GetList();

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_topicService"></param>
        /// <param name="topicModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetTopicById(
            ITopicService _topicService,
            int topicId
            )
        {
            var result = await _topicService.GetById(topicId);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_topicService"></param>
        /// <param name="topicModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddTopic(ClaimsPrincipal userClaim,
            ITopicService _topicService,
            [FromBody] TopicModel topicModel
            )
        {
            var userId = userClaim.GetUserId();
            topicModel.UserId = userId;
            var result = await _topicService.Add(topicModel);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_topicService"></param>
        /// <param name="topicModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateTopic(ClaimsPrincipal userClaim,
            ITopicService _topicService,
            [FromBody] TopicModel topicModel
            )
        {
            var userId = userClaim.GetUserId();
            topicModel.UserId = userId;
            var result = await _topicService.Update(topicModel);

            return Results.Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_topicService"></param>
        /// <param name="topicId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteTopic(ClaimsPrincipal userClaim,
            ITopicService _topicService,
            int topicId
            )
        {
            var userId = userClaim.GetUserId();
            var result = await _topicService.Delete(topicId);

            return Results.Ok(result);

        }

    }
}
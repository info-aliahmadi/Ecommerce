using System.Security.Claims;
using Hydra.Crm.Core.Interfaces;
using Hydra.Crm.Core.Models.Subscribe;
using Hydra.Kernel.GeneralModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Crm.Api.Handler
{
    public static class SubscribeHandler
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="subscribeService"></param>
        /// <param name="subscribeModel"></param>
        /// <returns></returns>
        public static async Task<IResult> SubscribeUser(ClaimsPrincipal userClaim, ISubscribeService subscribeService, [FromBody] UserSubscribeModel subscribeModel)
        {
            var result = await subscribeService.SubscribeUser(subscribeModel);
            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscribeService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(ISubscribeService subscribeService, GridDataBound dataGrid)
        {
            var result = await subscribeService.GetList(dataGrid);
            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscribeService"></param>
        /// <param name="subscribeId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetSubscribeById(ISubscribeService subscribeService, long subscribeId)
        {
            var result = await subscribeService.GetById(subscribeId);
            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="subscribeService"></param>
        /// <param name="subscribeModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddSubscribe(ClaimsPrincipal userClaim, ISubscribeService subscribeService, [FromBody] SubscribeModel subscribeModel)
        {
            var result = await subscribeService.Add(subscribeModel);
            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="subscribeService"></param>
        /// <param name="subscribeModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateSubscribe(ClaimsPrincipal userClaim, ISubscribeService subscribeService, [FromBody] SubscribeModel subscribeModel)
        {
            var result = await subscribeService.Update(subscribeModel);
            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscribeService"></param>
        /// <param name="subscribeId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteSubscribe(ISubscribeService subscribeService, long subscribeId)
        {
            var result = await subscribeService.Delete(subscribeId);
            return Results.Ok(result);
        }
    }
}
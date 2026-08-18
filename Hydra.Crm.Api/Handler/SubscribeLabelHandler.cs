
using System.Security.Claims;
using Hydra.Crm.Core.Interfaces;
using Hydra.Crm.Core.Models.Subscribe;
using Hydra.Kernel.GeneralModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Crm.Api.Handler
{
    public static class SubscribeLabelHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscribeLabelService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(ISubscribeLabelService subscribeLabelService, GridDataBound dataGrid)
        {
            var result = await subscribeLabelService.GetList(dataGrid);
            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="labelService"></param>
        /// <returns></returns>
        public static async Task<IResult> GetListForSelect(ISubscribeLabelService labelService)
        {
            var result = await labelService.GetListForSelect();
            return Results.Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscribeLabelService"></param>
        /// <param name="subscribeLabelId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetSubscribeLabelById(ISubscribeLabelService subscribeLabelService, int subscribeLabelId)
        {
            var result = await subscribeLabelService.GetById(subscribeLabelId);
            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="subscribeLabelService"></param>
        /// <param name="subscribelabelModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddSubscribeLabel(ClaimsPrincipal userClaim, ISubscribeLabelService subscribeLabelService, [FromBody] SubscribeLabelModel subscribelabelModel)
        {
            var result = await subscribeLabelService.Add(subscribelabelModel);
            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="subscribeLabelService"></param>
        /// <param name="subscribelabelModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateSubscribeLabel(ClaimsPrincipal userClaim, ISubscribeLabelService subscribeLabelService, [FromBody] SubscribeLabelModel subscribelabelModel)
        {
            var result = await subscribeLabelService.Update(subscribelabelModel);
            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscribeLabelService"></param>
        /// <param name="subscribeLabelId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteSubscribeLabel(ISubscribeLabelService subscribeLabelService, int subscribeLabelId)
        {
            var result = await subscribeLabelService.Delete(subscribeLabelId);
            return Results.Ok(result);
        }

    }
}
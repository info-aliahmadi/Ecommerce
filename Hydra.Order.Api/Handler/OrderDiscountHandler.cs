using System.Security.Claims;
using Hydra.Kernel.GeneralModels;
using Hydra.Order.Core.Interfaces;
using Hydra.Order.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Order.Api.Handler
{
    public static class OrderDiscountHandler
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderDiscountService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(IOrderDiscountService orderDiscountService, GridDataBound dataGrid)
        {
            var result = await orderDiscountService.GetList(dataGrid);
            return Results.Ok(result);

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderDiscountService"></param>
        /// <param name="orderDiscountId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetOrderDiscountById(IOrderDiscountService orderDiscountService, int orderDiscountId)
        {
            var result = await orderDiscountService.GetById(orderDiscountId);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="orderDiscountService"></param>
        /// <param name="orderDiscountModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddOrderDiscount(ClaimsPrincipal userClaim, IOrderDiscountService orderDiscountService, [FromBody] OrderDiscountModel orderDiscountModel)
        {
            var result = await orderDiscountService.Add(orderDiscountModel);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="orderDiscountService"></param>
        /// <param name="orderDiscountModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateOrderDiscount(ClaimsPrincipal userClaim, IOrderDiscountService orderDiscountService, [FromBody] OrderDiscountModel orderDiscountModel)
        {
            var result = await orderDiscountService.Update(orderDiscountModel);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderDiscountService"></param>
        /// <param name="orderDiscountId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteOrderDiscount(IOrderDiscountService orderDiscountService, int orderDiscountId)
        {
            var result = await orderDiscountService.Delete(orderDiscountId);
            return Results.Ok(result);
        }

    }
}
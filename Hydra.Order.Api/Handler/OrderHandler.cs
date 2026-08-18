using System.Security.Claims;
using Hydra.Kernel;
using Hydra.Kernel.GeneralModels;
using Hydra.Order.Core.Interfaces;
using Hydra.Order.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Order.Api.Handler
{
    public static class OrderHandler
    {


        // --- User-facing endpoints ---

        public static async Task<IResult> GetMyOrders(ClaimsPrincipal userClaim, IOrderService orderService)
        {
            var userId = userClaim.GetUserId();
            var result = await orderService.GetMyOrders(userId);
            return Results.Ok(result);
        }

        public static async Task<IResult> GetMyOrderById(ClaimsPrincipal userClaim, IOrderService orderService, int orderId)
        {
            var userId = userClaim.GetUserId();
            var result = await orderService.GetMyOrderById(userId, orderId);
            return Results.Ok(result);
        }

        public static async Task<IResult> GetMyOrderItems(ClaimsPrincipal userClaim, IOrderService orderService, int orderId)
        {
            var userId = userClaim.GetUserId();
            var result = await orderService.GetMyOrderItems(userId, orderId);
            return Results.Ok(result);
        }

        public static async Task<IResult> CreateOrder(ClaimsPrincipal userClaim, IOrderService orderService, IHttpContextAccessor httpContextAccessor, [FromBody] CreateOrderRequest request)
        {
            var userId = userClaim.GetUserId();
            request.CustomerIp = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? string.Empty;
            var result = await orderService.CreateOrder(userId, request);
            return Results.Ok(result);
        }

        public static async Task<IResult> CancelMyOrder(ClaimsPrincipal userClaim, IOrderService orderService, int orderId)
        {
            var userId = userClaim.GetUserId();
            var result = await orderService.CancelOrder(userId, orderId);
            return Results.Ok(result);
        }

        public static async Task<IResult> ConfirmOrder(ClaimsPrincipal userClaim, IOrderService orderService, int orderId)
        {
            var userId = userClaim.GetUserId();
            var result = await orderService.ConfirmOrder(userId, orderId);
            return Results.Ok(result);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="orderService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(IOrderService orderService, GridDataBound dataGrid)
        {
            var result = await orderService.GetList(dataGrid);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderService"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetOrderById(IOrderService orderService, int orderId)
        {
            var result = await orderService.GetById(orderId);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="orderService"></param>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateOrder(ClaimsPrincipal userClaim, IOrderService orderService, [FromBody] OrderModel orderModel)
        {
            var result = await orderService.Update(orderModel);
            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="orderService"></param>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateOrderState(ClaimsPrincipal userClaim, IOrderService orderService, [FromBody] OrderChangeStatusModel orderStatusModel)
        {
            var result = await orderService.UpdateState(orderStatusModel);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderService"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteOrder(IOrderService orderService, int orderId)
        {
            var result = await orderService.Delete(orderId);
            return Results.Ok(result);
        }

    }
}
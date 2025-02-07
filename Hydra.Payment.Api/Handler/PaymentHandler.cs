﻿using System.Security.Claims;
using Hydra.Kernel.GeneralModels;
using Hydra.Payment.Core.Interfaces;
using Hydra.Payment.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Payment.Api.Handler
{
    public static class PaymentHandler
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="paymentService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(IPaymentService paymentService, GridDataBound dataGrid)
        {
            try
            {
                var result = await paymentService.GetList(dataGrid);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderService"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetOrderPaymentById(IPaymentService paymentService, int orderId)
        {
            var result = await paymentService.GetOrderPaymentById(orderId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="paymentService"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetPaymentById(IPaymentService paymentService, int paymentId)
        {
            var result = await paymentService.GetById(paymentId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymentService"></param>
        /// <returns></returns>
        public static async Task<IResult> GetAllPaymentStatus(IPaymentService paymentService)
        {
            var result = await paymentService.GetAllPaymentStatus();
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="paymentService"></param>
        /// <param name="paymentModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddPayment(ClaimsPrincipal userClaim, IPaymentService paymentService, [FromBody] PaymentModel paymentModel)
        {
            var result = await paymentService.Add(paymentModel);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="paymentService"></param>
        /// <param name="paymentModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdatePayment(ClaimsPrincipal userClaim, IPaymentService paymentService, [FromBody] PaymentModel paymentModel)
        {
            var result = await paymentService.Update(paymentModel);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="paymentService"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeletePayment(IPaymentService paymentService, int paymentId)
        {
            try
            {
                var result = await paymentService.Delete(paymentId);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

    }
}
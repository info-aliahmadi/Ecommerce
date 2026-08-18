using System.Security.Claims;
using Hydra.Kernel;
using Hydra.Kernel.GeneralModels;
using Hydra.Order.Core.Interfaces;
using Hydra.Order.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Order.Api.Handler
{
    public static class PaymentHandler
    {


        // --- User-facing endpoints ---

        public static async Task<IResult> GetMyPayments(ClaimsPrincipal userClaim, IPaymentService paymentService)
        {
            var userId = userClaim.GetUserId();
            var result = await paymentService.GetMyPayments(userId);
            return Results.Ok(result);
        }

        public static async Task<IResult> GetMyPaymentById(ClaimsPrincipal userClaim, IPaymentService paymentService, int paymentId)
        {
            var userId = userClaim.GetUserId();
            var result = await paymentService.GetMyPaymentById(userId, paymentId);
            return Results.Ok(result);
        }

        public static async Task<IResult> ProcessPayment(ClaimsPrincipal userClaim, IPaymentService paymentService, [FromBody] ProcessPaymentRequest request)
        {
            var userId = userClaim.GetUserId();
            var result = await paymentService.ProcessPayment(userId, request);
            return Results.Ok(result);
        }

        public static async Task<IResult> GetList(IPaymentService paymentService, GridDataBound dataGrid)
        {
            var result = await paymentService.GetList(dataGrid);
            return Results.Ok(result);

        }

        public static async Task<IResult> GetOrderPaymentById(IPaymentService paymentService, int orderId)
        {
            var result = await paymentService.GetOrderPaymentById(orderId);
            return Results.Ok(result);
        }

        public static async Task<IResult> GetPaymentById(IPaymentService paymentService, int paymentId)
        {
            var result = await paymentService.GetById(paymentId);
            return Results.Ok(result);
        }

        public static async Task<IResult> AddPayment(ClaimsPrincipal userClaim, IPaymentService paymentService, [FromBody] PaymentModel paymentModel)
        {
            var result = await paymentService.Add(paymentModel);
            return Results.Ok(result);
        }

        public static async Task<IResult> UpdatePayment(ClaimsPrincipal userClaim, IPaymentService paymentService, [FromBody] PaymentModel paymentModel)
        {
            var result = await paymentService.Update(paymentModel);
            return Results.Ok(result);
        }

        public static async Task<IResult> DeletePayment(IPaymentService paymentService, int paymentId)
        {
            var result = await paymentService.Delete(paymentId);
            return Results.Ok(result);

        }
    }
}

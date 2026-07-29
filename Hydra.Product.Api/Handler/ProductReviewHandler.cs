using System.Security.Claims;
using Hydra.Kernel;
using Hydra.Kernel.GeneralModels;
using Hydra.Product.Core.Interfaces;
using Hydra.Product.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Product.Api.Handler
{
    public static class ProductReviewHandler
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productReviewService"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetProductReviews(IProductReviewService productReviewService, int productId)
        {
            var result = await productReviewService.GetProductReviews(productId);
            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="productReviewService"></param>
        /// <param name="productReviewModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddUserReview(ClaimsPrincipal userClaim, IProductReviewService productReviewService, [FromBody] ProductReviewModel productReviewModel)
        {
            productReviewModel.UserId = userClaim.GetUserId();
            var result = await productReviewService.AddUserReview(productReviewModel);
            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="productReviewService"></param>
        /// <param name="productReviewModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateUserReview(ClaimsPrincipal userClaim, IProductReviewService productReviewService, [FromBody] ProductReviewModel productReviewModel)
        {
            productReviewModel.UserId = userClaim.GetUserId();
            var result = await productReviewService.UpdateUserReview(productReviewModel);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productReviewService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(IProductReviewService productReviewService, GridDataBound dataGrid)
        {
            try
            {
                var result = await productReviewService.GetList(dataGrid);
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
        /// <param name="productReviewService"></param>
        /// <param name="productReviewId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetProductReviewById(IProductReviewService productReviewService, int productReviewId)
        {
            var result = await productReviewService.GetById(productReviewId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="productReviewService"></param>
        /// <param name="productReviewModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateProductReview(ClaimsPrincipal userClaim, IProductReviewService productReviewService, [FromBody] ProductReviewModel productReviewModel)
        {
            var result = await productReviewService.Update(productReviewModel);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productReviewService"></param>
        /// <param name="productReviewId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteProductReview(IProductReviewService productReviewService, int productReviewId)
        {
            try
            {
                var result = await productReviewService.Delete(productReviewId);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

    }
}
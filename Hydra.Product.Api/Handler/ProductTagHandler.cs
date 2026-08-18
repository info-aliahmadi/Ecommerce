using System.Security.Claims;
using Hydra.Kernel.GeneralModels;
using Hydra.Product.Core.Interfaces;
using Hydra.Product.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Product.Api.Handler
{
    public static class ProductTagHandler
    {
        /// <summary>
        /// Retrieves a list of published product tags and returns the result as an HTTP response.
        /// </summary>
        /// <param name="productTagService">The service used to access product tag data. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an HTTP 200 response with the
        /// published product tags if successful; otherwise, an HTTP 400 response with error details.</returns>
        public static IResult GetPublishedList(IProductTagService productTagService)
        {
            var result = productTagService.GetPublishedList();
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productTagService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(IProductTagService productTagService, GridDataBound dataGrid)
        {
            var result = await productTagService.GetList(dataGrid);
            return Results.Ok(result);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="productTagService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetListForSelect(IProductTagService productTagService)
        {
            var result = await productTagService.GetListForSelect();
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productTagService"></param>
        /// <param name="productTagId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetProductTagById(IProductTagService productTagService, int productTagId)
        {
            var result = await productTagService.GetById(productTagId);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="productTagService"></param>
        /// <param name="productTagModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddProductTag(ClaimsPrincipal userClaim, IProductTagService productTagService, [FromBody] ProductTagModel productTagModel)
        {
            var result = await productTagService.Add(productTagModel);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="productTagService"></param>
        /// <param name="productTagModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateProductTag(ClaimsPrincipal userClaim, IProductTagService productTagService, [FromBody] ProductTagModel productTagModel)
        {
            var result = await productTagService.Update(productTagModel);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productTagService"></param>
        /// <param name="productTagId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteProductTag(IProductTagService productTagService, int productTagId)
        {
            var result = await productTagService.Delete(productTagId);
            return Results.Ok(result);
        }

    }
}
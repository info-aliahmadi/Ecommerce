using System.Security.Claims;
using Hydra.Kernel;
using Hydra.Kernel.GeneralModels;
using Hydra.Product.Core.Interfaces;
using Hydra.Product.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Product.Api.Handler
{
    public static class ProductHandler
    {

        /// <summary>
        /// Retrieves a list of products that match the specified filter criteria.
        /// </summary>
        /// <param name="productService">The service used to query and retrieve product data.</param>
        /// <param name="productFilter">The filter criteria to apply when retrieving products. May include properties such as category, price range,
        /// or search terms.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IResult"/> that is
        /// <see langword="Ok"/> with the product data if the operation succeeds, or <see langword="BadRequest"/> with
        /// error details if it fails.</returns>
        public static async Task<IResult> GetPublishedProducts(IProductService productService, ProductFilterDisplayModel productFilter)
        {
            var result = await productService.GetPublishedProducts(productFilter);
            return Results.Ok(result);

        }

        /// <summary>
        /// Retrieves a list of products that match the specified filter criteria.
        /// </summary>
        /// <param name="productService">The service used to query and retrieve product data.</param>
        /// <param name="productFilter">The filter criteria to apply when retrieving products. May include properties such as category, price range,
        /// or search terms.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IResult"/> that is
        /// <see langword="Ok"/> with the product data if the operation succeeds, or <see langword="BadRequest"/> with
        /// error details if it fails.</returns>
        public static async Task<IResult> GetPublishedCuratedStyleProducts(IProductService productService)
        {
            var result = await productService.GetPublishedCuratedStyleProducts();
            return Results.Ok(result);
        }


        /// <summary>
        /// Retrieves a list of products that match the specified filter criteria.
        /// </summary>
        /// <param name="productService">The service used to query and retrieve product data.</param>
        /// <param name="productFilter">The filter criteria to apply when retrieving products. May include properties such as category, price range,
        /// or search terms.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IResult"/> that is
        /// <see langword="Ok"/> with the product data if the operation succeeds, or <see langword="BadRequest"/> with
        /// error details if it fails.</returns>
        public static async Task<IResult> GetPublishedProductById(IProductService productService, int productId)
        {
            var result = await productService.GetPublishedProductById(productId);
            return Results.Ok(result);
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="productService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(IProductService productService, GridDataBound dataGrid)
        {
            var result = await productService.GetList(dataGrid);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productService"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetProductById(IProductService productService, int productId)
        {
            var result = await productService.GetById(productId);
            return Results.Ok(result);
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="productService"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetProductsByIds(IProductService productService, string productIds)
        {
            var Ids = productIds.Split(',').Select(x => int.Parse(x)).ToArray();

            var result = await productService.GetByIds(Ids);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productService"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<IResult> GetProductsByInput(IProductService productService, string input)
        {
            var result = await productService.GetProductsByInput(input);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productService"></param>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public static async Task<IResult> GetProductStockByIds(IProductService productService, string productVariableIds)
        {
            var Ids = productVariableIds.Split(',').Select(x => int.Parse(x)).ToArray();
            var result = await productService.GetProductStockByIds(Ids);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="productService"></param>
        /// <param name="productModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddProduct(ClaimsPrincipal userClaim, IProductService productService, [FromBody] ProductModel productModel)
        {
            var userId = userClaim.GetUserId();
            productModel.CreateUserId = userId;
            var result = await productService.Add(productModel);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="productService"></param>
        /// <param name="productModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateProduct(ClaimsPrincipal userClaim, IProductService productService, [FromBody] ProductModel productModel)
        {
            var userId = userClaim.GetUserId();
            productModel.UpdateUserId = userId;

            var result = await productService.Update(productModel);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productService"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteProduct(IProductService productService, int productId)
        {
            var result = await productService.Delete(productId);
            return Results.Ok(result);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="productService"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static async Task<IResult> RemoveProduct(IProductService productService, int productId)
        {
            var result = await productService.Remove(productId);
            return Results.Ok(result);
        }

    }
}
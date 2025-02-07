using System.Security.Claims;
using Hydra.Inventory.Core.Interfaces;
using Hydra.Inventory.Core.Models;
using Hydra.Kernel.GeneralModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Product.Api.Handler
{
    public static class InventoryHandler
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="productInventoryService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(IInventoryService productInventoryService, GridDataBound dataGrid)
        {
            try
            {
                var result = await productInventoryService.GetList(dataGrid);
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
        /// <param name="productInventoryService"></param>
        /// <param name="productInventoryId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetProductInventoryById(IInventoryService productInventoryService, int productInventoryId)
        {
            var result = await productInventoryService.GetById(productInventoryId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="productInventoryService"></param>
        /// <param name="inventoryModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddProductInventory(ClaimsPrincipal userClaim, IInventoryService productInventoryService, [FromBody] InventoryModel inventoryModel)
        {
            var result = await productInventoryService.Add(inventoryModel);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="productInventoryService"></param>
        /// <param name="inventoryModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateProductInventory(ClaimsPrincipal userClaim, IInventoryService productInventoryService, [FromBody] InventoryModel inventoryModel)
        {
            var result = await productInventoryService.Update(inventoryModel);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productInventoryService"></param>
        /// <param name="productInventoryId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteProductInventory(IInventoryService productInventoryService, int productInventoryId)
        {
            try
            {
                var result = await productInventoryService.Delete(productInventoryId);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

    }
}
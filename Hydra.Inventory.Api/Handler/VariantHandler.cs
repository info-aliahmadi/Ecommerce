using System.Security.Claims;
using Hydra.Inventory.Core.Interfaces;
using Hydra.Inventory.Core.Models;
using Hydra.Kernel.GeneralModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Inventory.Api.Handler
{
    public static class VariantHandler
    {
        public static async Task<IResult> GetList(IVariantService variantService, GridDataBound dataGrid)
        {
            try
            {
                var result = await variantService.GetList(dataGrid);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        public static async Task<IResult> GetListByProductId(IVariantService variantService, int productId)
        {
            var result = await variantService.GetListByProductId(productId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> GetVariantById(IVariantService variantService, int variantId)
        {
            var result = await variantService.GetById(variantId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> AddVariant(ClaimsPrincipal userClaim, IVariantService variantService, [FromBody] VariantModel model)
        {
            var result = await variantService.Add(model);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> UpdateVariant(ClaimsPrincipal userClaim, IVariantService variantService, [FromBody] VariantModel model)
        {
            var result = await variantService.Update(model);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> DeleteVariant(IVariantService variantService, int variantId)
        {
            try
            {
                var result = await variantService.Delete(variantId);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        public static async Task<IResult> UpdateStock(ClaimsPrincipal userClaim, IVariantService variantService, [FromBody] StockAdjustmentModel model)
        {
            var result = await variantService.UpdateStock(model);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> GetInventoryTransactions(IVariantService variantService, int variantId)
        {
            var result = await variantService.GetInventoryTransactions(variantId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }
    }
}

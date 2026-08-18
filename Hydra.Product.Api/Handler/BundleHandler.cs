using System.Security.Claims;
using Hydra.Kernel;
using Hydra.Kernel.GeneralModels;
using Hydra.Product.Core.Interfaces;
using Hydra.Product.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Product.Api.Handler
{
    public static class BundleHandler
    {
        public static async Task<IResult> GetPublishedBundles(IBundleService bundleService)
        {
                var result = await bundleService.GetPublishedBundles();
                return Results.Ok(result);
        }

        public static async Task<IResult> GetList(IBundleService bundleService)
        {
                var result = await bundleService.GetBundlesList();
                return Results.Ok(result);
        }

        public static async Task<IResult> GetListForSelect(IBundleService bundleService)
        {
                var result = await bundleService.GetListForSelect();
                return Results.Ok(result);
        }

        public static async Task<IResult> GetBundleById(IBundleService bundleService, int bundleId)
        {
            var result = await bundleService.GetById(bundleId);
            return Results.Ok(result);
        }

        public static async Task<IResult> AddBundle(ClaimsPrincipal userClaim, IBundleService bundleService, [FromBody] BundleModel bundleModel)
        {
            var result = await bundleService.Add(bundleModel);
            return Results.Ok(result);
        }

        public static async Task<IResult> UpdateBundle(ClaimsPrincipal userClaim, IBundleService bundleService, [FromBody] BundleModel bundleModel)
        {
            var result = await bundleService.Update(bundleModel);
            return Results.Ok(result);
        }

        public static async Task<IResult> UpdateOrders(ClaimsPrincipal userClaim,
            IBundleService bundleService,
            [FromBody] List<BundleModel> bundleList)
        {
                var userId = userClaim.GetUserId();
                var result = await bundleService.UpdateOrder(bundleList);
                return Results.Ok(result);
        }

        public static async Task<IResult> DeleteBundle(IBundleService bundleService, int bundleId)
        {
                var result = await bundleService.Delete(bundleId);
                return Results.Ok(result);
        }
    }
}

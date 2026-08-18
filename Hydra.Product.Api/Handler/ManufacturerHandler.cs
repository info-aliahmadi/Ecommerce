using System.Security.Claims;
using Hydra.Kernel;
using Hydra.Kernel.GeneralModels;
using Hydra.Product.Core.Interfaces;
using Hydra.Product.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Product.Api.Handler
{
    public static class ManufacturerHandler
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="manufacturerService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetPublishedManufacturers(IManufacturerService manufacturerService)
        {
            var result = await manufacturerService.GetPublishedManufacturers();
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="manufacturerService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(IManufacturerService manufacturerService)
        {
            var result = await manufacturerService.GetManufacturersList();
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="manufacturerService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetListForSelect(IManufacturerService manufacturerService)
        {
            var result = await manufacturerService.GetListForSelect();
            return Results.Ok(result);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="manufacturerService"></param>
        /// <param name="manufacturerId"></param>
        /// <returns></returns>
        public static IResult GetManufacturerById(IManufacturerService manufacturerService, int manufacturerId)
        {
            var result = manufacturerService.GetById(manufacturerId);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="manufacturerService"></param>
        /// <param name="manufacturerModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddManufacturer(ClaimsPrincipal userClaim, IManufacturerService manufacturerService, [FromBody] ManufacturerModel manufacturerModel)
        {
            var result = await manufacturerService.Add(manufacturerModel);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="manufacturerService"></param>
        /// <param name="manufacturerModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateManufacturer(ClaimsPrincipal userClaim, IManufacturerService manufacturerService, [FromBody] ManufacturerModel manufacturerModel)
        {
            var result = await manufacturerService.Update(manufacturerModel);
            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_menuService"></param>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateOrders(ClaimsPrincipal userClaim,
            IManufacturerService manufacturerService,
            [FromBody] List<ManufacturerModel> manufacturerList
            )
        {
            var userId = userClaim.GetUserId();

            var result = await manufacturerService.UpdateOrder(manufacturerList);

            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="manufacturerService"></param>
        /// <param name="manufacturerId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteManufacturer(IManufacturerService manufacturerService, int manufacturerId)
        {
            var result = await manufacturerService.Delete(manufacturerId);
            return Results.Ok(result);
        }

    }
}
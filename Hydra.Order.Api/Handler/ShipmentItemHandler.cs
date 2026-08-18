using System.Security.Claims;
using Hydra.Kernel.GeneralModels;
using Hydra.Order.Core.Interfaces;
using Hydra.Order.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Order.Api.Handler
{
    public static class ShipmentItemHandler
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="shipmentItemService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(IShipmentItemService shipmentItemService, GridDataBound dataGrid)
        {
            var result = await shipmentItemService.GetList(dataGrid);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="shipmentItemService"></param>
        /// <param name="shipmentItemId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetShipmentItemById(IShipmentItemService shipmentItemService, int shipmentItemId)
        {
            var result = await shipmentItemService.GetById(shipmentItemId);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="shipmentItemService"></param>
        /// <param name="shipmentItemModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddShipmentItem(ClaimsPrincipal userClaim, IShipmentItemService shipmentItemService, [FromBody] ShipmentItemModel shipmentItemModel)
        {
            var result = await shipmentItemService.Add(shipmentItemModel);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="shipmentItemService"></param>
        /// <param name="shipmentItemModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateShipmentItem(ClaimsPrincipal userClaim, IShipmentItemService shipmentItemService, [FromBody] ShipmentItemModel shipmentItemModel)
        {
            var result = await shipmentItemService.Update(shipmentItemModel);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="shipmentItemService"></param>
        /// <param name="shipmentItemId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteShipmentItem(IShipmentItemService shipmentItemService, int shipmentItemId)
        {
            var result = await shipmentItemService.Delete(shipmentItemId);
            return Results.Ok(result);
        }

    }
}
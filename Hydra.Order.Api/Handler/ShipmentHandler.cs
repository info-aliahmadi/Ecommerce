using System.Security.Claims;
using Hydra.Kernel.GeneralModels;
using Hydra.Order.Core.Interfaces;
using Hydra.Order.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Order.Api.Handler
{
    public static class ShipmentHandler
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="shipmentService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(IShipmentService shipmentService, GridDataBound dataGrid)
        {
            var result = await shipmentService.GetList(dataGrid);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="shipmentService"></param>
        /// <param name="shipmentId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetShipmentById(IShipmentService shipmentService, int shipmentId)
        {
            var result = await shipmentService.GetById(shipmentId);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="shipmentService"></param>
        /// <param name="shipmentModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddShipment(ClaimsPrincipal userClaim, IShipmentService shipmentService, [FromBody] ShipmentModel shipmentModel)
        {
            var result = await shipmentService.Add(shipmentModel);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="shipmentService"></param>
        /// <param name="shipmentModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateShipment(ClaimsPrincipal userClaim, IShipmentService shipmentService, [FromBody] ShipmentModel shipmentModel)
        {
            var result = await shipmentService.Update(shipmentModel);
            return Results.Ok(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="shipmentService"></param>
        /// <param name="shipmentId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteShipment(IShipmentService shipmentService, int shipmentId)
        {
            var result = await shipmentService.Delete(shipmentId);
            return Results.Ok(result);
        }

    }
}
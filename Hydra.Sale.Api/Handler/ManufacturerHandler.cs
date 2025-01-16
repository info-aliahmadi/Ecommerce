﻿using System.Security.Claims;
using Hydra.Infrastructure.Data.Extension;
using Hydra.Sale.Core.Interfaces;
using Hydra.Sale.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Sale.Api.Handler
{
    public static class ManufacturerHandler
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="manufacturerService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(IManufacturerService manufacturerService, GridDataBound dataGrid)
        {
            try
            {
                var result = await manufacturerService.GetList(dataGrid);
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
        /// <param name="manufacturerService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static IResult GetListForSelect(IManufacturerService manufacturerService)
        {
            try
            {
                var result = manufacturerService.GetListForSelect();
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
        /// <param name="manufacturerService"></param>
        /// <param name="manufacturerId"></param>
        /// <returns></returns>
        public static IResult GetManufacturerById(IManufacturerService manufacturerService, int manufacturerId)
        {
            var result = manufacturerService.GetById(manufacturerId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
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
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
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
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="manufacturerService"></param>
        /// <param name="manufacturerId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteManufacturer(IManufacturerService manufacturerService, int manufacturerId)
        {
            try
            {
                var result = await manufacturerService.Delete(manufacturerId);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

    }
}
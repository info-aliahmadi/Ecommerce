using System.Security.Claims;
using Hydra.Kernel.GeneralModels;
using Hydra.Common.Core.Interfaces;
using Hydra.Common.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hydra.Kernel;

namespace Hydra.Common.Api.Handler
{
    public static class AddressHandler
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="addressService"></param>
        /// <returns></returns>
        public static async Task<IResult> GetUserAddresses(ClaimsPrincipal userClaim, IAddressService addressService)
        {
            try
            {
                var userId = userClaim.GetUserId();
                var result = await addressService.GetUserAddresses(userId);
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
        /// <param name="userClaim"></param>
        /// <param name="addressService"></param>
        /// <param name="addressModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddAddress(ClaimsPrincipal userClaim, IAddressService addressService, [FromBody] AddressModel addressModel)
        {
            addressModel.UserId = userClaim.GetUserId();
            var result = await addressService.Add(addressModel);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="addressService"></param>
        /// <param name="addressModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateAddress(ClaimsPrincipal userClaim, IAddressService addressService, [FromBody] AddressModel addressModel)
        {
            addressModel.UserId = userClaim.GetUserId();
            var result = await addressService.Update(addressModel);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="addressService"></param>
        /// <param name="addressModel"></param>
        /// <returns></returns>
        public static async Task<IResult> SetAsDefault(ClaimsPrincipal userClaim, IAddressService addressService, int addressId)
        {
            var userId = userClaim.GetUserId();
            var result = await addressService.SetAsDefault(userId, addressId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="addressService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(IAddressService addressService, GridDataBound dataGrid)
        {
            try
            {
                var result = await addressService.GetList(dataGrid);
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
        /// <param name="addressService"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetAddressById(IAddressService addressService, int addressId)
        {
            var result = await addressService.GetById(addressId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="addressService"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteAddress(IAddressService addressService, int addressId)
        {
            try
            {
                var result = await addressService.Delete(addressId);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

    }
}
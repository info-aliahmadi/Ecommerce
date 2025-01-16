﻿using System.Security.Claims;
using Hydra.Infrastructure.Data.Extension;
using Hydra.Sale.Core.Interfaces;
using Hydra.Sale.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Sale.Api.Handler
{
    public static class CountryHandler
    {

        /// <summary>
        /// test
        /// </summary>
        /// <param name="countryService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(ICountryService countryService, GridDataBound dataGrid)
        {
            try
            {
                var result = await countryService.GetList(dataGrid);
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
        /// <param name="countryService"></param>
        /// <returns></returns>
        public static async Task<IResult> GetCountrySeed(ICountryService countryService)
        {
            try
            {
                var result = await countryService.GetCountrySeed();
                await File.AppendAllTextAsync("C:\\Users\\AliReza\\Desktop\\test\\seed.txt", result);
                return Results.Ok();
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="countryService"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetCountryById(ICountryService countryService, int countryId)
        {
            var result = await countryService.GetById(countryId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="countryService"></param>
        /// <param name="countryModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddCountry(ClaimsPrincipal userClaim, ICountryService countryService, [FromBody] CountryModel countryModel)
        {
            var result = await countryService.Add(countryModel);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="countryService"></param>
        /// <param name="countryModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateCountry(ClaimsPrincipal userClaim, ICountryService countryService, [FromBody] CountryModel countryModel)
        {
            var result = await countryService.Update(countryModel);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="countryService"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteCountry(ICountryService countryService, int countryId)
        {
            try
            {
                var result = await countryService.Delete(countryId);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

    }
}
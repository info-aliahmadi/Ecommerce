using Hydra.Common.Api.Services;
using Hydra.Common.Core.Interfaces;
using Hydra.Common.Core.Models;
using Hydra.Kernel.GeneralModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hydra.Common.Api.Handler
{
    public static class TaxRateHandler
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="taxRateService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(ITaxRateService taxRateService, GridDataBound dataGrid)
        {
                var result = await taxRateService.GetList(dataGrid);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="taxCategoryService"></param>
        /// <returns></returns>
        public static async Task<IResult> GetTaxRateListForSelect(ITaxRateService taxCategoryService)
        {
            var result = await taxCategoryService.GetTaxRateListForSelect();
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="taxRateService"></param>
        /// <param name="taxRateId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetTaxRateById(ITaxRateService taxRateService, int taxRateId)
        {
            var result = await taxRateService.GetById(taxRateId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="taxRateService"></param>
        /// <param name="taxRateModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddTaxRate(ClaimsPrincipal userClaim, ITaxRateService taxRateService, [FromBody] TaxRateModel taxRateModel)
        {
            var result = await taxRateService.Add(taxRateModel);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="taxRateService"></param>
        /// <param name="taxRateModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateTaxRate(ClaimsPrincipal userClaim, ITaxRateService taxRateService, [FromBody] TaxRateModel taxRateModel)
        {
            var result = await taxRateService.Update(taxRateModel);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="taxRateService"></param>
        /// <param name="taxRateId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteTaxRate(ITaxRateService taxRateService, int taxRateId)
        {
                var result = await taxRateService.Delete(taxRateId);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

    }
}
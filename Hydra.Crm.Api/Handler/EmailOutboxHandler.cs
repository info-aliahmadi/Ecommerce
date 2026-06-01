
using Hydra.Crm.Core.Interfaces;
using Hydra.Crm.Core.Models.Email;
using Hydra.Kernel;
using Hydra.Kernel.Extension;
using Hydra.Kernel.GeneralModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hydra.Crm.Api.Handler
{
    public static class EmailOutboxHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_emailOutboxService"></param>
        /// <param name="emailOutboxModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetAllEmailOutbox(IEmailOutboxService _emailOutboxService, GridDataBound dataGrid)
        {
            var result = await _emailOutboxService.GetAllEmailOutbox(dataGrid);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_emailOutboxService"></param>
        /// <param name="emailOutboxModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetEmailOutbox(
            ClaimsPrincipal userClaim,
             IEmailOutboxService _emailOutboxService, GridDataBound dataGrid)
        {
            var currentEmail = userClaim.GetEmail();

            var result = await _emailOutboxService.GetOutbox(dataGrid, currentEmail);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_emailOutboxService"></param>
        /// <param name="emailOutboxModel"></param>
        /// <returns></returns>
        public static IResult GetAddressForSelect(IEmailOutboxService _emailOutboxService)
        {
            var result = _emailOutboxService.GetAddressForSelect();

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="_emailOutboxService"></param>
        /// <param name="emailOutboxId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetEmailOutboxByIdForSender(
            ClaimsPrincipal userClaim,
             IEmailOutboxService _emailOutboxService, int emailOutboxId)
        {
            var currentEmail = userClaim.GetEmail();

            var result = await _emailOutboxService.GetByIdForSender(emailOutboxId, currentEmail);

                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_emailOutboxService"></param>
        /// <param name="emailOutboxModel"></param>
        /// <returns></returns>
        public static async Task<IResult> SendEmailOutbox(
            HttpContext context,
            ClaimsPrincipal userClaim,
            IEmailOutboxService _emailOutboxService,
            [FromBody] EmailOutboxModel emailOutboxModel
            )
        {
            var currentEmail = userClaim.GetEmail();
            var currentName = userClaim.GetName();

            var fromUser = new AuthorModel()
            {
                Name = currentName,
                Email = currentEmail
            };

            var result = await _emailOutboxService.Send(emailOutboxModel, fromUser, context);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_emailOutboxService"></param>
        /// <param name="emailOutboxModel"></param>
        /// <returns></returns>
        public static async Task<IResult> SaveDraftEmailOutbox(
            ClaimsPrincipal userClaim,
            IEmailOutboxService _emailOutboxService,
            [FromBody] EmailOutboxModel emailOutboxModel
            )
        {
            var currentEmail = userClaim.GetEmail();
            var currentName = userClaim.GetName();;

            var fromUser = new AuthorModel()
            {
                Name = currentName,
                Email = currentEmail
            };

            var result = await _emailOutboxService.SaveDraft(emailOutboxModel, fromUser);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_emailOutboxService"></param>
        /// <param name="emailOutboxModel"></param>
        /// <returns></returns>
        public static async Task<IResult> RemoveEmailOutbox(
            ClaimsPrincipal userClaim,
             IEmailOutboxService _emailOutboxService, int emailOutboxId)
        {
            var currentEmail = userClaim.GetEmail();

            var result = await _emailOutboxService.Remove(emailOutboxId, currentEmail);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

        }
    }
}
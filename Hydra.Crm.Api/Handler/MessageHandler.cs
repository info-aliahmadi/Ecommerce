using Hydra.Crm.Core.Interfaces;
using Hydra.Crm.Core.Models.Message;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Hydra.Auth.Interface;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel;

namespace Hydra.Crm.Api.Handler
{
    public static class MessageHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetAllMessages(IMessageService _messageService, GridDataBound dataGrid)
        {
            var result = await _messageService.GetAllMessages(dataGrid);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetInboxMessages(
            ClaimsPrincipal userClaim,
             IMessageService _messageService, GridDataBound dataGrid)
        {
            var userId = userClaim.GetUserId();

            var result = await _messageService.GetInbox(dataGrid, userId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetPublicInboxMessages(
            ClaimsPrincipal userClaim,
             IMessageService _messageService, GridDataBound dataGrid)
        {
            var result = await _messageService.GetPublicInbox(dataGrid);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetDeletedInboxMessages(
            ClaimsPrincipal userClaim,
             IMessageService _messageService, GridDataBound dataGrid)
        {
            var userId = userClaim.GetUserId();

            var result = await _messageService.GetDeletedInbox(dataGrid, userId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetSentMessages(
            ClaimsPrincipal userClaim,
             IMessageService _messageService, GridDataBound dataGrid)
        {
            var userId = userClaim.GetUserId();

            var result = await _messageService.GetSent(dataGrid, userId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetDraftMessages(
            ClaimsPrincipal userClaim,
             IMessageService _messageService, GridDataBound dataGrid)
        {
            var userId = userClaim.GetUserId();

            var result = await _messageService.GetDrafts(dataGrid, userId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetDeletedSentMessages(
            ClaimsPrincipal userClaim,
             IMessageService _messageService, GridDataBound dataGrid)
        {
            var userId = userClaim.GetUserId();

            var result = await _messageService.GetDeletedSent(dataGrid, userId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetMessageByIdForPublic(
            ClaimsPrincipal userClaim,
             IMessageService _messageService, int messageId)
        {
            var result = await _messageService.GetByIdForPublic(messageId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetMessageByIdForReceiver(
            ClaimsPrincipal userClaim,
             IMessageService _messageService, int messageId)
        {
            var userId = userClaim.GetUserId();

            var result = await _messageService.GetByIdForReceiver(messageId, userId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetMessageByIdForSender(
            ClaimsPrincipal userClaim,
             IMessageService _messageService, int messageId)
        {
            var userId = userClaim.GetUserId();

            var result = await _messageService.GetByIdForSender(messageId, userId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> SendPublicMessage(
            ClaimsPrincipal userClaim,
            IMessageService _messageService,
            [FromBody] MessageModel messageModel
            )
        {
            var userId = userClaim.GetUserId();

            messageModel.FromUserId = userId;

            messageModel.MessageType = Core.Domain.Message.MessageType.Public;

            var result = await _messageService.Send(messageModel, userId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> SendPrivateMessage(
            ClaimsPrincipal userClaim,
            IMessageService _messageService,
            [FromBody] MessageModel messageModel
            )
        {
            var userId = userClaim.GetUserId();

            messageModel.FromUserId = userId;

            messageModel.MessageType = Core.Domain.Message.MessageType.Private;

            var result = await _messageService.Send(messageModel, userId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> SendRequestMessage(
            IMessageService _messageService, IMessageSettingsService _messageSettingsService, IUserService _user,
            [FromBody] MessageModel messageModel
            )
        {
            messageModel.FromUserId = null;

            messageModel.Name += " " + messageModel.Family;

            messageModel.Content = "[" + messageModel.Knowing + "]" + "<br>" + messageModel.Content;

            messageModel.MessageType = Core.Domain.Message.MessageType.Request;

            var settings = _messageSettingsService.GetSettings().Data;

            messageModel.ToUserIds = settings.RecipientIdsForRequestMessage.ToList();

            if (!messageModel.ToUserIds.Any())
            {
                var adminuser = (await _user.GetAdminUser()).Data;
                messageModel.ToUserIds.Add(adminuser.Id);
            }

            var result = await _messageService.Send(messageModel, 0);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> SendContactMessage(
            IMessageService _messageService, IMessageSettingsService _messageSettingsService, IUserService _user,
            [FromBody] MessageModel messageModel
            )
        {
            messageModel.FromUserId = null;

            messageModel.Name += " " + messageModel.Family;

            messageModel.Content = "[" + messageModel.Knowing + "]" + "<br>" + messageModel.Content;

            messageModel.MessageType = Core.Domain.Message.MessageType.Contact;

            var settings = _messageSettingsService.GetSettings().Data;

            messageModel.ToUserIds = settings.RecipientIdsForContactMessage.ToList();

            if (!messageModel.ToUserIds.Any())
            {
                var adminuser = (await _user.GetAdminUser()).Data;
                messageModel.ToUserIds.Add(adminuser.Id);
            }

            var result = await _messageService.Send(messageModel, 0);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> SaveDraftMessage(
            ClaimsPrincipal userClaim,
            IMessageService _messageService,
            [FromBody] MessageModel messageModel
            )
        {
            var userId = userClaim.GetUserId();

            messageModel.FromUserId = userId;

            var result = await _messageService.SaveDraft(messageModel, userId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteDraftMessage(
            ClaimsPrincipal userClaim,
             IMessageService _messageService, int messageId)
        {
            var userId = userClaim.GetUserId();

            var result = await _messageService.DeleteDraft(messageId, userId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteMessage(
            ClaimsPrincipal userClaim,
             IMessageService _messageService, int messageId)
        {
            var userId = userClaim.GetUserId();

            var result = await _messageService.Delete(messageId, userId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> RestoreMessage(
            ClaimsPrincipal userClaim,
             IMessageService _messageService, int messageId)
        {
            var userId = userClaim.GetUserId();

            var result = await _messageService.Restore(messageId, userId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> PinMessage(
            ClaimsPrincipal userClaim,
             IMessageService _messageService, int messageId)
        {
            var userId = userClaim.GetUserId();

            var result = await _messageService.Pin(messageId, userId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> ReadMessage(
            ClaimsPrincipal userClaim,
             IMessageService _messageService, int messageId)
        {
            var userId = userClaim.GetUserId();

            var result = await _messageService.Read(messageId, userId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public static async Task<IResult> RemoveDraftMessage(
            ClaimsPrincipal userClaim,
             IMessageService _messageService, int messageId)
        {
            var userId = userClaim.GetUserId();

            var result = await _messageService.RemoveDraft(messageId, userId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

        }
    }
}
﻿using Hydra.Crm.Core.Interfaces;
using Hydra.Crm.Core.Models.Message;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Hydra.Infrastructure.Data.Extension;
using Hydra.Infrastructure.Security.Interface;

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
            var currentUserId = int.Parse(userClaim.FindFirst("identity")!.Value);

            var result = await _messageService.GetInbox(dataGrid, currentUserId);

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
            var currentUserId = int.Parse(userClaim.FindFirst("identity")!.Value);

            var result = await _messageService.GetDeletedInbox(dataGrid, currentUserId);

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
            var currentUserId = int.Parse(userClaim.FindFirst("identity")!.Value);

            var result = await _messageService.GetSent(dataGrid, currentUserId);

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
            var currentUserId = int.Parse(userClaim.FindFirst("identity")!.Value);

            var result = await _messageService.GetDrafts(dataGrid, currentUserId);

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
            var currentUserId = int.Parse(userClaim.FindFirst("identity")!.Value);

            var result = await _messageService.GetDeletedSent(dataGrid, currentUserId);

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
            var currentUserId = int.Parse(userClaim.FindFirst("identity")!.Value);

            var result = await _messageService.GetByIdForReceiver(messageId, currentUserId);

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
            var currentUserId = int.Parse(userClaim.FindFirst("identity")!.Value);

            var result = await _messageService.GetByIdForSender(messageId, currentUserId);

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
            var currentUserId = int.Parse(userClaim.FindFirst("identity")!.Value);

            messageModel.FromUserId = currentUserId;

            messageModel.MessageType = Core.Domain.Message.MessageType.Public;

            var result = await _messageService.Send(messageModel, currentUserId);

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
            var currentUserId = int.Parse(userClaim.FindFirst("identity")!.Value);

            messageModel.FromUserId = currentUserId;

            messageModel.MessageType = Core.Domain.Message.MessageType.Private;

            var result = await _messageService.Send(messageModel, currentUserId);

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
            var currentUserId = int.Parse(userClaim.FindFirst("identity")!.Value);

            messageModel.FromUserId = currentUserId;

            var result = await _messageService.SaveDraft(messageModel, currentUserId);

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
            var currentUserId = int.Parse(userClaim.FindFirst("identity")!.Value);

            var result = await _messageService.DeleteDraft(messageId, currentUserId);

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
            var currentUserId = int.Parse(userClaim.FindFirst("identity")!.Value);

            var result = await _messageService.Delete(messageId, currentUserId);

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
            var currentUserId = int.Parse(userClaim.FindFirst("identity")!.Value);

            var result = await _messageService.Restore(messageId, currentUserId);

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
            var currentUserId = int.Parse(userClaim.FindFirst("identity")!.Value);

            var result = await _messageService.Pin(messageId, currentUserId);

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
            var currentUserId = int.Parse(userClaim.FindFirst("identity")!.Value);

            var result = await _messageService.Read(messageId, currentUserId);

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
            var currentUserId = int.Parse(userClaim.FindFirst("identity")!.Value);

            var result = await _messageService.RemoveDraft(messageId, currentUserId);

            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

        }
    }
}
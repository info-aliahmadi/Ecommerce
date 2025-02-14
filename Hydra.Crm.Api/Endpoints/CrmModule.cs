using Hydra.Crm.Api.Handler;
using Hydra.Crm.Api.Services;
using Hydra.Crm.Core.Constants;
using Hydra.Crm.Core.Interfaces;
using Hydra.Infrastructure.ModuleExtension;
using Hydra.Infrastructure.Security.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.Crm.Api.Endpoints
{
    public class CrmModule : IModule
    {
        private const string API_SCHEMA = "/Crm";
        public IServiceCollection RegisterModules(IServiceCollection services)
        {
            services.AddScoped<IMessageSettingsService, MessageSettingsService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IEmailInboxService, EmailInboxService>();
            services.AddScoped<IEmailOutboxService, EmailOutboxService>();
            services.AddScoped<ISubscribeService, SubscribeService>();
            services.AddScoped<ISubscribeLabelService, SubscribeLabelService>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(API_SCHEMA + "/SendRequestMessage", MessageHandler.SendRequestMessage).AllowAnonymous();
            endpoints.MapPost(API_SCHEMA + "/SendContactMessage", MessageHandler.SendContactMessage).AllowAnonymous();

            endpoints.MapGet(API_SCHEMA + "/GetSettings", MessageSettingsHandler.GetSettings).RequirePermission(CrmPermissionTypes.CRM_SETTING_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/AddOrUpdateSettings", MessageSettingsHandler.AddOrUpdateSettings).RequirePermission(CrmPermissionTypes.CRM_SETTING_MANAGMENT);

            endpoints.MapPost(API_SCHEMA + "/SendPublicMessage", MessageHandler.SendPublicMessage).RequirePermission(CrmPermissionTypes.CRM_ALL_MESSAGE_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/SendPrivateMessage", MessageHandler.SendPrivateMessage).RequirePermission(CrmPermissionTypes.CRM_ALL_MESSAGE_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/SaveDraftMessage", MessageHandler.SaveDraftMessage).RequirePermission(CrmPermissionTypes.CRM_ALL_MESSAGE_MANAGMENT);

            endpoints.MapPost(API_SCHEMA + "/GetAllMessages", MessageHandler.GetAllMessages).RequirePermission(CrmPermissionTypes.CRM_ALL_MESSAGE_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/GetInboxMessages", MessageHandler.GetInboxMessages).RequirePermission(CrmPermissionTypes.CRM_MESSAGE_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/GetSentMessages", MessageHandler.GetSentMessages).RequirePermission(CrmPermissionTypes.CRM_MESSAGE_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/GetDraftMessages", MessageHandler.GetDraftMessages).RequirePermission(CrmPermissionTypes.CRM_MESSAGE_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/GetPublicInboxMessages", MessageHandler.GetPublicInboxMessages).RequirePermission(CrmPermissionTypes.CRM_ALL_MESSAGE_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/GetDeletedInboxMessages", MessageHandler.GetDeletedInboxMessages).RequirePermission(CrmPermissionTypes.CRM_MESSAGE_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/GetDeletedSentMessages", MessageHandler.GetDeletedSentMessages).RequirePermission(CrmPermissionTypes.CRM_MESSAGE_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/GetMessageByIdForPublic", MessageHandler.GetMessageByIdForPublic).RequirePermission(CrmPermissionTypes.CRM_ALL_MESSAGE_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/GetMessageByIdForSender", MessageHandler.GetMessageByIdForSender).RequirePermission(CrmPermissionTypes.CRM_MESSAGE_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/GetMessageByIdForReceiver", MessageHandler.GetMessageByIdForReceiver).RequirePermission(CrmPermissionTypes.CRM_MESSAGE_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteMessage", MessageHandler.DeleteMessage).RequirePermission(CrmPermissionTypes.CRM_MESSAGE_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/RestoreMessage", MessageHandler.RestoreMessage).RequirePermission(CrmPermissionTypes.CRM_MESSAGE_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/PinMessage", MessageHandler.PinMessage).RequirePermission(CrmPermissionTypes.CRM_MESSAGE_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/ReadMessage", MessageHandler.ReadMessage).RequirePermission(CrmPermissionTypes.CRM_MESSAGE_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteDraftMessage", MessageHandler.DeleteDraftMessage).RequirePermission(CrmPermissionTypes.CRM_MESSAGE_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/RemoveDraftMessage", MessageHandler.RemoveDraftMessage).RequirePermission(CrmPermissionTypes.CRM_MESSAGE_MANAGMENT);



            endpoints.MapGet(API_SCHEMA + "/LoadEmailInbox", EmailInboxHandler.LoadEmailInbox).RequirePermission(CrmPermissionTypes.CRM_ALL_EMAIL_INBOX_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/GetAllEmailInbox", EmailInboxHandler.GetAllEmailInbox).RequirePermission(CrmPermissionTypes.CRM_ALL_EMAIL_INBOX_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/GetEmailInbox", EmailInboxHandler.GetEmailInbox).RequirePermission(CrmPermissionTypes.CRM_EMAIL_INBOX_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/GetDeletedEmailInbox", EmailInboxHandler.GetDeletedInbox).RequirePermission(CrmPermissionTypes.CRM_ALL_EMAIL_INBOX_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/GetEmailInboxById", EmailInboxHandler.GetEmailInboxById).RequirePermission(CrmPermissionTypes.CRM_ALL_EMAIL_INBOX_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/GetEmailInboxByIdForReceiver", EmailInboxHandler.GetEmailInboxByIdForReceiver).RequirePermission(CrmPermissionTypes.CRM_ALL_EMAIL_INBOX_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteEmailInbox", EmailInboxHandler.DeleteEmailInbox).RequirePermission(CrmPermissionTypes.CRM_ALL_EMAIL_INBOX_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/PinEmailInbox", EmailInboxHandler.PinEmailInbox).RequirePermission(CrmPermissionTypes.CRM_EMAIL_INBOX_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/ReadEmailInbox", EmailInboxHandler.ReadEmailInbox).RequirePermission(CrmPermissionTypes.CRM_EMAIL_INBOX_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/RemoveEmailInbox", EmailInboxHandler.RemoveEmailInbox).RequirePermission(CrmPermissionTypes.CRM_EMAIL_INBOX_MANAGMENT);



            endpoints.MapPost(API_SCHEMA + "/SendEmailOutbox", EmailOutboxHandler.SendEmailOutbox).RequirePermission(CrmPermissionTypes.CRM_EMAIL_OUTBOX_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/SaveDraftEmailOutbox", EmailOutboxHandler.SaveDraftEmailOutbox).RequirePermission(CrmPermissionTypes.CRM_EMAIL_OUTBOX_MANAGMENT);

            endpoints.MapPost(API_SCHEMA + "/GetAllEmailOutbox", EmailOutboxHandler.GetAllEmailOutbox).RequirePermission(CrmPermissionTypes.CRM_ALL_EMAIL_OUTBOX_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/GetEmailOutbox", EmailOutboxHandler.GetEmailOutbox).RequirePermission(CrmPermissionTypes.CRM_EMAIL_OUTBOX_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/GetAddressForSelect", EmailOutboxHandler.GetAddressForSelect).RequireAuthorization();
            endpoints.MapGet(API_SCHEMA + "/GetEmailOutboxByIdForSender", EmailOutboxHandler.GetEmailOutboxByIdForSender).RequirePermission(CrmPermissionTypes.CRM_EMAIL_OUTBOX_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/RemoveEmailOutbox", EmailOutboxHandler.RemoveEmailOutbox).RequirePermission(CrmPermissionTypes.CRM_EMAIL_OUTBOX_MANAGMENT);
            
            endpoints.MapPost(API_SCHEMA + "/GetSubscribeList", SubscribeHandler.GetList).RequirePermission(CrmPermissionTypes.CRM_SUBSCRIBE_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/GetSubscribeById", SubscribeHandler.GetSubscribeById).RequirePermission(CrmPermissionTypes.CRM_SUBSCRIBE_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/AddSubscribe", SubscribeHandler.AddSubscribe).AllowAnonymous();
            endpoints.MapPost(API_SCHEMA + "/UpdateSubscribe", SubscribeHandler.UpdateSubscribe).RequirePermission(CrmPermissionTypes.CRM_SUBSCRIBE_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteSubscribe", SubscribeHandler.DeleteSubscribe).RequirePermission(CrmPermissionTypes.CRM_SUBSCRIBE_MANAGMENT);

            endpoints.MapPost(API_SCHEMA + "/GetSubscribeLabelList", SubscribeLabelHandler.GetList).RequirePermission(CrmPermissionTypes.CRM_SUBSCRIBE_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/GetSubscribeLabelListForSelect", SubscribeLabelHandler.GetListForSelect).RequirePermission(CrmPermissionTypes.CRM_SUBSCRIBE_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/GetSubscribeLabelById", SubscribeLabelHandler.GetSubscribeLabelById).RequirePermission(CrmPermissionTypes.CRM_SUBSCRIBE_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/AddSubscribeLabel", SubscribeLabelHandler.AddSubscribeLabel).RequirePermission(CrmPermissionTypes.CRM_SUBSCRIBE_MANAGMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateSubscribeLabel", SubscribeLabelHandler.UpdateSubscribeLabel).RequirePermission(CrmPermissionTypes.CRM_SUBSCRIBE_MANAGMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteSubscribeLabel", SubscribeLabelHandler.DeleteSubscribeLabel).RequirePermission(CrmPermissionTypes.CRM_SUBSCRIBE_MANAGMENT);


            return endpoints;
        }

    }
}

using Hydra.Crm.Core.Constants;
using Hydra.Infrastructure.Security.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hydra.Crm.Core.Seed
{
    public class CrmPermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public static int INCREMENTER = 3000;
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(new Permission()
            {
                Id = INCREMENTER + 1,
                Name = CrmPermissionTypes.CRM_GET_SETTINGS,
                NormalizedName = CrmPermissionTypes.CRM_GET_SETTINGS,
            }, new Permission()
            {
                Id = INCREMENTER + 2,
                Name = CrmPermissionTypes.CRM_ADD_OR_UPDATE_SETTINGS,
                NormalizedName = CrmPermissionTypes.CRM_ADD_OR_UPDATE_SETTINGS,
            }, new Permission()
            {
                Id = INCREMENTER + 3,
                Name = CrmPermissionTypes.CRM_SEND_PUBLIC_MESSAGE,
                NormalizedName = CrmPermissionTypes.CRM_SEND_PUBLIC_MESSAGE,
            }, new Permission()
            {
                Id = INCREMENTER + 4,
                Name = CrmPermissionTypes.CRM_SEND_PRIVATE_MESSAGE,
                NormalizedName = CrmPermissionTypes.CRM_SEND_PRIVATE_MESSAGE,
            }, new Permission()
            {
                Id = INCREMENTER + 5,
                Name = CrmPermissionTypes.CRM_SAVE_DRAFT_MESSAGE,
                NormalizedName = CrmPermissionTypes.CRM_SAVE_DRAFT_MESSAGE,
            }, new Permission()
            {
                Id = INCREMENTER + 6,
                Name = CrmPermissionTypes.CRM_GET_ALLMESSAGES,
                NormalizedName = CrmPermissionTypes.CRM_GET_ALLMESSAGES,
            }, new Permission()
            {
                Id = INCREMENTER + 7,
                Name = CrmPermissionTypes.CRM_GET_INBOX_MESSAGES,
                NormalizedName = CrmPermissionTypes.CRM_GET_INBOX_MESSAGES,
            }, new Permission()
            {
                Id = INCREMENTER + 8,
                Name = CrmPermissionTypes.CRM_GET_SENT_MESSAGES,
                NormalizedName = CrmPermissionTypes.CRM_GET_SENT_MESSAGES,
            }, new Permission()
            {
                Id = INCREMENTER + 9,
                Name = CrmPermissionTypes.CRM_GET_DRAFT_MESSAGES,
                NormalizedName = CrmPermissionTypes.CRM_GET_DRAFT_MESSAGES,
            }, new Permission()
            {
                Id = INCREMENTER + 10,
                Name = CrmPermissionTypes.CRM_GET_PUBLIC_INBOX_MESSAGES,
                NormalizedName = CrmPermissionTypes.CRM_GET_PUBLIC_INBOX_MESSAGES,
            }, new Permission()
            {
                Id = INCREMENTER + 11,
                Name = CrmPermissionTypes.CRM_GET_DELETED_INBOX_MESSAGES,
                NormalizedName = CrmPermissionTypes.CRM_GET_DELETED_INBOX_MESSAGES,
            }, new Permission()
            {
                Id = INCREMENTER + 12,
                Name = CrmPermissionTypes.CRM_GET_DELETED_SENT_MESSAGES,
                NormalizedName = CrmPermissionTypes.CRM_GET_DELETED_SENT_MESSAGES,
            }, new Permission()
            {
                Id = INCREMENTER + 13,
                Name = CrmPermissionTypes.CRM_GET_MESSAGE_BY_ID_FOR_PUBLIC,
                NormalizedName = CrmPermissionTypes.CRM_GET_MESSAGE_BY_ID_FOR_PUBLIC,
            }, new Permission()
            {
                Id = INCREMENTER + 14,
                Name = CrmPermissionTypes.CRM_GET_MESSAGE_BY_ID_FOR_SENDER,
                NormalizedName = CrmPermissionTypes.CRM_GET_MESSAGE_BY_ID_FOR_SENDER,
            }, new Permission()
            {
                Id = INCREMENTER + 15,
                Name = CrmPermissionTypes.CRM_GET_MESSAGE_BY_ID_FOR_RECEIVER,
                NormalizedName = CrmPermissionTypes.CRM_GET_MESSAGE_BY_ID_FOR_RECEIVER,
            }, new Permission()
            {
                Id = INCREMENTER + 16,
                Name = CrmPermissionTypes.CRM_DELETE_MESSAGE,
                NormalizedName = CrmPermissionTypes.CRM_DELETE_MESSAGE,
            }, new Permission()
            {
                Id = INCREMENTER + 17,
                Name = CrmPermissionTypes.CRM_RESTORE_MESSAGE,
                NormalizedName = CrmPermissionTypes.CRM_RESTORE_MESSAGE,
            }, new Permission()
            {
                Id = INCREMENTER + 18,
                Name = CrmPermissionTypes.CRM_PIN_MESSAGE,
                NormalizedName = CrmPermissionTypes.CRM_PIN_MESSAGE,
            }, new Permission()
            {
                Id = INCREMENTER + 19,
                Name = CrmPermissionTypes.CRM_READ_MESSAGE,
                NormalizedName = CrmPermissionTypes.CRM_READ_MESSAGE,
            }, new Permission()
            {
                Id = INCREMENTER + 20,
                Name = CrmPermissionTypes.CRM_DELETE_DRAFT_MESSAGE,
                NormalizedName = CrmPermissionTypes.CRM_DELETE_DRAFT_MESSAGE,
            }, new Permission()
            {
                Id = INCREMENTER + 21,
                Name = CrmPermissionTypes.CRM_REMOVE_DRAFT_MESSAGE,
                NormalizedName = CrmPermissionTypes.CRM_REMOVE_DRAFT_MESSAGE,
            }, new Permission()
            {
                Id = INCREMENTER + 22,
                Name = CrmPermissionTypes.CRM_LOAD_EMAIL_INBOX,
                NormalizedName = CrmPermissionTypes.CRM_LOAD_EMAIL_INBOX,
            }, new Permission()
            {
                Id = INCREMENTER + 23,
                Name = CrmPermissionTypes.CRM_GET_ALL_EMAIL_INBOX,
                NormalizedName = CrmPermissionTypes.CRM_GET_ALL_EMAIL_INBOX,
            }, new Permission()
            {
                Id = INCREMENTER + 24,
                Name = CrmPermissionTypes.CRM_GET_INBOX_EMAIL_INBOX,
                NormalizedName = CrmPermissionTypes.CRM_GET_INBOX_EMAIL_INBOX,
            }, new Permission()
            {
                Id = INCREMENTER + 25,
                Name = CrmPermissionTypes.CRM_GET_DELETED_EMAIL_INBOX,
                NormalizedName = CrmPermissionTypes.CRM_GET_DELETED_EMAIL_INBOX,
            }, new Permission()
            {
                Id = INCREMENTER + 26,
                Name = CrmPermissionTypes.CRM_GET_EMAIL_INBOX_BY_ID,
                NormalizedName = CrmPermissionTypes.CRM_GET_EMAIL_INBOX_BY_ID,
            }, new Permission()
            {
                Id = INCREMENTER + 27,
                Name = CrmPermissionTypes.CRM_GET_EMAIL_INBOX_BY_ID_FOR_RECEIVER,
                NormalizedName = CrmPermissionTypes.CRM_GET_EMAIL_INBOX_BY_ID_FOR_RECEIVER,
            }, new Permission()
            {
                Id = INCREMENTER + 28,
                Name = CrmPermissionTypes.CRM_DELETE_EMAIL_INBOX,
                NormalizedName = CrmPermissionTypes.CRM_DELETE_EMAIL_INBOX,
            }, new Permission()
            {
                Id = INCREMENTER + 29,
                Name = CrmPermissionTypes.CRM_PIN_EMAIL_INBOX,
                NormalizedName = CrmPermissionTypes.CRM_PIN_EMAIL_INBOX,
            }, new Permission()
            {
                Id = INCREMENTER + 30,
                Name = CrmPermissionTypes.CRM_READ_EMAIL_INBOX,
                NormalizedName = CrmPermissionTypes.CRM_READ_EMAIL_INBOX,
            }, new Permission()
            {
                Id = INCREMENTER + 31,
                Name = CrmPermissionTypes.CRM_REMOVE_EMAIL_INBOX,
                NormalizedName = CrmPermissionTypes.CRM_REMOVE_EMAIL_INBOX,
            }, new Permission()
            {
                Id = INCREMENTER + 32,
                Name = CrmPermissionTypes.CRM_SEND_EMAIL_OUTBOX,
                NormalizedName = CrmPermissionTypes.CRM_SEND_EMAIL_OUTBOX,
            }, new Permission()
            {
                Id = INCREMENTER + 33,
                Name = CrmPermissionTypes.CRM_SAVE_DRAFT_EMAIL_OUTBOX,
                NormalizedName = CrmPermissionTypes.CRM_SAVE_DRAFT_EMAIL_OUTBOX,
            }, new Permission()
            {
                Id = INCREMENTER + 34,
                Name = CrmPermissionTypes.CRM_GET_ALL_EMAIL_OUTBOX,
                NormalizedName = CrmPermissionTypes.CRM_GET_ALL_EMAIL_OUTBOX,
            }, new Permission()
            {
                Id = INCREMENTER + 35,
                Name = CrmPermissionTypes.CRM_GET_EMAIL_OUTBOX,
                NormalizedName = CrmPermissionTypes.CRM_GET_EMAIL_OUTBOX,
            }, new Permission()
            {
                Id = INCREMENTER + 36,
                Name = CrmPermissionTypes.CRM_GET_ADDRESS_FOR_SELECT,
                NormalizedName = CrmPermissionTypes.CRM_GET_ADDRESS_FOR_SELECT,
            }, new Permission()
            {
                Id = INCREMENTER + 37,
                Name = CrmPermissionTypes.CRM_GET_EMAIL_OUTBOX_BY_ID_FOR_SENDER,
                NormalizedName = CrmPermissionTypes.CRM_GET_EMAIL_OUTBOX_BY_ID_FOR_SENDER,
            }, new Permission()
            {
                Id = INCREMENTER + 38,
                Name = CrmPermissionTypes.CRM_REMOVE_EMAIL_OUTBOX,
                NormalizedName = CrmPermissionTypes.CRM_REMOVE_EMAIL_OUTBOX,
            });
        }
    }
}

using Hydra.Crm.Core.Constants;
using Hydra.Auth.Domain;
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
                Name = CrmPermissionTypes.CRM_SETTING_MANAGMENT,
                NormalizedName = CrmPermissionTypes.CRM_SETTING_MANAGMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 2,
                Name = CrmPermissionTypes.CRM_ALL_MESSAGE_MANAGMENT,
                NormalizedName = CrmPermissionTypes.CRM_ALL_MESSAGE_MANAGMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 3,
                Name = CrmPermissionTypes.CRM_MESSAGE_MANAGMENT,
                NormalizedName = CrmPermissionTypes.CRM_MESSAGE_MANAGMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 4,
                Name = CrmPermissionTypes.CRM_ALL_EMAIL_INBOX_MANAGMENT,
                NormalizedName = CrmPermissionTypes.CRM_ALL_EMAIL_INBOX_MANAGMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 5,
                Name = CrmPermissionTypes.CRM_EMAIL_INBOX_MANAGMENT,
                NormalizedName = CrmPermissionTypes.CRM_EMAIL_INBOX_MANAGMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 6,
                Name = CrmPermissionTypes.CRM_ALL_EMAIL_OUTBOX_MANAGMENT,
                NormalizedName = CrmPermissionTypes.CRM_ALL_EMAIL_OUTBOX_MANAGMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 7,
                Name = CrmPermissionTypes.CRM_EMAIL_OUTBOX_MANAGMENT,
                NormalizedName = CrmPermissionTypes.CRM_EMAIL_OUTBOX_MANAGMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 8,
                Name = CrmPermissionTypes.CRM_SUBSCRIBE_MANAGMENT,
                NormalizedName = CrmPermissionTypes.CRM_SUBSCRIBE_MANAGMENT,
            });
        }
    }
}

using Hydra.Cms.Core.Constants;
using Hydra.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hydra.Cms.Core.Seed
{
    public class CmsPermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public static int INCREMENTER = 2000;
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(new Permission()
            {
                Id = INCREMENTER + 1,
                Name = CmsPermissionTypes.CMS_SETTINGS_MANAGEMENT,
                NormalizedName = CmsPermissionTypes.CMS_SETTINGS_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 2,
                Name = CmsPermissionTypes.CMS_ARTICLE_MANAGEMENT,
                NormalizedName = CmsPermissionTypes.CMS_ARTICLE_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 3,
                Name = CmsPermissionTypes.CMS_TOPIC_MANAGEMENT,
                NormalizedName = CmsPermissionTypes.CMS_TOPIC_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 4,
                Name = CmsPermissionTypes.CMS_TAG_MANAGEMENT,
                NormalizedName = CmsPermissionTypes.CMS_TAG_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 5,
                Name = CmsPermissionTypes.CMS_LINK_MANAGEMENT,
                NormalizedName = CmsPermissionTypes.CMS_LINK_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 6,
                Name = CmsPermissionTypes.CMS_PAGE_MANAGEMENT,
                NormalizedName = CmsPermissionTypes.CMS_PAGE_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 7,
                Name = CmsPermissionTypes.CMS_MENU_MANAGEMENT,
                NormalizedName = CmsPermissionTypes.CMS_MENU_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 8,
                Name = CmsPermissionTypes.CMS_SLIDESHOW_MANAGEMENT,
                NormalizedName = CmsPermissionTypes.CMS_SLIDESHOW_MANAGEMENT,
            });
        }
    }
}

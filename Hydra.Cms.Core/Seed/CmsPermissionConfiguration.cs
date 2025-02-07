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
                Name = CmsPermissionTypes.CMS_ADD_OR_UPDATE_SETTINGS,
                NormalizedName = CmsPermissionTypes.CMS_ADD_OR_UPDATE_SETTINGS,
            }, new Permission()
            {
                Id = INCREMENTER + 2,
                Name = CmsPermissionTypes.CMS_GET_TOPIC_LIST,
                NormalizedName = CmsPermissionTypes.CMS_GET_TOPIC_LIST,
            }, new Permission()
            {
                Id = INCREMENTER + 3,
                Name = CmsPermissionTypes.CMS_GET_TOPIC_BY_ID,
                NormalizedName = CmsPermissionTypes.CMS_GET_TOPIC_BY_ID,
            }, new Permission()
            {
                Id = INCREMENTER + 4,
                Name = CmsPermissionTypes.CMS_ADD_TOPIC,
                NormalizedName = CmsPermissionTypes.CMS_ADD_TOPIC,
            }, new Permission()
            {
                Id = INCREMENTER + 5,
                Name = CmsPermissionTypes.CMS_UPDATE_TOPIC,
                NormalizedName = CmsPermissionTypes.CMS_UPDATE_TOPIC,
            }, new Permission()
            {
                Id = INCREMENTER + 6,
                Name = CmsPermissionTypes.CMS_DELETE_TOPIC,
                NormalizedName = CmsPermissionTypes.CMS_DELETE_TOPIC,
            }, new Permission()
            {
                Id = INCREMENTER + 7,
                Name = CmsPermissionTypes.CMS_GET_TAG_LIST,
                NormalizedName = CmsPermissionTypes.CMS_GET_TAG_LIST,
            }, new Permission()
            {
                Id = INCREMENTER + 8,
                Name = CmsPermissionTypes.CMS_GET_TAG_BY_ID,
                NormalizedName = CmsPermissionTypes.CMS_GET_TAG_BY_ID,
            }, new Permission()
            {
                Id = INCREMENTER + 9,
                Name = CmsPermissionTypes.CMS_ADD_TAG,
                NormalizedName = CmsPermissionTypes.CMS_ADD_TAG,
            }, new Permission()
            {
                Id = INCREMENTER + 10,
                Name = CmsPermissionTypes.CMS_UPDATE_TAG,
                NormalizedName = CmsPermissionTypes.CMS_UPDATE_TAG,
            }, new Permission()
            {
                Id = INCREMENTER + 11,
                Name = CmsPermissionTypes.CMS_DELETE_TAG,
                NormalizedName = CmsPermissionTypes.CMS_DELETE_TAG,
            }, new Permission()
            {
                Id = INCREMENTER + 12,
                Name = CmsPermissionTypes.CMS_GET_ARTICLE_LIST,
                NormalizedName = CmsPermissionTypes.CMS_GET_ARTICLE_LIST,
            }, new Permission()
            {
                Id = INCREMENTER + 13,
                Name = CmsPermissionTypes.CMS_GET_TRASH_ARTICLE_LIST,
                NormalizedName = CmsPermissionTypes.CMS_GET_TRASH_ARTICLE_LIST,
            }, new Permission()
            {
                Id = INCREMENTER + 14,
                Name = CmsPermissionTypes.CMS_GET_ARTICLE_BY_ID,
                NormalizedName = CmsPermissionTypes.CMS_GET_ARTICLE_BY_ID,
            }, new Permission()
            {
                Id = INCREMENTER + 15,
                Name = CmsPermissionTypes.CMS_PINNED_ARTICLE,
                NormalizedName = CmsPermissionTypes.CMS_PINNED_ARTICLE,
            }, new Permission()
            {
                Id = INCREMENTER + 16,
                Name = CmsPermissionTypes.CMS_ADD_ARTICLE,
                NormalizedName = CmsPermissionTypes.CMS_ADD_ARTICLE,
            }, new Permission()
            {
                Id = INCREMENTER + 17,
                Name = CmsPermissionTypes.CMS_UPDATE_ARTICLE,
                NormalizedName = CmsPermissionTypes.CMS_UPDATE_ARTICLE,
            }, new Permission()
            {
                Id = INCREMENTER + 18,
                Name = CmsPermissionTypes.CMS_DELETE_ARTICLE,
                NormalizedName = CmsPermissionTypes.CMS_DELETE_ARTICLE,
            }, new Permission()
            {
                Id = INCREMENTER + 19,
                Name = CmsPermissionTypes.CMS_RESTORE_ARTICLE,
                NormalizedName = CmsPermissionTypes.CMS_RESTORE_ARTICLE,
            }, new Permission()
            {
                Id = INCREMENTER + 20,
                Name = CmsPermissionTypes.CMS_REMOVE_ARTICLE,
                NormalizedName = CmsPermissionTypes.CMS_REMOVE_ARTICLE,
            }, new Permission()
            {
                Id = INCREMENTER + 21,
                Name = CmsPermissionTypes.CMS_GET_PAGE_LIST,
                NormalizedName = CmsPermissionTypes.CMS_GET_PAGE_LIST,
            }, new Permission()
            {
                Id = INCREMENTER + 22,
                Name = CmsPermissionTypes.CMS_GET_PAGE_BY_ID,
                NormalizedName = CmsPermissionTypes.CMS_GET_PAGE_BY_ID,
            }, new Permission()
            {
                Id = INCREMENTER + 23,
                Name = CmsPermissionTypes.CMS_ADD_PAGE,
                NormalizedName = CmsPermissionTypes.CMS_ADD_PAGE,
            }, new Permission()
            {
                Id = INCREMENTER + 24,
                Name = CmsPermissionTypes.CMS_UPDATE_PAGE,
                NormalizedName = CmsPermissionTypes.CMS_UPDATE_PAGE,
            }, new Permission()
            {
                Id = INCREMENTER + 25,
                Name = CmsPermissionTypes.CMS_DELETE_PAGE,
                NormalizedName = CmsPermissionTypes.CMS_DELETE_PAGE,
            }, new Permission()
            {
                Id = INCREMENTER + 26,
                Name = CmsPermissionTypes.CMS_GET_MENU_LIST,
                NormalizedName = CmsPermissionTypes.CMS_GET_MENU_LIST,
            }, new Permission()
            {
                Id = INCREMENTER + 27,
                Name = CmsPermissionTypes.CMS_GET_MENU_BY_ID,
                NormalizedName = CmsPermissionTypes.CMS_GET_MENU_BY_ID,
            }, new Permission()
            {
                Id = INCREMENTER + 28,
                Name = CmsPermissionTypes.CMS_ADD_MENU,
                NormalizedName = CmsPermissionTypes.CMS_ADD_MENU,
            }, new Permission()
            {
                Id = INCREMENTER + 29,
                Name = CmsPermissionTypes.CMS_UPDATE_MENU,
                NormalizedName = CmsPermissionTypes.CMS_UPDATE_MENU,
            }, new Permission()
            {
                Id = INCREMENTER + 30,
                Name = CmsPermissionTypes.CMS_DELETE_MENU,
                NormalizedName = CmsPermissionTypes.CMS_DELETE_MENU,
            }, new Permission()
            {
                Id = INCREMENTER + 31,
                Name = CmsPermissionTypes.CMS_GET_SLIDESHOW_LIST,
                NormalizedName = CmsPermissionTypes.CMS_GET_SLIDESHOW_LIST,
            }, new Permission()
            {
                Id = INCREMENTER + 32,
                Name = CmsPermissionTypes.CMS_GET_SLIDESHOW_BY_ID,
                NormalizedName = CmsPermissionTypes.CMS_GET_SLIDESHOW_BY_ID,
            }, new Permission()
            {
                Id = INCREMENTER + 33,
                Name = CmsPermissionTypes.CMS_ADD_SLIDESHOW,
                NormalizedName = CmsPermissionTypes.CMS_ADD_SLIDESHOW,
            }, new Permission()
            {
                Id = INCREMENTER + 34,
                Name = CmsPermissionTypes.CMS_UPDATE_SLIDESHOW,
                NormalizedName = CmsPermissionTypes.CMS_UPDATE_SLIDESHOW,
            }, new Permission()
            {
                Id = INCREMENTER + 35,
                Name = CmsPermissionTypes.CMS_VISIBLE_SLIDESHOW,
                NormalizedName = CmsPermissionTypes.CMS_VISIBLE_SLIDESHOW,
            }, new Permission()
            {
                Id = INCREMENTER + 36,
                Name = CmsPermissionTypes.CMS_DELETE_SLIDESHOW,
                NormalizedName = CmsPermissionTypes.CMS_DELETE_SLIDESHOW,
            });
        }
    }
}

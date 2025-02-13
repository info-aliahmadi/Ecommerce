using Hydra.Cms.Api.Handler;
using Hydra.Cms.Api.Services;
using Hydra.Cms.Core.Interfaces;
using Hydra.Infrastructure.ModuleExtension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Hydra.Infrastructure.Security.Extension;
using Hydra.Cms.Core.Constants;
namespace Hydra.Cms.Api.Endpoints
{
    public class CmsModule : IModule
    {
        private const string API_SCHEMA = "/Cms";
        public IServiceCollection RegisterModules(IServiceCollection services)
        {
            services.AddScoped<ISiteSettingsService, SiteSettingsService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<ILinkSectionService, LinkSectionService>();
            services.AddScoped<ILinkService, LinkService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<ISlideshowService, SlideshowService>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            // Anonymous Endpoints

            endpoints.MapGet(API_SCHEMA + "/GetSettings", SettingsHandler.GetSettings).AllowAnonymous();
            endpoints.MapGet(API_SCHEMA + "/GetMenu", MenuHandler.GetMenu).AllowAnonymous();
            endpoints.MapGet(API_SCHEMA + "/GetArticlesList", ArticleHandler.GetListForVisitors).AllowAnonymous();
            endpoints.MapGet(API_SCHEMA + "/GetRelatedArticlesList", ArticleHandler.GetRelatedArticlesForVisitors).AllowAnonymous();
            endpoints.MapGet(API_SCHEMA + "/GetTopArticle", ArticleHandler.GetTopArticleForVisitors).AllowAnonymous();
            endpoints.MapGet(API_SCHEMA + "/GetArticle", ArticleHandler.GetArticleByIdForVisitors).AllowAnonymous();
            endpoints.MapGet(API_SCHEMA + "/GetPage", PageHandler.GetPageByIdForVisitors).AllowAnonymous();
            endpoints.MapGet(API_SCHEMA + "/GetTopicsList", TopicHandler.GetList).AllowAnonymous();
            endpoints.MapGet(API_SCHEMA + "/GetTagsList", TagHandler.GetAllList).AllowAnonymous();
            endpoints.MapGet(API_SCHEMA + "/GetLinksByKeyList", LinkHandler.GetLinksByKeyList).AllowAnonymous();



            endpoints.MapPost(API_SCHEMA + "/AddOrUpdateSettings", SettingsHandler.AddOrUpdateSettings).RequirePermission(CmsPermissionTypes.CMS_SETTINGS_MANAGEMENT);

            endpoints.MapGet(API_SCHEMA + "/GetTopicsHierarchy", TopicHandler.GetTopicsHierarchy).RequirePermission(CmsPermissionTypes.CMS_TOPIC_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetTopicListForSelect", TopicHandler.GetListForSelect).RequireAuthorization();
            endpoints.MapGet(API_SCHEMA + "/GetTopicById", TopicHandler.GetTopicById).RequirePermission(CmsPermissionTypes.CMS_TOPIC_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddTopic", TopicHandler.AddTopic).RequirePermission(CmsPermissionTypes.CMS_TOPIC_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateTopic", TopicHandler.UpdateTopic).RequirePermission(CmsPermissionTypes.CMS_TOPIC_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteTopic", TopicHandler.DeleteTopic).RequirePermission(CmsPermissionTypes.CMS_TOPIC_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetTagList", TagHandler.GetList).RequirePermission(CmsPermissionTypes.CMS_TAG_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetTagListForSelect", TagHandler.GetListForSelect).RequireAuthorization();
            endpoints.MapGet(API_SCHEMA + "/GetTagById", TagHandler.GetTagById).RequirePermission(CmsPermissionTypes.CMS_TAG_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddTag", TagHandler.AddTag).RequirePermission(CmsPermissionTypes.CMS_TAG_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateTag", TagHandler.UpdateTag).RequirePermission(CmsPermissionTypes.CMS_TAG_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteTag", TagHandler.DeleteTag).RequirePermission(CmsPermissionTypes.CMS_TAG_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetArticleList", ArticleHandler.GetList).RequirePermission(CmsPermissionTypes.CMS_ARTICLE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/GetArticleTrashList", ArticleHandler.GetTrashList).RequirePermission(CmsPermissionTypes.CMS_ARTICLE_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetArticleById", ArticleHandler.GetArticleById).RequirePermission(CmsPermissionTypes.CMS_ARTICLE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddArticle", ArticleHandler.AddArticle).RequirePermission(CmsPermissionTypes.CMS_ARTICLE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateArticle", ArticleHandler.UpdateArticle).RequirePermission(CmsPermissionTypes.CMS_ARTICLE_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/PinArticle", ArticleHandler.PinArticle).RequirePermission(CmsPermissionTypes.CMS_ARTICLE_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteArticle", ArticleHandler.DeleteArticle).RequirePermission(CmsPermissionTypes.CMS_ARTICLE_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/RestoreArticle", ArticleHandler.RestoreArticle).RequirePermission(CmsPermissionTypes.CMS_ARTICLE_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/RemoveArticle", ArticleHandler.RemoveArticle).RequirePermission(CmsPermissionTypes.CMS_ARTICLE_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetPageList", PageHandler.GetList).RequirePermission(CmsPermissionTypes.CMS_PAGE_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetPageById", PageHandler.GetPageById).RequirePermission(CmsPermissionTypes.CMS_PAGE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddPage", PageHandler.AddPage).RequirePermission(CmsPermissionTypes.CMS_PAGE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdatePage", PageHandler.UpdatePage).RequirePermission(CmsPermissionTypes.CMS_PAGE_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/DeletePage", PageHandler.DeletePage).RequirePermission(CmsPermissionTypes.CMS_PAGE_MANAGEMENT);

            endpoints.MapGet(API_SCHEMA + "/GetLinkSectionList", LinkSectionHandler.GetList).RequirePermission(CmsPermissionTypes.CMS_LINK_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetLinkSectionById", LinkSectionHandler.GetLinkSectionById).RequirePermission(CmsPermissionTypes.CMS_LINK_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/VisibleLinkSection", LinkSectionHandler.VisibleLinkSection).RequirePermission(CmsPermissionTypes.CMS_LINK_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddLinkSection", LinkSectionHandler.AddLinkSection).RequirePermission(CmsPermissionTypes.CMS_LINK_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateLinkSection", LinkSectionHandler.UpdateLinkSection).RequirePermission(CmsPermissionTypes.CMS_LINK_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteLinkSection", LinkSectionHandler.DeleteLinkSection).RequirePermission(CmsPermissionTypes.CMS_LINK_MANAGEMENT);

            endpoints.MapGet(API_SCHEMA + "/GetLinkList", LinkHandler.GetList).RequirePermission(CmsPermissionTypes.CMS_LINK_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetLinkById", LinkHandler.GetLinkById).RequirePermission(CmsPermissionTypes.CMS_LINK_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateLinkOrders", LinkHandler.UpdateOrders).RequirePermission(CmsPermissionTypes.CMS_LINK_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddLink", LinkHandler.AddLink).RequirePermission(CmsPermissionTypes.CMS_LINK_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateLink", LinkHandler.UpdateLink).RequirePermission(CmsPermissionTypes.CMS_LINK_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteLink", LinkHandler.DeleteLink).RequirePermission(CmsPermissionTypes.CMS_LINK_MANAGEMENT);


            endpoints.MapGet(API_SCHEMA + "/GetMenusHierarchy", MenuHandler.GetMenusHierarchy).RequirePermission(CmsPermissionTypes.CMS_MENU_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetMenuById", MenuHandler.GetMenuById).RequirePermission(CmsPermissionTypes.CMS_MENU_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddMenu", MenuHandler.AddMenu).RequirePermission(CmsPermissionTypes.CMS_MENU_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateMenu", MenuHandler.UpdateMenu).RequirePermission(CmsPermissionTypes.CMS_MENU_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateMenuOrders", MenuHandler.UpdateOrders).RequirePermission(CmsPermissionTypes.CMS_MENU_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteMenu", MenuHandler.DeleteMenu).RequirePermission(CmsPermissionTypes.CMS_MENU_MANAGEMENT);

            endpoints.MapGet(API_SCHEMA + "/GetSlideshowList", SlideshowHandler.GetList).RequirePermission(CmsPermissionTypes.CMS_SLIDESHOW_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetSlideshowById", SlideshowHandler.GetSlideshowById).RequirePermission(CmsPermissionTypes.CMS_SLIDESHOW_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddSlideshow", SlideshowHandler.AddSlideshow).RequirePermission(CmsPermissionTypes.CMS_SLIDESHOW_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateSlideshow", SlideshowHandler.UpdateSlideshow).RequirePermission(CmsPermissionTypes.CMS_SLIDESHOW_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateSlideshowOrders", SlideshowHandler.UpdateOrders).RequirePermission(CmsPermissionTypes.CMS_SLIDESHOW_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/VisibleSlideshow", SlideshowHandler.VisibleSlideshow).RequirePermission(CmsPermissionTypes.CMS_SLIDESHOW_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteSlideshow", SlideshowHandler.DeleteSlideshow).RequirePermission(CmsPermissionTypes.CMS_SLIDESHOW_MANAGEMENT);

            return endpoints;
        }

    }
}

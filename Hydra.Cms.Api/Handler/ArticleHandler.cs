using Hydra.Kernel.GeneralModels;
using Hydra.Cms.Core.Interfaces;
using Hydra.Cms.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Hydra.Kernel;

namespace Hydra.Cms.Api.Handler
{
    public static class ArticleHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_articleService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(
             IArticleService _articleService, GridDataBound dataGrid)
        {
            var result = await _articleService.GetList(dataGrid);

            return Results.Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_articleService"></param>
        /// <param name="searchInput"></param>
        /// <param name="categoryName"></param>
        /// <param name="tagName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<IResult> GetListForVisitors(
             IArticleService _articleService, string? searchInput, string? categoryName, string? tagName, int pageIndex, int pageSize)
        {
            var result = await _articleService.GetListForVisitors(searchInput, categoryName, tagName, pageIndex, pageSize);

            return Results.Ok(result);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_articleService"></param>
        /// <param name="searchInput"></param>
        /// <param name="categoryName"></param>
        /// <param name="tagName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<IResult> GetRelatedArticlesForVisitors(
             IArticleService _articleService, int articleId)
        {
            var result = await _articleService.GetRelatedForVisitors(articleId, 3);

            return Results.Ok(result);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_articleService"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetTopArticleForVisitors(
            IArticleService _articleService
            )
        {
            var result = await _articleService.GetTopForVisitors();

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_articleService"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetArticleByIdForVisitors(
            IArticleService _articleService,
            int articleId
            )
        {
            var result = await _articleService.GetByIdForVisitors(articleId);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_articleService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static async Task<IResult> GetTrashList(
             IArticleService _articleService, GridDataBound dataGrid)
        {
            var result = await _articleService.GetTrashList(dataGrid);

            return Results.Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_articleService"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static async Task<IResult> GetArticleById(
            IArticleService _articleService,
            int articleId
            )
        {
            var result = await _articleService.GetById(articleId);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="_articleService"></param>
        /// <param name="articleModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddArticle(
            ClaimsPrincipal userClaim,
            IArticleService _articleService,
            [FromBody] ArticleModel articleModel
            )
        {
            var userId = userClaim.GetUserId();
            articleModel.WriterId = userId;
            var result = await _articleService.Add(articleModel);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="_articleService"></param>
        /// <param name="articleModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateArticle(
            ClaimsPrincipal userClaim,
            IArticleService _articleService,
            [FromBody] ArticleModel articleModel
            )
        {
            var userId = userClaim.GetUserId();
            articleModel.EditorId = userId;
            var result = await _articleService.Update(articleModel);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_articleService"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static async Task<IResult> PinArticle(
            IArticleService _articleService,
            int articleId
            )
        {
            var result = await _articleService.Pin(articleId);

            return Results.Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_articleService"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteArticle(
            IArticleService _articleService,
            int articleId
            )
        {
            var result = await _articleService.Delete(articleId);

            return Results.Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_articleService"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static async Task<IResult> RestoreArticle(
            IArticleService _articleService,
            int articleId
            )
        {
            var result = await _articleService.Restore(articleId);

            return Results.Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_articleService"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static async Task<IResult> RemoveArticle(
            IArticleService _articleService,
            int articleId
            )
        {
            var result = await _articleService.Remove(articleId);

            return Results.Ok(result);
        }

    }
}
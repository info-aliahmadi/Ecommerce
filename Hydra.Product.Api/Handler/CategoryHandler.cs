using System.Security.Claims;
using Hydra.Kernel;
using Hydra.Product.Core.Interfaces;
using Hydra.Product.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Product.Api.Handler
{
    public static class CategoryHandler
    {
        /// <summary>
        /// Retrieves the list of published categories using the specified category service.
        /// </summary>
        /// <remarks>This method wraps the result of the category service in a standardized HTTP response.
        /// If an exception occurs during retrieval, the error message is returned in a bad request result.</remarks>
        /// <param name="_categoryService">The category service used to obtain published categories. Cannot be null.</param>
        /// <returns>An <see cref="IResult"/> containing the published categories if the operation succeeds; otherwise, a bad
        /// request result with error details.</returns>
        public static IResult GetPublishedCategories(ICategoryService _categoryService)
        {
            try
            {
                var result = _categoryService.GetPublishedCategories();

                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Retrieves the hierarchical structure of categories using the specified category service and returns the
        /// result as an HTTP response.
        /// </summary>
        /// <remarks>The returned result uses HTTP 200 (OK) if the hierarchy retrieval is successful, or
        /// HTTP 400 (Bad Request) if it fails or an exception occurs. The response includes details about the
        /// operation's outcome.</remarks>
        /// <param name="_categoryService">The category service used to obtain the category hierarchy. Cannot be null.</param>
        /// <returns>An HTTP result containing the category hierarchy if the operation succeeds; otherwise, a bad request result
        /// with error details.</returns>
        public static IResult GetCategoryHierarchy(ICategoryService _categoryService)
        {
            try
            {
                var result =  _categoryService.GetHierarchy();

                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_categoryService"></param>
        /// <returns></returns>
        public static IResult GetListForSelect(ICategoryService _categoryService)
        {
            try
            {
                var result = _categoryService.GetListForSelect();

                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);

            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryService"></param>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static IResult GetList(ICategoryService categoryService)
        {
            try
            {
                var result = categoryService.GetList();
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryService"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static IResult GetCategoryById(ICategoryService categoryService, int categoryId)
        {
            var result = categoryService.GetById(categoryId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="categoryService"></param>
        /// <param name="categoryModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddCategory(ClaimsPrincipal userClaim, ICategoryService categoryService, [FromBody] CategoryModel categoryModel)
        {
            var result = await categoryService.Add(categoryModel);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userClaim"></param>
        /// <param name="categoryService"></param>
        /// <param name="categoryModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateCategory(ClaimsPrincipal userClaim, ICategoryService categoryService, [FromBody] CategoryModel categoryModel)
        {
            var result = await categoryService.Update(categoryModel);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_menuService"></param>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateOrders(ClaimsPrincipal userClaim,
            ICategoryService _menuService,
            [FromBody] List<CategoryModel> categoryList
            )
        {
            try
            {
                var userId = userClaim.GetUserId();
               
                var result = await _menuService.UpdateOrder(categoryList);

                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryService"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteCategory(ICategoryService categoryService, int categoryId)
        {
            try
            {
                var result = await categoryService.Delete(categoryId);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

    }
}
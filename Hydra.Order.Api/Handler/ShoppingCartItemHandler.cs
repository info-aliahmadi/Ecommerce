using System.Security.Claims;
using Hydra.Ecommerce.Core.Enums;
using Hydra.Kernel;
using Hydra.Kernel.GeneralModels;
using Hydra.Order.Core.Interfaces;
using Hydra.Order.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Order.Api.Handler
{
    public static class ShoppingCartItemHandler
    {

        public static async Task<IResult> GetList(IShoppingCartItemService shoppingCartItemService, GridDataBound dataGrid)
        {
            try
            {
                var result = await shoppingCartItemService.GetList(dataGrid);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        public static async Task<IResult> GetShoppingCartItemById(IShoppingCartItemService shoppingCartItemService, int shoppingCartItemId)
        {
            var result = await shoppingCartItemService.GetById(shoppingCartItemId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> AddShoppingCartItem(ClaimsPrincipal userClaim, IShoppingCartItemService shoppingCartItemService, [FromBody] ShoppingCartItemModel shoppingCartItemModel)
        {
            var result = await shoppingCartItemService.Add(shoppingCartItemModel);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> UpdateShoppingCartItem(ClaimsPrincipal userClaim, IShoppingCartItemService shoppingCartItemService, [FromBody] ShoppingCartItemModel shoppingCartItemModel)
        {
            var result = await shoppingCartItemService.Update(shoppingCartItemModel);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> DeleteShoppingCartItem(IShoppingCartItemService shoppingCartItemService, int shoppingCartItemId)
        {
            try
            {
                var result = await shoppingCartItemService.Delete(shoppingCartItemId);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        // --- User-facing endpoints ---

        public static async Task<IResult> GetMyCartItems(ClaimsPrincipal userClaim, IShoppingCartItemService shoppingCartItemService)
        {
            var userId = userClaim.GetUserId();
            var result = await shoppingCartItemService.GetByUserIdAndType(userId, ShoppingCartTypeEnum.ShoppingCart);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> GetMyWishlistItems(ClaimsPrincipal userClaim, IShoppingCartItemService shoppingCartItemService)
        {
            var userId = userClaim.GetUserId();
            var result = await shoppingCartItemService.GetByUserIdAndType(userId, ShoppingCartTypeEnum.Wishlist);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> GetAllMyShoppingItems(ClaimsPrincipal userClaim, IShoppingCartItemService shoppingCartItemService)
        {
            var userId = userClaim.GetUserId();
            var result = await shoppingCartItemService.GetByUserId(userId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> AddToCart(ClaimsPrincipal userClaim, IShoppingCartItemService shoppingCartItemService, [FromBody] AddToCartRequest request)
        {
            var userId = userClaim.GetUserId();
            var result = await shoppingCartItemService.AddToCart(userId, request.ProductVariantId, request.Quantity);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> AddToWishlist(ClaimsPrincipal userClaim, IShoppingCartItemService shoppingCartItemService, [FromBody] AddToWishlistRequest request)
        {
            var userId = userClaim.GetUserId();
            var result = await shoppingCartItemService.AddToWishlist(userId, request.ProductVariantId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> RemoveFromCart(ClaimsPrincipal userClaim, IShoppingCartItemService shoppingCartItemService, [FromBody] RemoveFromCartRequest request)
        {
            var userId = userClaim.GetUserId();
            var result = await shoppingCartItemService.RemoveFromCart(userId, request.ProductVariantId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> RemoveFromWishlist(ClaimsPrincipal userClaim, IShoppingCartItemService shoppingCartItemService, [FromBody] RemoveFromWishlistRequest request)
        {
            var userId = userClaim.GetUserId();
            var result = await shoppingCartItemService.RemoveFromWishlist(userId, request.ProductVariantId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> ClearCart(ClaimsPrincipal userClaim, IShoppingCartItemService shoppingCartItemService)
        {
            var userId = userClaim.GetUserId();
            var result = await shoppingCartItemService.ClearCart(userId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> ClearWishlist(ClaimsPrincipal userClaim, IShoppingCartItemService shoppingCartItemService)
        {
            var userId = userClaim.GetUserId();
            var result = await shoppingCartItemService.ClearWishlist(userId);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }

        public static async Task<IResult> UpdateCartItemQuantity(IShoppingCartItemService shoppingCartItemService, [FromBody] UpdateQuantityRequest request)
        {
            var result = await shoppingCartItemService.UpdateQuantity(request.ItemId, request.Quantity);
            return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
        }
    }
  
}

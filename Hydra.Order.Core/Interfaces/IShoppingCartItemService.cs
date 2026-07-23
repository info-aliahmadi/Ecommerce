using Hydra.Ecommerce.Core.Enums;
using Hydra.Kernel.GeneralModels;
using Hydra.Order.Core.Models;

namespace Hydra.Order.Core.Interfaces
{
    public interface IShoppingCartItemService
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<PaginatedList<ShoppingCartItemModel>>> GetList(GridDataBound dataGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<ShoppingCartItemModel>> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="shoppingCartItemModel"></param>
        /// <returns></returns>
        Task<Result<ShoppingCartItemModel>> Add(ShoppingCartItemModel shoppingCartItemModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="shoppingCartItemModel"></param>
        /// <returns></returns>
        Task<Result<ShoppingCartItemModel>> Update(ShoppingCartItemModel shoppingCartItemModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

        /// <summary>
        /// Get all cart and wishlist items for a user.
        /// </summary>
        Task<Result<List<ShoppingCartItemModel>>> GetByUserId(int userId);

        /// <summary>
        /// Get cart or wishlist items for a user filtered by type.
        /// </summary>
        Task<Result<List<ShoppingCartItemModel>>> GetByUserIdAndType(int userId, ShoppingCartTypeEnum type);

        /// <summary>
        /// Add item to cart. If the product already exists in cart, increments quantity instead.
        /// </summary>
        Task<Result<ShoppingCartItemModel>> AddToCart(int userId, int productVariantId, int quantity);

        /// <summary>
        /// Add item to wishlist. No duplicates allowed per product.
        /// </summary>
        Task<Result<ShoppingCartItemModel>> AddToWishlist(int userId, int productVariantId);

        /// <summary>
        /// Remove a specific product from the user's cart.
        /// </summary>
        Task<Result> RemoveFromCart(int userId, int productVariantId);

        /// <summary>
        /// Remove a specific product from the user's wishlist.
        /// </summary>
        Task<Result> RemoveFromWishlist(int userId, int productVariantId);

        /// <summary>
        /// Clear all cart items for a user.
        /// </summary>
        Task<Result> ClearCart(int userId);

        /// <summary>
        /// Clear all wishlist items for a user.
        /// </summary>
        Task<Result> ClearWishlist(int userId);

        /// <summary>
        /// Update the quantity of a cart item.
        /// </summary>
        Task<Result<ShoppingCartItemModel>> UpdateQuantity(int itemId, int quantity);

    }
}

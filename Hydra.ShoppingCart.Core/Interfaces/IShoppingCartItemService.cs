using Hydra.Ecommerce.Core.Enums;
using Hydra.Kernel.GeneralModels;
using Hydra.ShoppingCart.Core.Models;

namespace Hydra.ShoppingCart.Core.Interfaces
{
    public interface IShoppingCartItemService
    {

        Task<Result<PaginatedList<ShoppingCartItemModel>>> GetList(GridDataBound dataGrid);
        Task<Result<ShoppingCartItemModel>> GetById(int id);
        Task<Result<ShoppingCartItemModel>> Add(ShoppingCartItemModel shoppingCartItemModel);
        Task<Result<ShoppingCartItemModel>> Update(ShoppingCartItemModel shoppingCartItemModel);
        Task<Result> Delete(int id);
        Task<Result<List<ShoppingCartItemModel>>> GetByUserId(int userId);
        Task<Result<List<ShoppingCartItemModel>>> GetByUserIdAndType(int userId, ShoppingCartTypeEnum type);
        Task<Result<ShoppingCartItemModel>> AddToCart(int userId, int productVariantId, int quantity);
        Task<Result<ShoppingCartItemModel>> AddToWishlist(int userId, int productVariantId);
        Task<Result> RemoveFromCart(int userId, int productVariantId);
        Task<Result> RemoveFromWishlist(int userId, int productVariantId);
        Task<Result> ClearCart(int userId);
        Task<Result> ClearWishlist(int userId);
        Task<Result<ShoppingCartItemModel>> UpdateQuantity(int itemId, int quantity);

    }
}

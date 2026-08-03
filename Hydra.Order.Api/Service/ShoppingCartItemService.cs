using Hydra.Ecommerce.Core.Domain;
using Hydra.Ecommerce.Core.Enums;
using Hydra.Infrastructure.Data;
using Hydra.Kernel.Extension;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Hydra.Order.Core.Interfaces;
using Hydra.Order.Core.Models;
using Hydra.Product.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Hydra.Order.Api.Services
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;
        public ShoppingCartItemService(IQueryRepository queryRepository, ICommandRepository commandRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        public async Task<Result<PaginatedList<ShoppingCartItemModel>>> GetList(GridDataBound dataGrid)
        {
            var result = new Result<PaginatedList<ShoppingCartItemModel>>();

            var list = await (from shoppingCartItem in _queryRepository.Table<ShoppingCartItem>()
                              select new ShoppingCartItemModel()
                              {
                                  Id = shoppingCartItem.Id,
                                  UserId = shoppingCartItem.UserId,
                                  ProductVariantId = shoppingCartItem.ProductVariantId,
                                  ShoppingCartTypeId = shoppingCartItem.ShoppingCartTypeId,
                                  Quantity = shoppingCartItem.Quantity,
                              }).OrderByDescending(x => x.Id).ToPaginatedListAsync(dataGrid);

            result.Data = list;
            return result;
        }

        public async Task<Result<ShoppingCartItemModel>> GetById(int id)
        {
            var result = new Result<ShoppingCartItemModel>();
            var shoppingCartItem = await _queryRepository.Table<ShoppingCartItem>().FirstOrDefaultAsync(x => x.Id == id);

            if (shoppingCartItem is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The ShoppingCartItem not found";
                return result;
            }

            result.Data = MapToModel(shoppingCartItem);
            return result;
        }

        public async Task<Result<ShoppingCartItemModel>> Add(ShoppingCartItemModel shoppingCartItemModel)
        {
            var result = new Result<ShoppingCartItemModel>();
            try
            {
                var shoppingCartItem = new ShoppingCartItem()
                {
                    UserId = shoppingCartItemModel.UserId,
                    ProductVariantId = shoppingCartItemModel.ProductVariantId,
                    ShoppingCartTypeId = shoppingCartItemModel.ShoppingCartTypeId,
                    Quantity = shoppingCartItemModel.Quantity,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedOnUtc = DateTime.UtcNow,
                };

                await _commandRepository.InsertAsync(shoppingCartItem);
                await _commandRepository.SaveChangesAsync();

                shoppingCartItemModel.Id = shoppingCartItem.Id;
                result.Data = shoppingCartItemModel;
                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        public async Task<Result<ShoppingCartItemModel>> Update(ShoppingCartItemModel shoppingCartItemModel)
        {
            var result = new Result<ShoppingCartItemModel>();
            try
            {
                var shoppingCartItem = await _queryRepository.Table<ShoppingCartItem>().FirstOrDefaultAsync(x => x.Id == shoppingCartItemModel.Id);
                if (shoppingCartItem is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The ShoppingCartItem not found";
                    return result;
                }

                shoppingCartItem.UserId = shoppingCartItemModel.UserId;
                shoppingCartItem.ProductVariantId = shoppingCartItemModel.ProductVariantId;
                shoppingCartItem.ShoppingCartTypeId = shoppingCartItemModel.ShoppingCartTypeId;
                shoppingCartItem.Quantity = shoppingCartItemModel.Quantity;
                shoppingCartItem.UpdatedOnUtc = DateTime.UtcNow;

                _commandRepository.Update(shoppingCartItem);
                await _commandRepository.SaveChangesAsync();

                result.Data = shoppingCartItemModel;
                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        public async Task<Result> Delete(int id)
        {
            var result = new Result();
            var shoppingCartItem = await _queryRepository.Table<ShoppingCartItem>().FirstOrDefaultAsync(x => x.Id == id);
            if (shoppingCartItem is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The ShoppingCartItem not found";
                return result;
            }

            _commandRepository.Delete(shoppingCartItem);
            await _commandRepository.SaveChangesAsync();
            return result;
        }

        public async Task<Result<List<ShoppingCartItemModel>>> GetByUserId(int userId)
        {
            var result = new Result<List<ShoppingCartItemModel>>();

            var items = await _queryRepository.Table<ShoppingCartItem>()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOnUtc)
                .ToListAsync();

            result.Data = items.Select(MapToModel).ToList();
            return result;
        }

        public async Task<Result<List<ShoppingCartItemModel>>> GetByUserIdAndType(int userId, ShoppingCartTypeEnum type)
        {
            var result = new Result<List<ShoppingCartItemModel>>();

            var items = await _queryRepository.Table<ShoppingCartItem>().Include(x => x.ProductVariant).ThenInclude(x => x.Product).ThenInclude(x => x.ProductCategories)
                .Where(x => x.UserId == userId && x.ShoppingCartTypeId == type)
                .OrderByDescending(x => x.CreatedOnUtc).Select(item => new ShoppingCartItemModel
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    Name = item.ProductVariant.Product.Name,
                    ProductVariantId = item.ProductVariantId,
                    Variant = new Product.Core.Models.ProductVariantDisplayModel(item.ProductVariant),
                    ShoppingCartTypeId = item.ShoppingCartTypeId,
                    Quantity = item.Quantity,
                    Categories = item.ProductVariant.Product.ProductCategories.Select(x => new CategoryDisplayModel(x.Category)).ToList(),
                    Image = item.ProductVariant.Product.ImagePreview != null ? new FileStorage.Core.Models.FileUploadModel(item.ProductVariant.Product.ImagePreview) : null,
                })
                .ToListAsync();

            result.Data = items;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productVariantId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public async Task<Result<ShoppingCartItemModel>> AddToCart(int userId, AddToCartRequest request)
        {
            var result = new Result<ShoppingCartItemModel>();
            try
            {
                var existing = await _queryRepository.Table<ShoppingCartItem>()
                    .FirstOrDefaultAsync(x => x.UserId == userId
                        && x.ProductVariantId == request.VariantId
                        && x.ShoppingCartTypeId == ShoppingCartTypeEnum.ShoppingCart);

                ShoppingCartItem cartItem;

                if (existing != null)
                {
                    existing.Quantity += request.Quantity;
                    existing.UpdatedOnUtc = DateTime.UtcNow;
                    _commandRepository.Update(existing);
                    cartItem = existing;
                }
                else
                {
                    cartItem = new ShoppingCartItem()
                    {
                        UserId = userId,
                        ProductVariantId = request.VariantId,
                        ShoppingCartTypeId = ShoppingCartTypeEnum.ShoppingCart,
                        Quantity = request.Quantity,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    };

                    await _commandRepository.InsertAsync(cartItem);
                }
                _commandRepository.SaveChanges();
                result.Data = MapToModel(cartItem);
                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productVariantId"></param>
        /// <returns></returns>
        public async Task<Result<ShoppingCartItemModel>> AddToWishlist(int userId, int variantId)
        {
            var result = new Result<ShoppingCartItemModel>();
            try
            {
                var existing = await _queryRepository.Table<ShoppingCartItem>()
                    .FirstOrDefaultAsync(x => x.UserId == userId
                        && x.ProductVariantId == variantId
                        && x.ShoppingCartTypeId == ShoppingCartTypeEnum.Wishlist);

                if (existing != null)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "Item already in wishlist";
                    return result;
                }

                var item = new ShoppingCartItem()
                {
                    UserId = userId,
                    ProductVariantId = variantId,
                    ShoppingCartTypeId = ShoppingCartTypeEnum.Wishlist,
                    Quantity = 1,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedOnUtc = DateTime.UtcNow,
                };

                await _commandRepository.InsertAsync(item);
                await _commandRepository.SaveChangesAsync();

                result.Data = MapToModel(item);
                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productVariantId"></param>
        /// <returns></returns>
        public async Task<Result> RemoveFromCart(int userId, int variantId)
        {
            var result = new Result();
            var item = await _queryRepository.Table<ShoppingCartItem>()
                .FirstOrDefaultAsync(x => x.UserId == userId
                    && x.ProductVariantId == variantId
                    && x.ShoppingCartTypeId == ShoppingCartTypeEnum.ShoppingCart);

            if (item is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "Item not found in cart";
                return result;
            }

            _commandRepository.Delete(item);
            await _commandRepository.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productVariantId"></param>
        /// <returns></returns>
        public async Task<Result> RemoveFromWishlist(int userId, int variantId)
        {
            var result = new Result();
            var item = await _queryRepository.Table<ShoppingCartItem>()
                .FirstOrDefaultAsync(x => x.UserId == userId
                    && x.ProductVariantId == variantId
                    && x.ShoppingCartTypeId == ShoppingCartTypeEnum.Wishlist);

            if (item is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "Item not found in wishlist";
                return result;
            }

            _commandRepository.Delete(item);
            await _commandRepository.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Result> ClearCart(int userId)
        {
            var result = new Result();
            var items = await _queryRepository.Table<ShoppingCartItem>()
                .Where(x => x.UserId == userId && x.ShoppingCartTypeId == ShoppingCartTypeEnum.ShoppingCart)
                .ToListAsync();

            if (items.Count == 0)
                return result;
            foreach (var item in items)
            {
                _commandRepository.Delete(item);
            }
            await _commandRepository.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Result> ClearWishlist(int userId)
        {
            var result = new Result();
            var items = await _queryRepository.Table<ShoppingCartItem>()
                .Where(x => x.UserId == userId && x.ShoppingCartTypeId == ShoppingCartTypeEnum.Wishlist)
                .ToListAsync();

            if (items.Count == 0)
                return result;

            _commandRepository.Delete(items);
            await _commandRepository.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public async Task<Result<ShoppingCartItemModel>> UpdateQuantity(int userId, UpdateQuantityRequest request)
        {
            var result = new Result<ShoppingCartItemModel>();
            try
            {
                var item = await _queryRepository.Table<ShoppingCartItem>().FirstOrDefaultAsync(x => x.UserId == userId && x.ProductVariantId == request.VariantId);
                if (item is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The ShoppingCartItem not found";
                    return result;
                }
                if (request.Quantity == 0)
                {
                    await RemoveFromCart(userId, request.VariantId);
                }
                else
                {
                    item.Quantity = request.Quantity;
                    item.UpdatedOnUtc = DateTime.UtcNow;

                    _commandRepository.Update(item);
                    await _commandRepository.SaveChangesAsync();
                }
                result.Data = MapToModel(item);
                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        private static ShoppingCartItemModel MapToModel(ShoppingCartItem item)
        {
            return new ShoppingCartItemModel()
            {
                Id = item.Id,
                UserId = item.UserId,
                ProductVariantId = item.ProductVariantId,
                ShoppingCartTypeId = item.ShoppingCartTypeId,
                Quantity = item.Quantity
            };
        }
    }
}

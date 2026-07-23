using Hydra.Ecommerce.Core.Domain;
using Hydra.Ecommerce.Core.Enums;
using Hydra.Kernel.Extension;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Hydra.Order.Core.Interfaces;
using Hydra.Order.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Hydra.Order.Api.Service
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
                                  CreatedOnUtc = shoppingCartItem.CreatedOnUtc,
                                  UpdatedOnUtc = shoppingCartItem.UpdatedOnUtc,
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

            var items = await _queryRepository.Table<ShoppingCartItem>()
                .Where(x => x.UserId == userId && x.ShoppingCartTypeId == type)
                .OrderByDescending(x => x.CreatedOnUtc)
                .ToListAsync();

            result.Data = items.Select(MapToModel).ToList();
            return result;
        }

        public async Task<Result<ShoppingCartItemModel>> AddToCart(int userId, int productVariantId, int quantity)
        {
            var result = new Result<ShoppingCartItemModel>();
            try
            {
                var existing = await _queryRepository.Table<ShoppingCartItem>()
                    .FirstOrDefaultAsync(x => x.UserId == userId
                        && x.ProductVariantId == productVariantId
                        && x.ShoppingCartTypeId == ShoppingCartTypeEnum.ShoppingCart);

                if (existing != null)
                {
                    existing.Quantity += quantity;
                    existing.UpdatedOnUtc = DateTime.UtcNow;
                    _commandRepository.Update(existing);
                    await _commandRepository.SaveChangesAsync();
                    result.Data = MapToModel(existing);
                    return result;
                }

                var item = new ShoppingCartItem()
                {
                    UserId = userId,
                    ProductVariantId = productVariantId,
                    ShoppingCartTypeId = ShoppingCartTypeEnum.ShoppingCart,
                    Quantity = quantity,
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

        public async Task<Result<ShoppingCartItemModel>> AddToWishlist(int userId, int productVariantId)
        {
            var result = new Result<ShoppingCartItemModel>();
            try
            {
                var existing = await _queryRepository.Table<ShoppingCartItem>()
                    .FirstOrDefaultAsync(x => x.UserId == userId
                        && x.ProductVariantId == productVariantId
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
                    ProductVariantId = productVariantId,
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

        public async Task<Result> RemoveFromCart(int userId, int productVariantId)
        {
            var result = new Result();
            var item = await _queryRepository.Table<ShoppingCartItem>()
                .FirstOrDefaultAsync(x => x.UserId == userId
                    && x.ProductVariantId == productVariantId
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

        public async Task<Result> RemoveFromWishlist(int userId, int productVariantId)
        {
            var result = new Result();
            var item = await _queryRepository.Table<ShoppingCartItem>()
                .FirstOrDefaultAsync(x => x.UserId == userId
                    && x.ProductVariantId == productVariantId
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

        public async Task<Result> ClearCart(int userId)
        {
            var result = new Result();
            var items = await _queryRepository.Table<ShoppingCartItem>()
                .Where(x => x.UserId == userId && x.ShoppingCartTypeId == ShoppingCartTypeEnum.ShoppingCart)
                .ToListAsync();

            if (items.Count == 0)
                return result;

            _commandRepository.Delete(items);
            await _commandRepository.SaveChangesAsync();
            return result;
        }

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

        public async Task<Result<ShoppingCartItemModel>> UpdateQuantity(int itemId, int quantity)
        {
            var result = new Result<ShoppingCartItemModel>();
            try
            {
                var item = await _queryRepository.Table<ShoppingCartItem>().FirstOrDefaultAsync(x => x.Id == itemId);
                if (item is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The ShoppingCartItem not found";
                    return result;
                }

                item.Quantity = quantity;
                item.UpdatedOnUtc = DateTime.UtcNow;

                _commandRepository.Update(item);
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

        private static ShoppingCartItemModel MapToModel(ShoppingCartItem item)
        {
            return new ShoppingCartItemModel()
            {
                Id = item.Id,
                UserId = item.UserId,
                ProductVariantId = item.ProductVariantId,
                ShoppingCartTypeId = item.ShoppingCartTypeId,
                Quantity = item.Quantity,
                CreatedOnUtc = item.CreatedOnUtc,
                UpdatedOnUtc = item.UpdatedOnUtc,
            };
        }
    }
}

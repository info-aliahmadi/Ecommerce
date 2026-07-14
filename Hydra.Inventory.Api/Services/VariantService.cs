using Hydra.Ecommerce.Core.Domain;
using Hydra.Ecommerce.Core.Enums;
using Hydra.Inventory.Core.Interfaces;
using Hydra.Inventory.Core.Models;
using Hydra.Kernel.Extension;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hydra.Inventory.Api.Services
{
    public class VariantService : IVariantService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;

        public VariantService(IQueryRepository queryRepository, ICommandRepository commandRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        public async Task<Result<PaginatedList<VariantModel>>> GetList(GridDataBound dataGrid)
        {
            var result = new Result<PaginatedList<VariantModel>>();

            var list = await (from variant in _queryRepository.Table<ProductVariant>()
                                .Include(v => v.Product)
                                .Include(v => v.ProductInventory)
                                .Include(v => v.VariantAttributes).ThenInclude(va => va.Attribute)
                              select new VariantModel
                              {
                                  Id = variant.Id,
                                  SKU = variant.SKU,
                                  ProductId = variant.ProductId,
                                  ProductName = variant.Product.Name,
                                  SellPrice = variant.SellPrice,
                                  OldSellPrice = variant.OldSellPrice,
                                  StockQuantity = variant.ProductInventory != null ? variant.ProductInventory.StockQuantity : 0,
                                  ReservedQuantity = variant.ProductInventory != null ? variant.ProductInventory.ReservedQuantity : 0,
                                  Attributes = variant.VariantAttributes.Select(va => new VariantAttributeModel
                                  {
                                      AttributeId = va.AttributeId,
                                      AttributeName = va.Attribute.DisplayName,
                                      AttributeValue = va.Attribute.Key,
                                      AttributeType = va.Attribute.AttributeType,
                                  }).ToList(),
                              }).OrderByDescending(x => x.Id).ToPaginatedListAsync(dataGrid);

            result.Data = list;
            return result;
        }

        public async Task<Result<List<VariantModel>>> GetListByProductId(int productId)
        {
            var result = new Result<List<VariantModel>>();

            var list = await _queryRepository.Table<ProductVariant>()
                .Include(v => v.Product)
                .Include(v => v.ProductInventory)
                .Include(v => v.VariantAttributes).ThenInclude(va => va.Attribute)
                .Where(v => v.ProductId == productId)
                .Select(variant => new VariantModel
                {
                    Id = variant.Id,
                    SKU = variant.SKU,
                    ProductId = variant.ProductId,
                    ProductName = variant.Product.Name,
                    SellPrice = variant.SellPrice,
                    OldSellPrice = variant.OldSellPrice,
                    StockQuantity = variant.ProductInventory != null ? variant.ProductInventory.StockQuantity : 0,
                    ReservedQuantity = variant.ProductInventory != null ? variant.ProductInventory.ReservedQuantity : 0,
                    Attributes = variant.VariantAttributes.Select(va => new VariantAttributeModel
                    {
                        AttributeId = va.AttributeId,
                        AttributeName = va.Attribute.DisplayName,
                        AttributeValue = va.Attribute.Key,
                        AttributeType = va.Attribute.AttributeType,
                    }).ToList(),
                }).ToListAsync();

            result.Data = list;
            return result;
        }

        public async Task<Result<VariantModel>> GetById(int id)
        {
            var result = new Result<VariantModel>();

            var variant = await _queryRepository.Table<ProductVariant>()
                .Include(v => v.Product)
                .Include(v => v.ProductInventory)
                .Include(v => v.VariantAttributes).ThenInclude(va => va.Attribute).ThenInclude(a => a.ImagePreview)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (variant is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The Variant not found";
                return result;
            }

            result.Data = new VariantModel
            {
                Id = variant.Id,
                SKU = variant.SKU,
                ProductId = variant.ProductId,
                ProductName = variant.Product.Name,
                SellPrice = variant.SellPrice,
                OldSellPrice = variant.OldSellPrice,
                StockQuantity = variant.ProductInventory != null ? variant.ProductInventory.StockQuantity : 0,
                ReservedQuantity = variant.ProductInventory != null ? variant.ProductInventory.ReservedQuantity : 0,
                Attributes = variant.VariantAttributes.Select(va => new VariantAttributeModel
                {
                    AttributeId = va.AttributeId,
                    AttributeName = va.Attribute.DisplayName,
                    AttributeValue = va.Attribute.Key,
                    AttributeType = va.Attribute.AttributeType,
                }).ToList(),
            };

            return result;
        }

        public async Task<Result<VariantModel>> Add(VariantModel model)
        {
            var result = new Result<VariantModel>();
            try
            {
                var isSkuExist = await _queryRepository.Table<ProductVariant>()
                    .AnyAsync(v => v.SKU == model.SKU);
                if (isSkuExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The SKU already exists";
                    result.Errors.Add(new Error(nameof(model.SKU), "The SKU already exists"));
                    return result;
                }

                var isProductExist = await _queryRepository.Table<Ecommerce.Core.Domain.Product>()
                    .AnyAsync(p => p.Id == model.ProductId);
                if (!isProductExist)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The Product not found";
                    return result;
                }

                var currentDateTime = DateTime.UtcNow;

                await using var transaction = await _commandRepository.BeginTransactionAsync();
                try
                {
                    var variant = new ProductVariant
                    {
                        SKU = model.SKU,
                        ProductId = model.ProductId,
                        SellPrice = model.SellPrice,
                        OldSellPrice = model.OldSellPrice,
                    };

                    await _commandRepository.InsertAsync(variant);
                    await _commandRepository.SaveChangesAsync();

                    var inventory = new ProductInventory
                    {
                        VariantId = variant.Id,
                        StockQuantity = model.StockQuantity,
                        ReservedQuantity = model.ReservedQuantity,
                        CreatedDatetime = currentDateTime,
                    };

                    await _commandRepository.InsertAsync(inventory);

                    foreach (var attr in model.Attributes)
                    {
                        var variantAttr = new ProductVariantAttribute
                        {
                            VariantId = variant.Id,
                            AttributeId = attr.AttributeId,
                        };
                        await _commandRepository.InsertAsync(variantAttr);
                    }

                    await _commandRepository.SaveChangesAsync();
                    await transaction.CommitAsync();

                    model.Id = variant.Id;
                    result.Data = model;
                    return result;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        public async Task<Result<VariantModel>> Update(VariantModel model)
        {
            var result = new Result<VariantModel>();
            try
            {
                var variant = await _queryRepository.Table<ProductVariant>()
                    .Include(v => v.ProductInventory)
                    .Include(v => v.VariantAttributes)
                    .FirstOrDefaultAsync(v => v.Id == model.Id);

                if (variant is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The Variant not found";
                    return result;
                }

                var isSkuExist = await _queryRepository.Table<ProductVariant>()
                    .AnyAsync(v => v.Id != model.Id && v.SKU == model.SKU);
                if (isSkuExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The SKU already exists";
                    result.Errors.Add(new Error(nameof(model.SKU), "The SKU already exists"));
                    return result;
                }

                await using var transaction = await _commandRepository.BeginTransactionAsync();
                try
                {
                    variant.SKU = model.SKU;
                    variant.SellPrice = model.SellPrice;
                    variant.OldSellPrice = model.OldSellPrice;

                    _commandRepository.UpdateAsync(variant);

                    if (variant.ProductInventory != null)
                    {
                        variant.ProductInventory.StockQuantity = model.StockQuantity;
                        variant.ProductInventory.ReservedQuantity = model.ReservedQuantity;
                        _commandRepository.UpdateAsync(variant.ProductInventory);
                    }
                    else
                    {
                        var inventory = new ProductInventory
                        {
                            VariantId = variant.Id,
                            StockQuantity = model.StockQuantity,
                            ReservedQuantity = model.ReservedQuantity,
                            CreatedDatetime = DateTime.UtcNow,
                        };
                        await _commandRepository.InsertAsync(inventory);
                    }

                    var existingAttrIds = variant.VariantAttributes.Select(va => va.AttributeId).ToList();
                    var newAttrIds = model.Attributes.Select(a => a.AttributeId).ToList();

                    var attrsToRemove = variant.VariantAttributes
                        .Where(va => !newAttrIds.Contains(va.AttributeId))
                        .ToList();
                    foreach (var attr in attrsToRemove)
                    {
                        _commandRepository.DeleteAsync(attr);
                    }

                    var attrsToAdd = newAttrIds
                        .Where(id => !existingAttrIds.Contains(id))
                        .Select(id => new ProductVariantAttribute
                        {
                            VariantId = variant.Id,
                            AttributeId = id,
                        })
                        .ToList();
                    foreach (var attr in attrsToAdd)
                    {
                        await _commandRepository.InsertAsync(attr);
                    }

                    await _commandRepository.SaveChangesAsync();
                    await transaction.CommitAsync();

                    result.Data = model;
                    return result;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
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

            var variant = await _queryRepository.Table<ProductVariant>()
                .Include(v => v.ProductInventory).ThenInclude(i => i.InventoryTransactions)
                .Include(v => v.VariantAttributes)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (variant is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The Variant not found";
                return result;
            }

            if (variant.ProductInventory?.InventoryTransactions != null)
            {
                foreach (var txn in variant.ProductInventory.InventoryTransactions)
                {
                    _commandRepository.DeleteAsync(txn);
                }
            }

            foreach (var attr in variant.VariantAttributes)
            {
                _commandRepository.DeleteAsync(attr);
            }

            if (variant.ProductInventory != null)
            {
                _commandRepository.DeleteAsync(variant.ProductInventory);
            }

            _commandRepository.DeleteAsync(variant);
            await _commandRepository.SaveChangesAsync();

            return result;
        }

        public async Task<Result> UpdateStock(StockAdjustmentModel model)
        {
            var result = new Result();
            try
            {
                var inventory = await _queryRepository.Table<ProductInventory>()
                    .FirstOrDefaultAsync(i => i.VariantId == model.VariantId);

                if (inventory is null)
                {
                    inventory = new ProductInventory
                    {
                        VariantId = model.VariantId,
                        StockQuantity = 0,
                        ReservedQuantity = 0,
                        CreatedDatetime = DateTime.UtcNow,
                    };
                    await _commandRepository.InsertAsync(inventory);
                    await _commandRepository.SaveChangesAsync();
                }

                inventory.StockQuantity += model.Quantity;

                if (inventory.StockQuantity < 0)
                {
                    result.Status = ResultStatusEnum.InvalidValidation;
                    result.Message = "Stock quantity cannot be negative";
                    return result;
                }

                _commandRepository.UpdateAsync(inventory);

                var transaction = new ProductInventoryTransaction
                {
                    ProductInventoryId = inventory.Id,
                    TransactionType = model.TransactionType,
                    StockQuantity = model.Quantity,
                    ReservedQuantity = 0,
                    CreatedDatetime = DateTime.UtcNow,
                };

                await _commandRepository.InsertAsync(transaction);
                await _commandRepository.SaveChangesAsync();

                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        public async Task<Result<List<InventoryTransactionModel>>> GetInventoryTransactions(int variantId)
        {
            var result = new Result<List<InventoryTransactionModel>>();

            var inventory = await _queryRepository.Table<ProductInventory>()
                .FirstOrDefaultAsync(i => i.VariantId == variantId);

            if (inventory is null)
            {
                result.Data = new List<InventoryTransactionModel>();
                return result;
            }

            var transactions = await _queryRepository.Table<ProductInventoryTransaction>()
                .Where(t => t.ProductInventoryId == inventory.Id)
                .OrderByDescending(t => t.CreatedDatetime)
                .Select(t => new InventoryTransactionModel
                {
                    Id = t.Id,
                    VariantId = variantId,
                    TransactionType = t.TransactionType,
                    StockQuantity = t.StockQuantity,
                    ReservedQuantity = t.ReservedQuantity,
                    CreatedDatetime = t.CreatedDatetime,
                }).ToListAsync();

            result.Data = transactions;
            return result;
        }
    }
}

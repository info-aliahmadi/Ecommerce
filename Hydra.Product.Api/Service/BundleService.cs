using EFCoreSecondLevelCacheInterceptor;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Hydra.Ecommerce.Core.Domain;
using Hydra.Product.Core.Interfaces;
using Hydra.Product.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Hydra.Product.Api.Services
{
    public class BundleService : IBundleService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;

        public BundleService(IQueryRepository queryRepository, ICommandRepository commandRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        public async Task<Result<List<BundleDisplayModel>>> GetPublishedBundles()
        {
            var result = new Result<List<BundleDisplayModel>>();

            var list = await (from bundle in _queryRepository.Table<Bundle>().Include(x=>x.ProductBundles).ThenInclude(x=>x.Product).ThenInclude(p=>p.ProductVariants).ThenInclude(v=>v.ProductInventory)
                              where bundle.ShowOnHomepage
                              select new BundleDisplayModel()
                              {
                                  Id = bundle.Id,
                                  Name = bundle.Name,
                                  Description = bundle.Description,
                                  DisplayOrder = bundle.DisplayOrder,
                                  ShowOnHomepage = bundle.ShowOnHomepage,
                                  ProductsCount = bundle.ProductBundles.Count(),
                                  Products = bundle.ProductBundles.OrderBy(x=>x.DisplayOrder).Select(pb => new ProductDisplayModel()
                                  {
                                      Id = pb.Product.Id,
                                      Name = pb.Product.Name,
                                      SKU = pb.Product.SKU,
                                      CreateUserId = pb.Product.CreateUserId,
                                      UpdateUserId = pb.Product.UpdateUserId,
                                      MetaKeywords = pb.Product.MetaKeywords,
                                      MetaTitle = pb.Product.MetaTitle,
                                      FullDescription = pb.Product.FullDescription,
                                      ShortDescription = pb.Product.ShortDescription,
                                      AdminComment = pb.Product.AdminComment,
                                      MetaDescription = pb.Product.MetaDescription,
                                      DeliveryDateType = pb.Product.DeliveryDateType,
                                      TaxCategoryId = pb.Product.TaxCategoryId,
                                      TaxCategoryName = pb.Product.TaxCategory.Name,
                                      StockQuantity = pb.Product.ProductVariants.Sum(v => v.ProductInventory != null ? v.ProductInventory.StockQuantity : 0),
                                      MinStockQuantity = pb.Product.MinStockQuantity,
                                      OrderMinimumQuantity = pb.Product.OrderMinimumQuantity,
                                      OrderMaximumQuantity = pb.Product.OrderMaximumQuantity,
                                      CurrencyType = pb.Product.CurrencyType,
                                      DisplayOrder = pb.Product.DisplayOrder,
                                      ApprovedRatingSum = pb.Product.ApprovedRatingSum,
                                      NotApprovedRatingSum = pb.Product.NotApprovedRatingSum,
                                      ApprovedTotalReviews = pb.Product.ApprovedTotalReviews,
                                      NotApprovedTotalReviews = pb.Product.NotApprovedTotalReviews,
                                      HasDiscountsApplied = pb.Product.HasDiscountsApplied,
                                      MarkAsNew = pb.Product.MarkAsNew,
                                      MarkAsNewStartDateTimeUtc = pb.Product.MarkAsNewStartDateTimeUtc,
                                      MarkAsNewEndDateTimeUtc = pb.Product.MarkAsNewEndDateTimeUtc,
                                      NotReturnable = pb.Product.NotReturnable,
                                      AllowedQuantities = pb.Product.AllowedQuantities,
                                      IsTaxExempt = pb.Product.IsTaxExempt,
                                      ShowOnHomepage = pb.Product.ShowOnHomepage,
                                      IsFreeShipping = pb.Product.IsFreeShipping,
                                      AllowCustomerReviews = pb.Product.AllowCustomerReviews,
                                      DisplayStockQuantity = pb.Product.DisplayStockQuantity,
                                      DisableBuyButton = pb.Product.DisableBuyButton,
                                      DisableWishlistButton = pb.Product.DisableWishlistButton,
                                      AvailableForPreOrder = pb.Product.AvailableForPreOrder,
                                      CallForPrice = pb.Product.CallForPrice,
                                      CreatedOnUtc = pb.Product.CreatedOnUtc,
                                      UpdatedOnUtc = pb.Product.UpdatedOnUtc,
                                      ImagePreview = new FileStorage.Core.Models.FileUploadModel(pb.Product.ImagePreview),
                                      ImagePaths = pb.Product.ProductImages.Select(x => x.Image.FullPath).ToList(),
                                      Categories = pb.Product.ProductCategories.Select(cat => new CategoryDisplayModel()
                                      {
                                          Id = cat.CategoryId,
                                          Name = cat.Category.Name,
                                          ImagePreview = new FileStorage.Core.Models.FileUploadModel(cat.Category.ImagePreview),
                                          Color = cat.Category.Color,
                                      }).ToList(),
                                      ManufacturerNames = pb.Product.ProductManufacturers.Select(c => c.Manufacturer.Name).ToList(),
                                      Attributes = pb.Product.ProductAttributes.Select(c => c.Attribute).Select(z => new ProductAttributeDisplayModel()
                                      {
                                          Id = z.Id,
                                          AttributeType = z.AttributeType,
                                          Description = z.Description,
                                          DisplayOrder = z.DisplayOrder,
                                          DisplayName = z.DisplayName,
                                          ImagePreview = new FileStorage.Core.Models.FileUploadModel(z.ImagePreview),
                                          Key = z.Key,
                                      }).ToList(),
                                      ProductTags = pb.Product.ProductProductTags.Select(x => x.ProductTag).Select(cat => cat.Name).ToList(),
                                      Variants = pb.Product.ProductVariants.Select(v => new ProductVariantDisplayModel()
                                      {
                                          Id = v.Id,
                                          SKU = v.SKU,
                                          ProductId = v.ProductId,
                                          SellPrice = v.SellPrice,
                                          OldSellPrice = v.OldSellPrice,
                                          ProductInventory = new ProductInventoryDisplayModel()
                                          {
                                              Id = v.ProductInventory.Id,
                                              VariantId = v.ProductInventory.VariantId,
                                              StockQuantity = v.ProductInventory.StockQuantity,
                                              ReservedQuantity = v.ProductInventory.ReservedQuantity
                                          },
                                          ProductAttributes = v.VariantAttributes != null ? v.VariantAttributes.Select(va => new ProductAttributeDisplayModel()
                                          {
                                              Id = va.Attribute.Id,
                                              DisplayName = va.Attribute.DisplayName,
                                              Key = va.Attribute.Key,
                                              AttributeType = va.Attribute.AttributeType,
                                              DisplayOrder = va.Attribute.DisplayOrder,
                                              Description = va.Attribute.Description,
                                          }).ToList() : new List<ProductAttributeDisplayModel>()
                                      }).ToList()
                                  }).ToList()
                              }).OrderBy(x => x.DisplayOrder).Cacheable().ToListAsync();
            result.Data = list;
            return result;
        }

        public async Task<Result<List<BundleModel>>> GetBundlesList()
        {
            var result = new Result<List<BundleModel>>();

            var list = await (from bundle in _queryRepository.Table<Bundle>()
                              select new BundleModel()
                              {
                                  Id = bundle.Id,
                                  Name = bundle.Name,
                                  Description = bundle.Description,
                                  DisplayOrder = bundle.DisplayOrder,
                                  ShowOnHomepage = bundle.ShowOnHomepage,
                                  CreatedOnUtc = bundle.CreatedOnUtc,
                                  Products = bundle.ProductBundles.Select(pb => new ProductBundleModel()
                                  {
                                      ProductId = pb.ProductId,
                                      DisplayOrder = pb.DisplayOrder
                                  }).ToList()
                              }).OrderBy(x => x.DisplayOrder).Cacheable().ToListAsync();
            result.Data = list;
            return result;
        }

        public async Task<Result<List<BundleModel>>> GetListForSelect()
        {
            return await GetBundlesList();
        }

        public async Task<Result<BundleModel>> GetById(int id)
        {
            var result = new Result<BundleModel>();
            var bundle = await _queryRepository.Table<Bundle>()
                .Include(x => x.ProductBundles)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (bundle is null)
            {
                result.Data = new BundleModel();
                return result;
            }

            result.Data = new BundleModel()
            {
                Id = bundle.Id,
                Name = bundle.Name,
                Description = bundle.Description,
                DisplayOrder = bundle.DisplayOrder,
                ShowOnHomepage = bundle.ShowOnHomepage,
                CreatedOnUtc = bundle.CreatedOnUtc,
                Products = bundle.ProductBundles.Select(pb => new ProductBundleModel()
                {
                    ProductId = pb.ProductId,
                    DisplayOrder = pb.DisplayOrder
                }).ToList()
            };
            return result;
        }

        public async Task<Result<BundleModel>> Add(BundleModel bundleModel)
        {
            var result = new Result<BundleModel>();
            try
            {
                bool isExist = await _queryRepository.Table<Bundle>().AnyAsync(x => x.Name == bundleModel.Name);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Name already exists";
                    result.Errors.Add(new Error(nameof(bundleModel.Id), "The Name already exists"));
                    return result;
                }

                var date = DateTime.UtcNow;
                var bundle = new Bundle()
                {
                    Name = bundleModel.Name,
                    Description = bundleModel.Description,
                    DisplayOrder = bundleModel.DisplayOrder,
                    ShowOnHomepage = bundleModel.ShowOnHomepage,
                    CreatedOnUtc = date
                };

                await _commandRepository.InsertAsync(bundle);
                await _commandRepository.SaveChangesAsync();

                if (bundleModel.Products?.Any() == true)
                {
                    foreach (var pb in bundleModel.Products)
                    {
                        var productBundle = new ProductBundle()
                        {
                            BundleId = bundle.Id,
                            ProductId = pb.ProductId,
                            DisplayOrder = pb.DisplayOrder
                        };
                        await _commandRepository.InsertAsync(productBundle);
                    }
                    await _commandRepository.SaveChangesAsync();
                }

                bundleModel.Id = bundle.Id;
                result.Data = bundleModel;
                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        public async Task<Result<BundleModel>> Update(BundleModel bundleModel)
        {
            var result = new Result<BundleModel>();
            try
            {
                var bundle = await _queryRepository.Table<Bundle>()
                    .Include(x => x.ProductBundles)
                    .FirstOrDefaultAsync(x => x.Id == bundleModel.Id);
                if (bundle is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The Bundle not found";
                    return result;
                }

                var isExist = await _queryRepository.Table<Bundle>().AnyAsync(x => x.Id != bundleModel.Id && x.Name == bundleModel.Name);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Name already exists";
                    result.Errors.Add(new Error(nameof(bundleModel.Id), "The Name already exists"));
                    return result;
                }

                bundle.Name = bundleModel.Name;
                bundle.Description = bundleModel.Description;
                bundle.DisplayOrder = bundleModel.DisplayOrder;
                bundle.ShowOnHomepage = bundleModel.ShowOnHomepage;

                _commandRepository.Update(bundle);

                // Sync ProductBundles: remove existing, add new ones
                if (bundle.ProductBundles?.Any() == true)
                {
                    foreach (var existing in bundle.ProductBundles)
                    {
                        _commandRepository.Delete(existing);
                    }
                }

                if (bundleModel.Products?.Any() == true)
                {
                    foreach (var pb in bundleModel.Products)
                    {
                        var productBundle = new ProductBundle()
                        {
                            BundleId = bundle.Id,
                            ProductId = pb.ProductId,
                            DisplayOrder = pb.DisplayOrder
                        };
                        await _commandRepository.InsertAsync(productBundle);
                    }
                }

                await _commandRepository.SaveChangesAsync();

                result.Data = bundleModel;
                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        public async Task<Result<List<BundleModel>>> UpdateOrder(List<BundleModel> linkModelList)
        {
            var result = new Result<List<BundleModel>>();

            var ids = linkModelList.Select(x => x.Id).ToArray();
            var i = linkModelList.Count;
            foreach (var item in linkModelList)
            {
                item.DisplayOrder = i--;
            }

            var visibleList = _queryRepository.Table<Bundle>().Where(x => ids.Contains(x.Id)).ToList();
            foreach (var item in visibleList)
            {
                var model = linkModelList.First(x => x.Id == item.Id);
                item.DisplayOrder = model.DisplayOrder;
                _commandRepository.Update(item);
            }

            await _commandRepository.SaveChangesAsync();

            result.Data = linkModelList;
            return result;
        }

        public async Task<Result> Delete(int id)
        {
            var result = new Result();
            var bundle = await _queryRepository.Table<Bundle>()
                .Include(x => x.ProductBundles)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (bundle is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The Bundle not found";
                return result;
            }

            // Sync ProductBundles: remove existing, add new ones
            if (bundle.ProductBundles?.Any() == true)
            {
                foreach (var existing in bundle.ProductBundles)
                {
                    _commandRepository.Delete(existing);
                }
            }

            _commandRepository.Delete(bundle);
            await _commandRepository.SaveChangesAsync();

            return result;
        }
    }
}

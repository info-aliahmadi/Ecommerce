using EFCoreSecondLevelCacheInterceptor;
using Hydra.Ecommerce.Core.Domain;
using Hydra.Ecommerce.Core.Enums;
using Hydra.Kernel.Extension;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Hydra.Product.Core.Interfaces;
using Hydra.Product.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Hydra.Product.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;
        public ProductService(IQueryRepository queryRepository, ICommandRepository commandRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        public IQueryable<Ecommerce.Core.Domain.Product> GetPublishedQuery()
        {
            var currentDateTime = DateTime.UtcNow;
            return _queryRepository.Table<Ecommerce.Core.Domain.Product>()
                            .Include(x => x.ProductVariants).ThenInclude(v => v.ProductInventory)
                            .Include(x => x.ProductVariants).ThenInclude(v => v.VariantAttributes).ThenInclude(va => va.Attribute).ThenInclude(a => a.ImagePreview)
                            .Include(x => x.ProductCategories).ThenInclude(x => x.Category).ThenInclude(x => x.ImagePreview)
                            .Include(x => x.ProductManufacturers).ThenInclude(x => x.Manufacturer).ThenInclude(x => x.ImagePreview)
                            .Include(x => x.ProductProductTags).ThenInclude(x => x.ProductTag)
                            .Include(x => x.ProductAttributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.ImagePreview)
                            .Include(x => x.ProductImages).ThenInclude(x => x.Image)
                            .Include(x => x.ImagePreview)
                            .Include(x => x.TaxCategory)
                            .Where(x => x.Deleted == false && x.Published == true &&
                            ((x.AvailableStartDateTimeUtc != null && x.AvailableStartDateTimeUtc <= currentDateTime) ||
                            (x.AvailableEndDateTimeUtc != null && x.AvailableEndDateTimeUtc >= currentDateTime)));
        }

        public async Task<Result<PaginatedList<ProductDisplayModel>>> GetPublishedProducts(ProductFilterDisplayModel productFilter)
        {
            var result = new Result<PaginatedList<ProductDisplayModel>>();
            var currentDateTime = DateTime.UtcNow;
            var query = GetPublishedQuery();

            var maxRange = query.SelectMany(x => x.ProductVariants).Max(v => v.SellPrice);

            if (!string.IsNullOrEmpty(productFilter.SearchInput))
            {
                var searchLower = productFilter.SearchInput.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(searchLower) ||
                                         x.MetaKeywords.ToLower().Contains(searchLower) ||
                                         x.MetaTitle.ToLower().Contains(searchLower) ||
                                         x.MetaDescription.ToLower().Contains(searchLower) ||
                                         x.FullDescription.ToLower().Contains(searchLower) ||
                                         x.ShortDescription.ToLower().Contains(searchLower));
            }

            if (productFilter.HasStockQuantity.HasValue)
            {
                if (productFilter.HasStockQuantity.Value == true)
                {
                    query = query.Where(x => x.ProductVariants.Any(v => v.ProductInventory != null && v.ProductInventory.StockQuantity > 0));
                }
                else
                {
                    query = query.Where(x => !x.ProductVariants.Any(v => v.ProductInventory != null && v.ProductInventory.StockQuantity > 0));
                }
            }

            if (productFilter.CategoryIds.Count > 0)
                query = query.Where(x => x.ProductCategories.Any(c => productFilter.CategoryIds.Contains(c.CategoryId)));

            if (productFilter.ManufacturerIds.Count > 0)
                query = query.Where(x => x.ProductManufacturers.Any(m => productFilter.ManufacturerIds.Contains(m.ManufacturerId)));

            if (productFilter.ProductTagIds.Count > 0)
                query = query.Where(x => x.ProductProductTags.Any(c => productFilter.ProductTagIds.Contains(c.ProductTagId)));

            if (productFilter.AttributeTypes.Count > 0)
                query = query.Where(x => x.ProductAttributes.Any(c => productFilter.AttributeTypes.Contains(c.Attribute.AttributeType)));

            if (productFilter.AttributeIds.Count > 0)
                query = query.Where(x => x.ProductAttributes.Any(c => productFilter.AttributeIds.Contains(c.AttributeId)));

            if (productFilter.DateFilter.HasValue)
            {
                switch (productFilter.DateFilter)
                {
                    case DateFilter.Today:
                        query = query.Where(x => x.AvailableStartDateTimeUtc.Date == currentDateTime.Date);
                        break;
                    case DateFilter.ThisWeek:
                        var startOfWeek = currentDateTime.AddDays(-(int)currentDateTime.DayOfWeek);
                        query = query.Where(x => x.AvailableStartDateTimeUtc.Date >= startOfWeek);
                        break;
                    case DateFilter.ThisMonth:
                        query = query.Where(x => x.AvailableStartDateTimeUtc.Month == currentDateTime.Month && x.AvailableStartDateTimeUtc.Year == currentDateTime.Year);
                        break;
                    case DateFilter.Last3Months:
                        var threeMonthsAgo = currentDateTime.AddMonths(-3);
                        query = query.Where(x => x.AvailableStartDateTimeUtc >= threeMonthsAgo);
                        break;
                    case DateFilter.Last6Months:
                        var sixMonthsAgo = currentDateTime.AddMonths(-6);
                        query = query.Where(x => x.AvailableStartDateTimeUtc >= sixMonthsAgo);
                        break;
                    case DateFilter.ThisYear:
                        query = query.Where(x => x.AvailableStartDateTimeUtc.Year == currentDateTime.Year);
                        break;
                }
            }

            if (productFilter.HasDiscounts.HasValue && productFilter.HasDiscounts == true)
                query = query.Where(x => x.ProductVariants.Any(v => v.SellPrice < v.OldSellPrice && v.OldSellPrice > 0));

            if (productFilter.FromSellUnitPrice.HasValue)
                query = query.Where(x => x.ProductVariants.Any(v => v.SellPrice >= productFilter.FromSellUnitPrice.Value));

            if (productFilter.ToSellUnitPrice.HasValue)
                query = query.Where(x => x.ProductVariants.Any(v => v.SellPrice <= productFilter.ToSellUnitPrice.Value));

            if (productFilter.Sorting != null)
            {
                switch (productFilter.Sorting)
                {
                    case Kernel.Enums.SortingType.SortNewest:
                        query = query.OrderBy(x => x.AvailableStartDateTimeUtc); break;
                    case Kernel.Enums.SortingType.SortOldest:
                        query = query.OrderByDescending(x => x.AvailableStartDateTimeUtc); break;
                    case Kernel.Enums.SortingType.SortPopular:
                        query = query.OrderByDescending(x => x.ApprovedTotalReviews); break;
                    case Kernel.Enums.SortingType.SortRating:
                        query = query.OrderByDescending(x => x.ApprovedRatingSum); break;
                    case Kernel.Enums.SortingType.SortNameAsc:
                        query = query.OrderBy(x => x.Name); break;
                    case Kernel.Enums.SortingType.SortNameDesc:
                        query = query.OrderByDescending(x => x.Name); break;
                    case Kernel.Enums.SortingType.SortPriceAsc:
                        query = query.OrderBy(x => x.ProductVariants.Min(v => v.SellPrice)); break;
                    case Kernel.Enums.SortingType.SortPriceDesc:
                        query = query.OrderByDescending(x => x.ProductVariants.Max(v => v.SellPrice)); break;
                    default:
                        query = query.OrderBy(x => x.AvailableStartDateTimeUtc); break;
                }
            }

            var list = (from product in query
                        select new ProductDisplayModel()
                        {
                            Id = product.Id,
                            Name = product.Name,
                            SKU = product.SKU,
                            CreateUserId = product.CreateUserId,
                            UpdateUserId = product.UpdateUserId,
                            MetaKeywords = product.MetaKeywords,
                            MetaTitle = product.MetaTitle,
                            FullDescription = product.FullDescription,
                            ShortDescription = product.ShortDescription,
                            AdminComment = product.AdminComment,
                            MetaDescription = product.MetaDescription,
                            DeliveryDateType = product.DeliveryDateType,
                            TaxCategoryId = product.TaxCategoryId,
                            TaxCategoryName = product.TaxCategory.Name,
                            StockQuantity = product.ProductVariants.Sum(v => v.ProductInventory != null ? v.ProductInventory.StockQuantity : 0),
                            MeasureType = product.MeasureType,
                            MinStockQuantity = product.MinStockQuantity,
                            OrderMinimumQuantity = product.OrderMinimumQuantity,
                            OrderMaximumQuantity = product.OrderMaximumQuantity,
                            CurrencyType = product.CurrencyType,
                            DisplayOrder = product.DisplayOrder,
                            ApprovedRatingSum = product.ApprovedRatingSum,
                            NotApprovedRatingSum = product.NotApprovedRatingSum,
                            ApprovedTotalReviews = product.ApprovedTotalReviews,
                            NotApprovedTotalReviews = product.NotApprovedTotalReviews,
                            HasDiscountsApplied = product.HasDiscountsApplied,
                            MarkAsNew = product.MarkAsNew,
                            MarkAsNewStartDateTimeUtc = product.MarkAsNewStartDateTimeUtc,
                            MarkAsNewEndDateTimeUtc = product.MarkAsNewEndDateTimeUtc,
                            NotReturnable = product.NotReturnable,
                            AllowedQuantities = product.AllowedQuantities,
                            IsTaxExempt = product.IsTaxExempt,
                            ShowOnHomepage = product.ShowOnHomepage,
                            IsFreeShipping = product.IsFreeShipping,
                            AllowCustomerReviews = product.AllowCustomerReviews,
                            DisplayStockQuantity = product.DisplayStockQuantity,
                            DisableBuyButton = product.DisableBuyButton,
                            DisableWishlistButton = product.DisableWishlistButton,
                            AvailableForPreOrder = product.AvailableForPreOrder,
                            CallForPrice = product.CallForPrice,
                            CreatedOnUtc = product.CreatedOnUtc,
                            UpdatedOnUtc = product.UpdatedOnUtc,
                            ImagePreview = new FileStorage.Core.Models.FileUploadModel(product.ImagePreview),
                            ImagePaths = product.ProductImages.Select(x => x.Image.FullPath).ToList(),
                            Categories = product.ProductCategories.Select(cat => new CategoryDisplayModel()
                            {
                                Id = cat.CategoryId,
                                Name = cat.Category.Name,
                                ImagePreview = new FileStorage.Core.Models.FileUploadModel(cat.Category.ImagePreview),
                                Color = cat.Category.Color,
                            }).ToList(),
                            ManufacturerNames = product.ProductManufacturers.Select(c => c.Manufacturer.Name).ToList(),
                            Attributes = product.ProductAttributes.Select(c => c.Attribute).Select(z => new ProductAttributeDisplayModel()
                            {
                                Id = z.Id,
                                AttributeType = z.AttributeType,
                                Description = z.Description,
                                DisplayOrder = z.DisplayOrder,
                                DisplayName = z.DisplayName,
                                ImagePreview = new FileStorage.Core.Models.FileUploadModel(z.ImagePreview),
                                Key = z.Key,
                            }).ToList(),
                            ProductTags = product.ProductProductTags.Select(x => x.ProductTag).Select(cat => cat.Name).ToList(),
                            Variants = product.ProductVariants.Select(v => new ProductVariantDisplayModel()
                            {
                                Id = v.Id,
                                SKU = v.SKU,
                                ProductId = v.ProductId,
                                SellPrice = v.SellPrice,
                                OldSellPrice = v.OldSellPrice,
                                ProductInventory = v.ProductInventory != null ? new ProductInventoryDisplayModel()
                                {
                                    Id = v.ProductInventory.Id,
                                    VariantId = v.ProductInventory.VariantId,
                                    StockQuantity = v.ProductInventory.StockQuantity,
                                    ReservedQuantity = v.ProductInventory.ReservedQuantity
                                } : null,
                                ProductAttributes = v.VariantAttributes.Select(va => new ProductAttributeDisplayModel()
                                {
                                    Id = va.Attribute.Id,
                                    AttributeType = va.Attribute.AttributeType,
                                    Description = va.Attribute.Description,
                                    DisplayOrder = va.Attribute.DisplayOrder,
                                    DisplayName = va.Attribute.DisplayName,
                                    ImagePreview = new FileStorage.Core.Models.FileUploadModel(va.Attribute.ImagePreview),
                                    Key = va.Attribute.Key,
                                }).ToList(),
                            }).ToList(),
                        }).AsNoTracking().Cacheable();

            result.Data = await list.ToPaginatedListAsync(productFilter.PageIndex, productFilter.PageSize);
            result.Data.MaxRange = maxRange;
            return result;
        }

        public async Task<Result<List<CuratedStyleProductModel>>> GetPublishedCuratedStyleProducts()
        {
            var result = new Result<List<CuratedStyleProductModel>>();
            var currentDateTime = DateTime.UtcNow;

            var products = await _queryRepository.Table<Ecommerce.Core.Domain.Product>()
                .Include(x => x.ProductVariants).ThenInclude(v => v.ProductInventory)
                .Include(x => x.ProductVariants).ThenInclude(v => v.VariantAttributes).ThenInclude(va => va.Attribute)
                .Include(x => x.ProductCategories).ThenInclude(x => x.Category)
                .Include(x => x.ProductManufacturers).ThenInclude(x => x.Manufacturer)
                .Include(x => x.ImagePreview)
                .Include(x => x.ProductAttributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.ImagePreview)
                .Include(x => x.ProductProductTags).ThenInclude(x => x.ProductTag)
                .Include(x => x.ProductImages).ThenInclude(x => x.Image)
                .Where(x => x.Deleted == false && x.Published == true &&
                    ((x.AvailableStartDateTimeUtc != null && x.AvailableStartDateTimeUtc <= currentDateTime) ||
                            (x.AvailableEndDateTimeUtc != null && x.AvailableEndDateTimeUtc >= currentDateTime)) &&
                    x.ProductAttributes.Any(a =>
                        a.Attribute.AttributeType == AttributeType.Style))
                .AsNoTracking().ToListAsync();

            var groups = products
                .SelectMany(p => p.ProductAttributes
                    .Where(a => a.Attribute.AttributeType == AttributeType.Style)
                    .Select(a => new { a.Attribute.Id, a.Attribute, Product = p }))
                .GroupBy(x => x.Id)
                .Select(g => new CuratedStyleProductModel
                {
                    AttributeId = g.Key,
                    AttributeName = g.First().Attribute.DisplayName,
                    AttributeValue = g.First().Attribute.Key,
                    AttributeDescription = g.First().Attribute.Description,
                    ImagePreview = new FileStorage.Core.Models.FileUploadModel(g.First().Attribute.ImagePreview),
                    Products = g.Select(x => new ProductDisplayModel
                    {
                        Id = x.Product.Id,
                        Name = x.Product.Name,
                        SKU = x.Product.SKU,
                        CreateUserId = x.Product.CreateUserId,
                        UpdateUserId = x.Product.UpdateUserId,
                        MetaKeywords = x.Product.MetaKeywords,
                        MetaTitle = x.Product.MetaTitle,
                        FullDescription = x.Product.FullDescription,
                        ShortDescription = x.Product.ShortDescription,
                        AdminComment = x.Product.AdminComment,
                        MetaDescription = x.Product.MetaDescription,
                        DeliveryDateType = x.Product.DeliveryDateType,
                        TaxCategoryId = x.Product.TaxCategoryId,
                        TaxCategoryName = x.Product.TaxCategory?.Name,
                        MeasureType = x.Product.MeasureType,
                        StockQuantity = x.Product.ProductVariants.Sum(v => v.ProductInventory != null ? v.ProductInventory.StockQuantity : 0),
                        MinStockQuantity = x.Product.MinStockQuantity,
                        OrderMinimumQuantity = x.Product.OrderMinimumQuantity,
                        OrderMaximumQuantity = x.Product.OrderMaximumQuantity,
                        CurrencyType = x.Product.CurrencyType,
                        DisplayOrder = x.Product.DisplayOrder,
                        ApprovedRatingSum = x.Product.ApprovedRatingSum,
                        NotApprovedRatingSum = x.Product.NotApprovedRatingSum,
                        ApprovedTotalReviews = x.Product.ApprovedTotalReviews,
                        NotApprovedTotalReviews = x.Product.NotApprovedTotalReviews,
                        HasDiscountsApplied = x.Product.HasDiscountsApplied,
                        MarkAsNew = x.Product.MarkAsNew,
                        MarkAsNewStartDateTimeUtc = x.Product.MarkAsNewStartDateTimeUtc,
                        MarkAsNewEndDateTimeUtc = x.Product.MarkAsNewEndDateTimeUtc,
                        NotReturnable = x.Product.NotReturnable,
                        AllowedQuantities = x.Product.AllowedQuantities,
                        IsTaxExempt = x.Product.IsTaxExempt,
                        ShowOnHomepage = x.Product.ShowOnHomepage,
                        IsFreeShipping = x.Product.IsFreeShipping,
                        AllowCustomerReviews = x.Product.AllowCustomerReviews,
                        DisplayStockQuantity = x.Product.DisplayStockQuantity,
                        DisableBuyButton = x.Product.DisableBuyButton,
                        DisableWishlistButton = x.Product.DisableWishlistButton,
                        AvailableForPreOrder = x.Product.AvailableForPreOrder,
                        CallForPrice = x.Product.CallForPrice,
                        CreatedOnUtc = x.Product.CreatedOnUtc,
                        UpdatedOnUtc = x.Product.UpdatedOnUtc,
                        ImagePreview = new FileStorage.Core.Models.FileUploadModel(x.Product.ImagePreview),
                        ImagePaths = x.Product.ProductImages.Select(pi => pi.Image.FullPath).ToList(),
                        Categories = x.Product.ProductCategories.Select(cat => new CategoryDisplayModel()
                        {
                            Id = cat.CategoryId,
                            Name = cat.Category.Name,
                            ImagePreview = new FileStorage.Core.Models.FileUploadModel(cat.Category.ImagePreview),
                            Color = cat.Category.Color,
                        }).ToList(),
                        ManufacturerNames = x.Product.ProductManufacturers.Select(c => c.Manufacturer.Name).ToList(),
                        ProductTags = x.Product.ProductProductTags.Select(t => t.ProductTag).Select(cat => cat.Name).ToList(),
                    }).Take(4).ToList()
                })
                .ToList();

            result.Data = groups;
            return result;
        }

        public async Task<Result<ProductDisplayModel>> GetPublishedProductById(int id)
        {
            var result = new Result<ProductDisplayModel>();
            var product = await GetPublishedQuery().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            var productModel = new ProductDisplayModel()
            {
                Id = product.Id,
                Name = product.Name,
                SKU = product.SKU,
                CreateUserId = product.CreateUserId,
                UpdateUserId = product.UpdateUserId,
                MetaKeywords = product.MetaKeywords,
                MetaTitle = product.MetaTitle,
                FullDescription = product.FullDescription,
                ShortDescription = product.ShortDescription,
                AdminComment = product.AdminComment,
                MetaDescription = product.MetaDescription,
                DeliveryDateType = product.DeliveryDateType,
                TaxCategoryId = product.TaxCategoryId,
                TaxCategoryName = product.TaxCategory.Name,
                MeasureType = product.MeasureType,
                StockQuantity = product.ProductVariants.Sum(v => v.ProductInventory != null ? v.ProductInventory.StockQuantity : 0),
                MinStockQuantity = product.MinStockQuantity,
                OrderMinimumQuantity = product.OrderMinimumQuantity,
                OrderMaximumQuantity = product.OrderMaximumQuantity,
                CurrencyType = product.CurrencyType,
                DisplayOrder = product.DisplayOrder,
                ApprovedRatingSum = product.ApprovedRatingSum,
                NotApprovedRatingSum = product.NotApprovedRatingSum,
                ApprovedTotalReviews = product.ApprovedTotalReviews,
                NotApprovedTotalReviews = product.NotApprovedTotalReviews,
                HasDiscountsApplied = product.HasDiscountsApplied,
                MarkAsNew = product.MarkAsNew,
                MarkAsNewStartDateTimeUtc = product.MarkAsNewStartDateTimeUtc,
                MarkAsNewEndDateTimeUtc = product.MarkAsNewEndDateTimeUtc,
                NotReturnable = product.NotReturnable,
                AllowedQuantities = product.AllowedQuantities,
                IsTaxExempt = product.IsTaxExempt,
                ShowOnHomepage = product.ShowOnHomepage,
                IsFreeShipping = product.IsFreeShipping,
                AllowCustomerReviews = product.AllowCustomerReviews,
                DisplayStockQuantity = product.DisplayStockQuantity,
                DisableBuyButton = product.DisableBuyButton,
                DisableWishlistButton = product.DisableWishlistButton,
                AvailableForPreOrder = product.AvailableForPreOrder,
                CallForPrice = product.CallForPrice,
                CreatedOnUtc = product.CreatedOnUtc,
                UpdatedOnUtc = product.UpdatedOnUtc,
                ImagePreview = new FileStorage.Core.Models.FileUploadModel(product.ImagePreview),
                ImagePaths = product.ProductImages.Select(x => x.Image.FullPath).ToList(),
                Categories = product.ProductCategories.Select(cat => new CategoryDisplayModel()
                {
                    Id = cat.CategoryId,
                    Name = cat.Category.Name,
                    Key = cat.Category.Key,
                    ImagePreview = new FileStorage.Core.Models.FileUploadModel(cat.Category.ImagePreview),
                    Color = cat.Category.Color,
                }).ToList(),
                ManufacturerNames = product.ProductManufacturers.Select(c => c.Manufacturer.Name).ToList(),
                Attributes = product.ProductAttributes.Select(c => c.Attribute).Select(z => new ProductAttributeDisplayModel()
                {
                    Id = z.Id,
                    AttributeType = z.AttributeType,
                    Description = z.Description,
                    DisplayOrder = z.DisplayOrder,
                    DisplayName = z.DisplayName,
                    ImagePreview = new FileStorage.Core.Models.FileUploadModel(z.ImagePreview),
                    Key = z.Key,
                }).ToList(),
                ProductTags = product.ProductProductTags.Select(x => x.ProductTag).Select(cat => cat.Name).ToList(),
                Variants = product.ProductVariants.Select(v => new ProductVariantDisplayModel()
                {
                    Id = v.Id,
                    SKU = v.SKU,
                    ProductId = v.ProductId,
                    SellPrice = v.SellPrice,
                    OldSellPrice = v.OldSellPrice,
                    ProductInventory = v.ProductInventory != null ? new ProductInventoryDisplayModel()
                    {
                        Id = v.ProductInventory.Id,
                        VariantId = v.ProductInventory.VariantId,
                        StockQuantity = v.ProductInventory.StockQuantity,
                        ReservedQuantity = v.ProductInventory.ReservedQuantity
                    } : null,
                    ProductAttributes = v.VariantAttributes.Select(va => new ProductAttributeDisplayModel()
                    {
                        Id = va.Attribute.Id,
                        AttributeType = va.Attribute.AttributeType,
                        Description = va.Attribute.Description,
                        DisplayOrder = va.Attribute.DisplayOrder,
                        DisplayName = va.Attribute.DisplayName,
                        ImagePreview = new FileStorage.Core.Models.FileUploadModel(va.Attribute.ImagePreview),
                        Key = va.Attribute.Key,
                    }).ToList(),
                }).ToList(),
            };
            result.Data = productModel;
            return result;
        }

        public async Task<Result<PaginatedList<ProductModel>>> GetList(GridDataBound dataGrid)
        {
            var result = new Result<PaginatedList<ProductModel>>();

            var list = await (from product in _queryRepository.Table<Ecommerce.Core.Domain.Product>()
                                .Include(x => x.ProductVariants).ThenInclude(v => v.ProductInventory)
                                .Include(x => x.ProductVariants).ThenInclude(v => v.VariantAttributes).ThenInclude(va => va.Attribute)
                                .Include(x => x.ProductCategories)
                                .Include(x => x.ProductManufacturers)
                                .Include(x => x.ProductAttributes)
                                .Where(x => x.Deleted == false)
                              select new ProductModel()
                              {
                                  Id = product.Id,
                                  Name = product.Name,
                                  SKU = product.SKU,
                                  CreateUserId = product.CreateUserId,
                                  UpdateUserId = product.UpdateUserId,
                                  MetaKeywords = product.MetaKeywords,
                                  MetaTitle = product.MetaTitle,
                                  FullDescription = product.FullDescription,
                                  ShortDescription = product.ShortDescription,
                                  AdminComment = product.AdminComment,
                                  MetaDescription = product.MetaDescription,
                                  DeliveryDateType = product.DeliveryDateType,
                                  TaxCategoryId = product.TaxCategoryId,
                                  TaxCategoryName = product.TaxCategory.Name,
                                  MeasureType = product.MeasureType,
                                  StockQuantity = product.ProductVariants.Sum(v => v.ProductInventory != null ? v.ProductInventory.StockQuantity : 0),
                                  MinStockQuantity = product.MinStockQuantity,
                                  NotifyAdminForQuantityBelow = product.NotifyAdminForQuantityBelow,
                                  OrderMinimumQuantity = product.OrderMinimumQuantity,
                                  OrderMaximumQuantity = product.OrderMaximumQuantity,
                                  CurrencyType = product.CurrencyType,
                                  AvailableStartDateTimeUtc = product.AvailableStartDateTimeUtc,
                                  AvailableEndDateTimeUtc = product.AvailableEndDateTimeUtc,
                                  DisplayOrder = product.DisplayOrder,
                                  ApprovedRatingSum = product.ApprovedRatingSum,
                                  NotApprovedRatingSum = product.NotApprovedRatingSum,
                                  ApprovedTotalReviews = product.ApprovedTotalReviews,
                                  NotApprovedTotalReviews = product.NotApprovedTotalReviews,
                                  HasDiscountsApplied = product.HasDiscountsApplied,
                                  MarkAsNew = product.MarkAsNew,
                                  MarkAsNewStartDateTimeUtc = product.MarkAsNewStartDateTimeUtc,
                                  MarkAsNewEndDateTimeUtc = product.MarkAsNewEndDateTimeUtc,
                                  NotReturnable = product.NotReturnable,
                                  AllowedQuantities = product.AllowedQuantities,
                                  IsTaxExempt = product.IsTaxExempt,
                                  ShowOnHomepage = product.ShowOnHomepage,
                                  IsFreeShipping = product.IsFreeShipping,
                                  AllowCustomerReviews = product.AllowCustomerReviews,
                                  DisplayStockQuantity = product.DisplayStockQuantity,
                                  DisableBuyButton = product.DisableBuyButton,
                                  DisableWishlistButton = product.DisableWishlistButton,
                                  AvailableForPreOrder = product.AvailableForPreOrder,
                                  CallForPrice = product.CallForPrice,
                                  Published = product.Published,
                                  Deleted = product.Deleted,
                                  CreatedOnUtc = product.CreatedOnUtc,
                                  UpdatedOnUtc = product.UpdatedOnUtc,
                                  ImagePreviewId = product.ImagePreviewId,
                                  ImagePreview = new FileStorage.Core.Models.FileUploadModel(product.ImagePreview),
                                  CategoryIds = product.ProductCategories.Select(c => c.CategoryId).ToList(),
                                  CategoryNames = product.ProductCategories.Select(c => c.Category.Name).ToList(),
                                  ManufacturerIds = product.ProductManufacturers.Select(c => c.ManufacturerId).ToList(),
                                  ManufacturerNames = product.ProductManufacturers.Select(c => c.Manufacturer.Name).ToList(),
                                  AttributeIds = product.ProductAttributes.Select(c => c.AttributeId).ToList(),
                                  AttributeNames = product.ProductAttributes.Select(c => c.Attribute.DisplayName).ToList(),
                                  TagIds = product.ProductProductTags.Select(c => c.ProductTagId).ToList(),
                                  TagNames = product.ProductProductTags.Select(c => c.ProductTag.Name).ToList(),
                                  Variants = product.ProductVariants.Select(v => new ProductVariantModel()
                                  {
                                      Id = v.Id,
                                      SKU = v.SKU,
                                      ProductId = v.ProductId,
                                      SellPrice = v.SellPrice,
                                      OldSellPrice = v.OldSellPrice,
                                      ProductInventory = v.ProductInventory != null ? new ProductInventoryModel()
                                      {
                                          Id = v.ProductInventory.Id,
                                          VariantId = v.ProductInventory.VariantId,
                                          StockQuantity = v.ProductInventory.StockQuantity,
                                          ReservedQuantity = v.ProductInventory.ReservedQuantity
                                      } : null,
                                      ProductAttributes = v.VariantAttributes.Select(va => new ProductAttributeModel()
                                      {
                                          Id = va.Attribute.Id,
                                          Name = va.Attribute.DisplayName,
                                          Value = va.Attribute.Key,
                                          AttributeType = va.Attribute.AttributeType,
                                          DisplayOrder = va.Attribute.DisplayOrder,
                                          Description = va.Attribute.Description,
                                      }).ToList(),
                                  }).ToList(),

                              }).OrderByDescending(x => x.Id).AsNoTracking().Cacheable().ToPaginatedListAsync(dataGrid);

            result.Data = list;
            return result;
        }

        public async Task<Result<ProductModel>> GetById(int id)
        {
            var result = new Result<ProductModel>();
            var product = await _queryRepository.Table<Ecommerce.Core.Domain.Product>().Where(x => x.Deleted == false)
                .Include(x => x.CreateUser)
                .Include(x => x.UpdateUser)
                .Include(x => x.ProductCategories)
                .Include(x => x.ProductManufacturers)
                .Include(x => x.ProductImages)
                .Include(x => x.ProductAttributes)
                .Include(x => x.ProductProductTags).ThenInclude(x => x.ProductTag)
                .Include(x => x.ProductVariants).ThenInclude(v => v.ProductInventory)
                .Include(x => x.ProductVariants).ThenInclude(v => v.VariantAttributes).ThenInclude(va => va.Attribute)
                .Include(x => x.RelatedProduct2Navigation).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            var productModel = new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                SKU = product.SKU,
                CreateUserId = product.CreateUserId,
                UpdateUserId = product.UpdateUserId,
                MetaKeywords = product.MetaKeywords,
                MetaTitle = product.MetaTitle,
                ShortDescription = product.ShortDescription,
                FullDescription = product.FullDescription,
                AdminComment = product.AdminComment,
                MetaDescription = product.MetaDescription,
                DeliveryDateType = product.DeliveryDateType,
                TaxCategoryId = product.TaxCategoryId,
                NotifyAdminForQuantityBelow = product.NotifyAdminForQuantityBelow,
                OrderMinimumQuantity = product.OrderMinimumQuantity,
                OrderMaximumQuantity = product.OrderMaximumQuantity,
                CurrencyType = product.CurrencyType,
                AvailableStartDateTimeUtc = product.AvailableStartDateTimeUtc,
                AvailableEndDateTimeUtc = product.AvailableEndDateTimeUtc,
                DisplayOrder = product.DisplayOrder,
                ApprovedRatingSum = product.ApprovedRatingSum,
                NotApprovedRatingSum = product.NotApprovedRatingSum,
                ApprovedTotalReviews = product.ApprovedTotalReviews,
                NotApprovedTotalReviews = product.NotApprovedTotalReviews,
                HasDiscountsApplied = product.HasDiscountsApplied,
                MarkAsNew = product.MarkAsNew,
                MarkAsNewStartDateTimeUtc = product.MarkAsNewStartDateTimeUtc,
                MarkAsNewEndDateTimeUtc = product.MarkAsNewEndDateTimeUtc,
                NotReturnable = product.NotReturnable,
                AllowedQuantities = product.AllowedQuantities,
                IsTaxExempt = product.IsTaxExempt,
                ShowOnHomepage = product.ShowOnHomepage,
                IsFreeShipping = product.IsFreeShipping,
                AllowCustomerReviews = product.AllowCustomerReviews,
                DisableBuyButton = product.DisableBuyButton,
                DisableWishlistButton = product.DisableWishlistButton,
                AvailableForPreOrder = product.AvailableForPreOrder,
                CallForPrice = product.CallForPrice,
                Published = product.Published,
                Deleted = product.Deleted,
                CreatedOnUtc = product.CreatedOnUtc,
                UpdatedOnUtc = product.UpdatedOnUtc,
                ImagePreviewId = product.ImagePreviewId,
                MeasureType = product.MeasureType,
                ImagePreview = new FileStorage.Core.Models.FileUploadModel(product.ImagePreview),
                CreateUser = new AuthorModel()
                {
                    Id = product.CreateUser.Id,
                    Name = product.CreateUser.Name,
                    UserName = product.CreateUser.UserName,
                    Avatar = product.CreateUser.Avatar
                },
                UpdateUser = new AuthorModel()
                {
                    Id = product.UpdateUser?.Id,
                    Name = product.UpdateUser?.Name,
                    UserName = product.UpdateUser?.UserName,
                    Avatar = product.UpdateUser?.Avatar
                },
                CategoryIds = product.ProductCategories.Select(cat => cat.CategoryId).ToList(),
                ManufacturerIds = product.ProductManufacturers.Select(cat => cat.ManufacturerId).ToList(),
                Images = product.ProductImages.OrderBy(pi => pi.DisplayOrder).Select(pi => new ProductImageModel()
                {
                    ImageId = pi.ImageId,
                    DisplayOrder = pi.DisplayOrder
                }).ToList(),
                AttributeIds = product.ProductAttributes.Select(cat => cat.AttributeId).ToList(),
                RelatedProductIds = product.RelatedProduct2Navigation.Select(cat => cat.ProductId2).ToList(),
                TagIds = product.ProductProductTags.Select(c => c.ProductTagId).ToList(),

                StockQuantity = product.ProductVariants.Sum(v => v.ProductInventory != null ? v.ProductInventory.StockQuantity : 0),
                MinStockQuantity = product.MinStockQuantity,
                DisplayStockQuantity = product.DisplayStockQuantity,
                Variants = product.ProductVariants.Select(v => new ProductVariantModel()
                {
                    Id = v.Id,
                    SKU = v.SKU,
                    ProductId = v.ProductId,
                    SellPrice = v.SellPrice,
                    OldSellPrice = v.OldSellPrice,
                    ProductInventory = new ProductInventoryModel()
                    {
                        Id = v.ProductInventory.Id,
                        VariantId = v.ProductInventory.VariantId,
                        StockQuantity = v.ProductInventory.StockQuantity,
                        ReservedQuantity = v.ProductInventory.ReservedQuantity
                    },
                    ProductAttributes = v.VariantAttributes != null ? v.VariantAttributes.Select(va => new ProductAttributeModel()
                    {
                        Id = va.Attribute.Id,
                        Name = va.Attribute.DisplayName,
                        Value = va.Attribute.Key,
                        AttributeType = va.Attribute.AttributeType,
                        DisplayOrder = va.Attribute.DisplayOrder,
                        Description = va.Attribute.Description,
                    }).ToList() : []
                }).ToList(),
            };
            result.Data = productModel;
            return result;
        }

        public async Task<Result<List<ProductModel>>> GetByIds(int[] ids)
        {
            var result = new Result<List<ProductModel>>();
            if (ids.Length == 0)
                return result;

            var products = await _queryRepository.Table<Ecommerce.Core.Domain.Product>().Where(x => ids.Contains(x.Id)).Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Id + " | " + x.Name,
                ImagePreviewId = x.ProductImages.OrderBy(v => v.DisplayOrder).FirstOrDefault().ImageId,
            }).AsNoTracking().ToListAsync();
            result.Data = products;
            return result;
        }

        public async Task<Result<List<ProductModel>>> GetProductsByInput(string input)
        {
            var result = new Result<List<ProductModel>>();
            var quary = _queryRepository.Table<Ecommerce.Core.Domain.Product>().Where(x => x.Published == true && x.Deleted == false);

            var id = 0;
            int.TryParse(input, out id);

            quary = quary.Where(x => x.Name.Contains(input) || x.MetaKeywords.Contains(input) || x.MetaTitle.Contains(input) || (id > 0 && x.Id == id));

            var list = await quary.Take(10).Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Id + " | " + x.Name,
            }).AsNoTracking().ToListAsync();

            result.Data = list;
            return result;
        }

        public async Task<Result<ProductModel>> Add(ProductModel productModel)
        {
            var result = new Result<ProductModel>();

            var validationErrors = ValidateProductModel(productModel);
            if (validationErrors.Count > 0)
            {
                result.Status = ResultStatusEnum.InvalidValidation;
                result.Message = "Validation failed.";
                result.Errors.AddRange(validationErrors);
                return result;
            }
            if (productModel.Published)
            {
                var isNameExist = _queryRepository.Table<Ecommerce.Core.Domain.Product>().Any(x => x.Name == productModel.Name);
                if (isNameExist)
                {
                    result.Message = "Validation failed.";
                    result.Errors.Add(new Error(nameof(productModel.Name), "Product name is Existed."));
                    result.Status = ResultStatusEnum.InvalidValidation;
                    return result;
                }

                var isSkuExist = _queryRepository.Table<Ecommerce.Core.Domain.Product>().Any(x => x.SKU == productModel.SKU);
                if (isSkuExist)
                {
                    result.Message = "Validation failed.";
                    result.Errors.Add(new Error(nameof(productModel.SKU), "Product Sku is Existed."));
                    result.Status = ResultStatusEnum.InvalidValidation;
                    return result;
                }
            }

            var currentDateTime = DateTime.UtcNow;

            var product = new Ecommerce.Core.Domain.Product
            {
                Name = productModel.Name,
                SKU = productModel.SKU,
                CreateUserId = productModel.CreateUserId,
                MetaKeywords = productModel.MetaKeywords,
                MetaTitle = productModel.MetaTitle,
                ShortDescription = productModel.ShortDescription,
                FullDescription = productModel.FullDescription,
                AdminComment = productModel.AdminComment,
                MetaDescription = productModel.MetaDescription,
                DeliveryDateType = productModel.DeliveryDateType,
                TaxCategoryId = productModel.TaxCategoryId,
                MinStockQuantity = productModel.MinStockQuantity,
                DisplayStockQuantity = productModel.DisplayStockQuantity,
                NotifyAdminForQuantityBelow = productModel.NotifyAdminForQuantityBelow,
                OrderMinimumQuantity = productModel.OrderMinimumQuantity,
                OrderMaximumQuantity = productModel.OrderMaximumQuantity,
                CurrencyType = productModel.CurrencyType,
                ImagePreviewId = productModel.ImagePreviewId,
                AvailableStartDateTimeUtc = productModel.AvailableStartDateTimeUtc != null ? productModel.AvailableStartDateTimeUtc.Value : currentDateTime,
                AvailableEndDateTimeUtc = productModel.AvailableEndDateTimeUtc,
                DisplayOrder = productModel.DisplayOrder,
                ApprovedRatingSum = productModel.ApprovedRatingSum,
                NotApprovedRatingSum = productModel.NotApprovedRatingSum,
                ApprovedTotalReviews = productModel.ApprovedTotalReviews,
                NotApprovedTotalReviews = productModel.NotApprovedTotalReviews,
                HasDiscountsApplied = productModel.HasDiscountsApplied,
                MarkAsNew = productModel.MarkAsNew,
                MarkAsNewStartDateTimeUtc = productModel.MarkAsNewStartDateTimeUtc,
                MarkAsNewEndDateTimeUtc = productModel.MarkAsNewEndDateTimeUtc,
                NotReturnable = productModel.NotReturnable,
                AllowedQuantities = productModel.AllowedQuantities,
                IsTaxExempt = productModel.IsTaxExempt,
                ShowOnHomepage = productModel.ShowOnHomepage,
                IsFreeShipping = productModel.IsFreeShipping,
                AllowCustomerReviews = productModel.AllowCustomerReviews,
                DisableBuyButton = productModel.DisableBuyButton,
                DisableWishlistButton = productModel.DisableWishlistButton,
                AvailableForPreOrder = productModel.AvailableForPreOrder,
                CallForPrice = productModel.CallForPrice,
                Published = productModel.Published,
                Deleted = false,
                CreatedOnUtc = currentDateTime,
                UpdatedOnUtc = null
            };

            await using var transaction = await _commandRepository.BeginTransactionAsync();
            try
            {
                await _commandRepository.InsertAsync(product);
                await _commandRepository.SaveChangesAsync();

                var pid = product.Id;

                var categories = productModel.CategoryIds
                    .Select((catId, idx) => new ProductCategory { ProductId = pid, CategoryId = catId, DisplayOrder = idx })
                    .ToList();

                var manufacturers = productModel.ManufacturerIds
                    .Select((mId, idx) => new ProductManufacturer { ProductId = pid, ManufacturerId = mId, DisplayOrder = idx })
                    .ToList();

                var images = productModel.Images
                    .Select((img, idx) => new ProductImage { ProductId = pid, ImageId = img.ImageId, DisplayOrder = img.DisplayOrder != 0 ? img.DisplayOrder : idx })
                    .ToList();

                var attributes = productModel.AttributeIds
                    .Select(attrId => new ProductProductAttribute { ProductId = pid, AttributeId = attrId })
                    .ToList();

                var related = productModel.RelatedProductIds
                    .Select((rid, idx) => new RelatedProduct { ProductId1 = pid, ProductId2 = rid, DisplayOrder = idx })
                    .ToList();

                var tags = productModel.TagIds
                    .Select(tagId => new ProductProductTag { ProductId = pid, ProductTagId = tagId })
                    .ToList();

                if (images.Any()) await _commandRepository.InsertRangeAsync(images);
                if (attributes.Any()) await _commandRepository.InsertRangeAsync(attributes);
                if (related.Any()) await _commandRepository.InsertRangeAsync(related);
                if (tags.Any()) await _commandRepository.InsertRangeAsync(tags);

                await _commandRepository.InsertRangeAsync(categories);
                await _commandRepository.InsertRangeAsync(manufacturers);

                // Create variants with their inventory and attributes
                foreach (var variantModel in productModel.Variants)
                {
                    var variant = new ProductVariant
                    {
                        SKU = variantModel.SKU,
                        ProductId = pid,
                        SellPrice = variantModel.SellPrice,
                        OldSellPrice = variantModel.OldSellPrice
                    };
                    await _commandRepository.InsertAsync(variant);
                    await _commandRepository.SaveChangesAsync();

                    if (variantModel.ProductInventory != null)
                    {
                        await _commandRepository.InsertAsync(new ProductInventory
                        {
                            VariantId = variant.Id,
                            StockQuantity = variantModel.ProductInventory.StockQuantity,
                            ReservedQuantity = variantModel.ProductInventory.ReservedQuantity,
                            CreatedDatetime = currentDateTime
                        });
                    }

                    foreach (var attrModel in variantModel.ProductAttributes)
                    {
                        await _commandRepository.InsertAsync(new ProductVariantAttribute
                        {
                            VariantId = variant.Id,
                            AttributeId = attrModel.Id
                        });
                    }
                }

                await _commandRepository.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }

            result.Data = productModel;
            return result;
        }

        public async Task<Result<ProductModel>> Update(ProductModel productModel)
        {
            var result = new Result<ProductModel>();
            try
            {
                var validationErrors = ValidateProductModel(productModel);
                if (validationErrors.Count > 0)
                {
                    result.Status = ResultStatusEnum.InvalidValidation;
                    result.Message = "Validation failed.";
                    result.Errors.AddRange(validationErrors);
                    return result;
                }

                if (productModel.Published)
                {
                    var isNameExist = _queryRepository.Table<Ecommerce.Core.Domain.Product>()
                        .Any(x => x.Id != productModel.Id && x.Name == productModel.Name);
                    if (isNameExist)
                    {
                        result.Message = "Validation failed.";
                        result.Errors.Add(new Error(nameof(productModel.Name), "Product name is Existed."));
                        result.Status = ResultStatusEnum.InvalidValidation;
                        return result;
                    }

                    var isSkuExist = _queryRepository.Table<Ecommerce.Core.Domain.Product>()
                        .Any(x => x.Id != productModel.Id && x.SKU == productModel.SKU);
                    if (isSkuExist)
                    {
                        result.Message = "Validation failed.";
                        result.Errors.Add(new Error(nameof(productModel.SKU), "Product Sku is Existed."));
                        result.Status = ResultStatusEnum.InvalidValidation;
                        return result;
                    }
                }

                var transaction = await _commandRepository.BeginTransactionAsync();
                try
                {
                    var product = await _queryRepository.Table<Ecommerce.Core.Domain.Product>().FirstOrDefaultAsync(x => x.Id == productModel.Id);
                    if (product is null)
                    {
                        result.Status = ResultStatusEnum.NotFound;
                        result.Message = "The Product not found";
                        return result;
                    }

                    var currentDateTime = DateTime.UtcNow;

                    product.UpdateUserId = productModel.UpdateUserId;
                    product.UpdatedOnUtc = currentDateTime;

                    product.Name = productModel.Name;
                    product.SKU = productModel.SKU;
                    product.ShortDescription = productModel.ShortDescription;
                    product.FullDescription = productModel.FullDescription;
                    product.ImagePreviewId = productModel.ImagePreviewId;

                    product.MetaKeywords = productModel.MetaKeywords;
                    product.MetaTitle = productModel.MetaTitle;
                    product.MetaDescription = productModel.MetaDescription;

                    product.AdminComment = productModel.AdminComment;
                    product.NotifyAdminForQuantityBelow = productModel.NotifyAdminForQuantityBelow;
                    product.OrderMinimumQuantity = productModel.OrderMinimumQuantity;
                    product.OrderMaximumQuantity = productModel.OrderMaximumQuantity;
                    product.AvailableEndDateTimeUtc = productModel.AvailableEndDateTimeUtc;
                    product.DisplayOrder = productModel.DisplayOrder;
                    product.ApprovedRatingSum = productModel.ApprovedRatingSum;
                    product.NotApprovedRatingSum = productModel.NotApprovedRatingSum;
                    product.ApprovedTotalReviews = productModel.ApprovedTotalReviews;
                    product.NotApprovedTotalReviews = productModel.NotApprovedTotalReviews;
                    product.HasDiscountsApplied = productModel.HasDiscountsApplied;
                    product.MarkAsNew = productModel.MarkAsNew;
                    product.MarkAsNewStartDateTimeUtc = productModel.MarkAsNewStartDateTimeUtc;
                    product.MarkAsNewEndDateTimeUtc = productModel.MarkAsNewEndDateTimeUtc;
                    product.NotReturnable = productModel.NotReturnable;
                    product.AllowedQuantities = productModel.AllowedQuantities;
                    product.IsTaxExempt = productModel.IsTaxExempt;
                    product.ShowOnHomepage = productModel.ShowOnHomepage;
                    product.IsFreeShipping = productModel.IsFreeShipping;
                    product.AllowCustomerReviews = productModel.AllowCustomerReviews;
                    product.DisableBuyButton = productModel.DisableBuyButton;
                    product.DisableWishlistButton = productModel.DisableWishlistButton;
                    product.AvailableForPreOrder = productModel.AvailableForPreOrder;
                    product.CallForPrice = productModel.CallForPrice;
                    product.Published = productModel.Published;

                    product.DeliveryDateType = productModel.DeliveryDateType;
                    product.TaxCategoryId = productModel.TaxCategoryId;
                    product.MeasureType = productModel.MeasureType;

                    product.CurrencyType = productModel.CurrencyType;

                    product.DisplayStockQuantity = productModel.DisplayStockQuantity;
                    product.MinStockQuantity = productModel.MinStockQuantity;

                    if (product.AvailableStartDateTimeUtc != productModel.AvailableStartDateTimeUtc)
                        product.AvailableStartDateTimeUtc = productModel.AvailableStartDateTimeUtc != null ?
                            productModel.AvailableStartDateTimeUtc.Value : currentDateTime;

                    _commandRepository.Update(product);

                    await UpdateProductCategory(product.Id, productModel.CategoryIds.ToArray());
                    await UpdateProductAttribute(product.Id, productModel.AttributeIds.ToArray());
                    await UpdateProductManufacturer(product.Id, productModel.ManufacturerIds.ToArray());
                    await UpdateProductPicture(product.Id, productModel.Images.ToArray());
                    await UpdateProductTags(product.Id, productModel.TagIds.ToArray());
                    await UpdateRelatedProducts(product.Id, productModel.RelatedProductIds.ToArray());
                    await UpdateProductVariants(product.Id, productModel.Variants);

                    _commandRepository.SaveChanges();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }

                result.Data = productModel;
                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        private async Task<Result> UpdateProductCategory(int productId, int[] newCategories)
        {
            var result = new Result();
            try
            {
                var productCategories = _queryRepository.Table<ProductCategory>().Where(x => x.ProductId == productId).ToList();
                var currentCategories = productCategories.Select(x => x.CategoryId).ToArray();

                if (!newCategories.SequenceEqual(currentCategories))
                {
                    foreach (var cat in productCategories)
                    {
                        _commandRepository.Delete(cat);
                        _commandRepository.SaveChanges();
                    }

                    foreach (var id in newCategories)
                        await _commandRepository.InsertAsync(new ProductCategory() { ProductId = productId, CategoryId = id });
                }
                return result;
            }
            catch (Exception e)
            {
                result.Status = ResultStatusEnum.ExceptionThrowed;
                result.Message = e.Message;
                return result;
            }
        }

        private async Task<Result> UpdateProductManufacturer(int productId, int[] newManufacturers)
        {
            var result = new Result();
            try
            {
                var productManufacturers = _queryRepository.Table<ProductManufacturer>().Where(x => x.ProductId == productId).ToList();
                var currentManufacturers = productManufacturers.Select(x => x.ManufacturerId).ToArray();

                if (!newManufacturers.SequenceEqual(currentManufacturers))
                {
                    foreach (var cat in productManufacturers)
                        _commandRepository.Delete(cat);
                    _commandRepository.SaveChanges();

                    foreach (var id in newManufacturers)
                        await _commandRepository.InsertAsync(new ProductManufacturer() { ProductId = productId, ManufacturerId = id });
                }
                return result;
            }
            catch (Exception e)
            {
                result.Status = ResultStatusEnum.ExceptionThrowed;
                result.Message = e.Message;
                return result;
            }
        }

        private async Task<Result> UpdateProductTags(int productId, int[] newTags)
        {
            var result = new Result();
            try
            {
                var productProductTags = _queryRepository.Table<ProductProductTag>().Where(x => x.ProductId == productId).ToList();
                var currentProductTags = productProductTags.Select(x => x.ProductTagId).ToArray();

                if (!newTags.SequenceEqual(currentProductTags))
                {
                    foreach (var cat in productProductTags)
                        _commandRepository.Delete(cat);
                    _commandRepository.SaveChanges();
                    foreach (var id in newTags)
                        await _commandRepository.InsertAsync(new ProductProductTag() { ProductId = productId, ProductTagId = id });
                }
                return result;
            }
            catch (Exception e)
            {
                result.Status = ResultStatusEnum.ExceptionThrowed;
                result.Message = e.Message;
                return result;
            }
        }

        private async Task<Result> UpdateProductAttribute(int productId, int[] newAttributes)
        {
            var result = new Result();
            try
            {
                var productAttributes = _queryRepository.Table<ProductProductAttribute>().Where(x => x.ProductId == productId).ToList();
                var currentAttributes = productAttributes.Select(x => x.AttributeId).ToArray();

                if (!newAttributes.SequenceEqual(currentAttributes))
                {
                    foreach (var cat in productAttributes)
                        _commandRepository.Delete(cat);
                    _commandRepository.SaveChanges();
                    foreach (var id in newAttributes)
                        await _commandRepository.InsertAsync(new ProductProductAttribute() { ProductId = productId, AttributeId = id });
                }
                return result;
            }
            catch (Exception e)
            {
                result.Status = ResultStatusEnum.ExceptionThrowed;
                result.Message = e.Message;
                return result;
            }
        }

        private async Task<Result> UpdateProductPicture(int productId, ProductImageModel[] newPictures)
        {
            var result = new Result();
            try
            {
                var productPictures = _queryRepository.Table<ProductImage>().Where(x => x.ProductId == productId).ToList();
                var currentOrderedIds = productPictures.OrderBy(x => x.DisplayOrder).Select(x => x.ImageId).ToArray();
                var newOrderedIds = newPictures.OrderBy(x => x.DisplayOrder).Select(x => x.ImageId).ToArray();

                if (!currentOrderedIds.SequenceEqual(newOrderedIds))
                {
                    foreach (var pic in productPictures)
                        _commandRepository.Delete(pic);
                    _commandRepository.SaveChanges();

                    var ordered = newPictures.OrderBy(x => x.DisplayOrder).ToList();
                    for (int i = 0; i < ordered.Count; i++)
                    {
                        var np = ordered[i];
                        await _commandRepository.InsertAsync(new ProductImage()
                        {
                            ProductId = productId,
                            ImageId = np.ImageId,
                            DisplayOrder = np.DisplayOrder != 0 ? np.DisplayOrder : i
                        });
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                result.Status = ResultStatusEnum.ExceptionThrowed;
                result.Message = e.Message;
                return result;
            }
        }

        private async Task<Result> UpdateProductVariants(int productId, List<ProductVariantModel> newVariants)
        {
            var result = new Result();
            try
            {
                var existingVariants = await _queryRepository.Table<ProductVariant>()
                    .Include(v => v.ProductInventory)
                    .Include(v => v.VariantAttributes)
                    .Where(v => v.ProductId == productId).ToListAsync();

                var existingById = existingVariants.ToDictionary(v => v.Id);

                // Delete variants not in the incoming list
                foreach (var variant in existingVariants.Where(v => !newVariants.Any(nv => nv.Id == v.Id)))
                    _commandRepository.Delete(variant);

                _commandRepository.SaveChanges();

                var datetime = DateTime.UtcNow;

                foreach (var variantModel in newVariants)
                {
                    if (variantModel.Id > 0 && existingById.ContainsKey(variantModel.Id))
                    {
                        // Update existing variant
                        var existing = existingById[variantModel.Id];

                        if (existing.SKU != variantModel.SKU ||
                            existing.SellPrice != variantModel.SellPrice ||
                            existing.OldSellPrice != variantModel.OldSellPrice)
                        {
                            existing.SKU = variantModel.SKU.ToUpper();
                            existing.SellPrice = variantModel.SellPrice;
                            existing.OldSellPrice = variantModel.OldSellPrice;
                            _commandRepository.Update(existing);
                        }

                        // Update inventory
                        if (variantModel.ProductInventory != null)
                        {
                            if (existing.ProductInventory != null)
                            {
                                if (existing.ProductInventory.StockQuantity != variantModel.ProductInventory.StockQuantity ||
                                    existing.ProductInventory.ReservedQuantity != variantModel.ProductInventory.ReservedQuantity)
                                {
                                    existing.ProductInventory.StockQuantity = variantModel.ProductInventory.StockQuantity;
                                    existing.ProductInventory.ReservedQuantity = variantModel.ProductInventory.ReservedQuantity;
                                    _commandRepository.Update(existing.ProductInventory);
                                }
                            }
                            else
                            {
                                await _commandRepository.InsertAsync(new ProductInventory
                                {
                                    VariantId = existing.Id,
                                    StockQuantity = variantModel.ProductInventory.StockQuantity,
                                    ReservedQuantity = variantModel.ProductInventory.ReservedQuantity,
                                    CreatedDatetime = datetime
                                });
                            }
                        }
                        else if (existing.ProductInventory != null)
                        {
                            _commandRepository.Delete(existing.ProductInventory);
                            _commandRepository.SaveChanges();
                        }

                        // Update variant attributes
                        var existingAttrIds = existing.VariantAttributes.Select(va => va.AttributeId).ToArray();
                        var newAttrIds = variantModel.ProductAttributes.Select(a => a.Id).ToArray();

                        if (!existingAttrIds.SequenceEqual(newAttrIds))
                        {
                            foreach (var va in existing.VariantAttributes)
                                _commandRepository.Delete(va);

                            _commandRepository.SaveChanges();

                            foreach (var attrId in newAttrIds)
                            {
                                await _commandRepository.InsertAsync(new ProductVariantAttribute
                                {
                                    VariantId = existing.Id,
                                    AttributeId = attrId
                                });
                            }
                        }
                    }
                    else
                    {
                        // Insert new variant
                        var newVariant = new ProductVariant
                        {
                            SKU = variantModel.SKU,
                            ProductId = productId,
                            SellPrice = variantModel.SellPrice,
                            OldSellPrice = variantModel.OldSellPrice
                        };
                        await _commandRepository.InsertAsync(newVariant);
                        await _commandRepository.SaveChangesAsync();

                        if (variantModel.ProductInventory != null)
                        {
                            await _commandRepository.InsertAsync(new ProductInventory
                            {
                                VariantId = newVariant.Id,
                                StockQuantity = variantModel.ProductInventory.StockQuantity,
                                ReservedQuantity = variantModel.ProductInventory.ReservedQuantity,
                                CreatedDatetime = datetime
                            });
                        }

                        foreach (var attrModel in variantModel.ProductAttributes)
                        {
                            await _commandRepository.InsertAsync(new ProductVariantAttribute
                            {
                                VariantId = newVariant.Id,
                                AttributeId = attrModel.Id
                            });
                        }
                    }
                }


                return result;
            }
            catch (Exception e)
            {
                result.Status = ResultStatusEnum.ExceptionThrowed;
                result.Message = e.Message;
                return result;
            }
        }

        private async Task<Result> UpdateRelatedProducts(int productId, int[] newRelateds)
        {
            var result = new Result();
            try
            {
                var productRelateds = _queryRepository.Table<RelatedProduct>().Where(x => x.ProductId1 == productId).ToList();
                var currentRelateds = productRelateds.Select(x => x.ProductId2).ToArray();

                if (!newRelateds.SequenceEqual(currentRelateds))
                {
                    foreach (var cat in productRelateds)
                        _commandRepository.Delete(cat);

                    _commandRepository.SaveChanges();

                    foreach (var id in newRelateds)
                        await _commandRepository.InsertAsync(new RelatedProduct() { ProductId1 = productId, ProductId2 = id });
                }
                return result;
            }
            catch (Exception e)
            {
                result.Status = ResultStatusEnum.ExceptionThrowed;
                result.Message = e.Message;
                return result;
            }
        }

        private List<Error> ValidateProductModel(ProductModel productModel)
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(productModel.Name))
                errors.Add(new Error(nameof(productModel.Name), "Product name is required."));

            if (string.IsNullOrWhiteSpace(productModel.SKU))
                errors.Add(new Error(nameof(productModel.SKU), "Product Sku is required."));

            if (!productModel.ManufacturerIds.Any())
                errors.Add(new Error(nameof(productModel.ManufacturerIds), "Manufacturer is required."));

            if (!productModel.CategoryIds.Any())
                errors.Add(new Error(nameof(productModel.CategoryIds), "Category is required."));

            if (productModel.MeasureType == null)
                errors.Add(new Error(nameof(productModel.MeasureType), "MeasureType is required."));

            if (productModel.CurrencyType == null)
                errors.Add(new Error(nameof(productModel.CurrencyType), "CurrencyType is required."));

            if (!productModel.Variants.Any())
                errors.Add(new Error(nameof(productModel.Variants), "At least one variant is required."));

            var seenVariantAttributeSets = new HashSet<string>();

            for (int i = 0; i < productModel.Variants.Count; i++)
            {
                var variant = productModel.Variants[i];
                if (string.IsNullOrWhiteSpace(variant.SKU))
                    errors.Add(new Error(nameof(productModel.Variants), "Variant SKU is required."));
                if (variant.SellPrice < 0)
                    errors.Add(new Error(nameof(productModel.Variants), "SellPrice cannot be negative."));
                if (variant.OldSellPrice < 0)
                    errors.Add(new Error(nameof(productModel.Variants), "OldSellPrice cannot be negative."));
                if (variant.ProductInventory == null)
                    errors.Add(new Error(nameof(productModel.Variants), "Variant inventory is required."));
                else if (variant.ProductInventory.StockQuantity < 0)
                    errors.Add(new Error(nameof(productModel.Variants), "StockQuantity cannot be negative."));

                // Check for duplicate attribute types within the same variant (e.g. two Sizes or two Colors)
                var attributeTypes = variant.ProductAttributes
                    .GroupBy(a => a.AttributeType)
                    .Where(g => g.Count() > 1);
                foreach (var group in attributeTypes)
                {
                    errors.Add(new Error(nameof(productModel.Variants), $"Variant cannot have duplicate attribute type '{group.Key}'."));
                }

                // Check for duplicate variants (same set of attribute IDs)
                var sortedAttrIds = variant.ProductAttributes.Select(a => a.Id).OrderBy(x => x);
                var key = string.Join(",", sortedAttrIds);
                if (!seenVariantAttributeSets.Add(key))
                {
                    errors.Add(new Error(nameof(productModel.Variants), "Duplicate variant detected. Two variants cannot have the same attribute combination."));
                }
            }

            return errors;
        }

        public async Task<Result> Delete(int id)
        {
            var result = new Result();
            var product = await _queryRepository.Table<Ecommerce.Core.Domain.Product>().FirstOrDefaultAsync(x => x.Id == id);
            if (product is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The Product not found";
                return result;
            }
            product.Deleted = true;
            _commandRepository.Update(product);
            await _commandRepository.SaveChangesAsync();
            return result;
        }

        public async Task<Result> Remove(int id)
        {
            var result = new Result();
            var product = await _queryRepository.Table<Ecommerce.Core.Domain.Product>().FirstOrDefaultAsync(x => x.Id == id);
            if (product is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The Product not found";
                return result;
            }
            _commandRepository.Delete(product);
            await _commandRepository.SaveChangesAsync();
            return result;
        }
    }
}

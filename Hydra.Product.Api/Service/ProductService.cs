using EFCoreSecondLevelCacheInterceptor;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Hydra.Ecommerce.Core.Domain;
using Hydra.Product.Core.Interfaces;
using Hydra.Product.Core.Models;
using Microsoft.EntityFrameworkCore;
using Hydra.Kernel.Extension;

namespace Hydra.Product.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;
        private readonly IProductTagService _productTagService;
        public ProductService(IQueryRepository queryRepository, ICommandRepository commandRepository, IProductTagService productTagService)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _productTagService = productTagService;
        }

        /// <summary>
        /// Retrieves a paginated list of published products that match the specified filter criteria.
        /// This is used by customer-facing endpoints and only returns products that are published and not deleted.
        /// </summary>
        /// <remarks>The returned list is paginated according to the page index and page size specified in
        /// the filter. Filtering supports searching by product name, metadata, descriptions, tags, price range, stock
        /// status, categories, and manufacturers. Only products that are published and not deleted are included in the
        /// results.</remarks>
        /// <param name="productFilter">An object containing filtering and pagination options to apply when retrieving products. Cannot be null.
        /// Filtering options may include search terms, price range, stock availability, category, manufacturer, and
        /// product tags.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a Result object wrapping a
        /// PaginatedList of ProductModel instances that match the filter criteria. The list will be empty if no
        /// products are found.</returns>
        public async Task<Result<PaginatedList<ProductDisplayModel>>> GetPublishedProducts(ProductFilterDisplayModel productFilter)
        {
            var result = new Result<PaginatedList<ProductDisplayModel>>();
            var currentDateTime = DateTime.UtcNow;
            var query = _queryRepository.Table<Ecommerce.Core.Domain.Product>()
                            .Include(x => x.ProductInventories)
                            .ThenInclude(x => x.ProductAttribute)
                            .Include(x => x.ProductCategories)
                            .Include(x => x.ProductManufacturers)
                            .Include(x => x.ProductAttributes)
                            .Where(x => !x.Deleted && x.Published &&
                            (x.AvailableStartDateTimeUtc != null && x.AvailableStartDateTimeUtc >= currentDateTime) &&
                            (x.AvailableEndDateTimeUtc != null && x.AvailableEndDateTimeUtc <= currentDateTime));

            // Apply search filter
            if (!string.IsNullOrEmpty(productFilter.SearchInput))
            {
                query = query.Where(x => x.Name.Contains(productFilter.SearchInput) ||
                                         x.MetaKeywords.Contains(productFilter.SearchInput) ||
                                         x.MetaTitle.Contains(productFilter.SearchInput) ||
                                         x.MetaDescription.Contains(productFilter.SearchInput) ||
                                         x.FullDescription.Contains(productFilter.SearchInput) ||
                                         x.ShortDescription.Contains(productFilter.SearchInput));

                // Apply search input on tags
                query = query.Where(x => x.ProductProductTags.Any(m => m.ProductTag.Name.Contains(productFilter.SearchInput)));

            }

            // Apply price range filter
            if (productFilter.FromSellUnitPrice.HasValue)
            {
                query = query.Where(x => x.SellUnitPrice >= productFilter.FromSellUnitPrice.Value);
            }

            if (productFilter.ToSellUnitPrice.HasValue)
            {
                query = query.Where(x => x.SellUnitPrice <= productFilter.ToSellUnitPrice.Value);
            }

            // Apply stock availability filter
            if (productFilter.HasStockQuantity.HasValue && productFilter.HasStockQuantity.Value)
            {
                query = query.Where(x => x.StockQuantity > 0);
            }

            // Apply category filter
            if (productFilter.CategoryIds.Count > 0)
            {
                query = query.Where(x => x.ProductCategories.Any(c => productFilter.CategoryIds.Contains(c.CategoryId)));
            }

            // Apply manufacturer filter
            if (productFilter.ManufacturerIds.Count > 0)
            {
                query = query.Where(x => x.ProductManufacturers.Any(m => productFilter.ManufacturerIds.Contains(m.ManufacturerId)));
            }


            // Apply Attribute Types filter
            if (productFilter.AttributeTypes.Count > 0)
            {
                query = query.Where(x => x.ProductAttributes.Any(c => productFilter.AttributeTypes.Contains(c.Attribute.AttributeType)));
            }

            // Apply TopRate filter
            if (productFilter.TopRate.HasValue)
            {
                query = query.OrderByDescending(c => c.ApprovedRatingSum);
            }

            // Apply manufacturer filter
            if (productFilter.TopSell.HasValue)
            {
                query = query.Include(x => x.OrderItems).OrderByDescending(c => c.OrderItems.Count());
            }

            var list = (from product in query
                        select new ProductDisplayModel()
                        {
                            Id = product.Id,
                            CreateUserId = product.CreateUserId,
                            UpdateUserId = product.UpdateUserId,
                            Name = product.Name,
                            MetaKeywords = product.MetaKeywords,
                            MetaTitle = product.MetaTitle,
                            FullDescription = product.FullDescription,
                            AdminComment = product.AdminComment,
                            MetaDescription = product.MetaDescription,
                            DeliveryDateType = product.DeliveryDateType,
                            TaxCategoryId = product.TaxCategoryId,
                            TaxCategoryName = product.TaxCategory.Name,
                            StockQuantity = product.StockQuantity,
                            MinStockQuantity = product.MinStockQuantity,
                            OrderMinimumQuantity = product.OrderMinimumQuantity,
                            OrderMaximumQuantity = product.OrderMaximumQuantity,
                            SellUnitPrice = product.SellUnitPrice,
                            OldSellUnitPrice = product.OldSellUnitPrice,
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
                            StockType = product.StockType,
                            ImagePreviewPath = product.ImagePreview.Directory + product.ImagePreview.FileName,
                            CategoryNames = product.ProductCategories.Select(c => c.Category.Name).ToList(),
                            ManufacturerNames = product.ProductManufacturers.Select(c => c.Manufacturer.Name).ToList(),
                            Attributes = product.ProductAttributes.Select(c => c.Attribute).Select(z => new ProductAttributeDisplayModel()
                            {
                                Id = z.Id,
                                AttributeType = z.AttributeType,
                                Description = z.Description,
                                DisplayOrder = z.DisplayOrder,
                                Name = z.Name,
                                ImagePreviewPath = z.ImagePreview.Directory + z.ImagePreview.FileName,
                                Value = z.Value,
                            }).ToList(),
                            ProductTags = product.ProductProductTags.Select(x => x.ProductTag).Select(cat => cat.Name).ToList(),
                            Inventories = product.ProductInventories.Select(x => new ProductInventoryDisplayModel()
                            {
                                Id = x.Id,
                                ProductId = x.ProductId,
                                AttributeId = x.AttributeId,
                                AttributeName = x.ProductAttribute.Name,
                                StockQuantity = x.StockQuantity,
                                ReservedQuantity = x.ReservedQuantity
                            }).ToList(),
                        });

            if (productFilter.Sorting != null)
            {
                var orders = new string[1] { productFilter.Sorting.Order };

                list = list.AddOrderBy(new Sort[] { productFilter.Sorting });
            }

            result.Data = await list.Cacheable().ToPaginatedListAsync(productFilter.PageIndex, productFilter.PageSize);

            return result;
        }


        /// <summary>
        /// Retrieves curated product groups for published products. Groups are built from featured Style attributes.
        /// </summary>
        public async Task<Result<List<CuratedProductGroupModel>>> GetPublishedCuratedProducts()
        {
            var result = new Result<List<CuratedProductGroupModel>>();
            var currentDateTime = DateTime.UtcNow;

            var products = await _queryRepository.Table<Ecommerce.Core.Domain.Product>()
                .Include(x => x.ProductInventories).ThenInclude(x => x.ProductAttribute)
                .Include(x => x.ProductCategories)
                .Include(x => x.ProductManufacturers)
                .Include(x => x.ProductAttributes).ThenInclude(x => x.Attribute)
                .Include(x => x.ProductProductTags).ThenInclude(x => x.ProductTag)
                .Where(x => !x.Deleted && x.Published &&
                    (x.AvailableStartDateTimeUtc != null && x.AvailableStartDateTimeUtc >= currentDateTime) &&
                    (x.AvailableEndDateTimeUtc != null && x.AvailableEndDateTimeUtc <= currentDateTime) &&
                    x.ProductAttributes.Any(a =>
                        a.Attribute.AttributeType == AttributeType.Style && a.Attribute.IsFeatured))
                .ToListAsync();

            var groups = products
                .SelectMany(p => p.ProductAttributes
                    .Where(a => a.Attribute.AttributeType == AttributeType.Style && a.Attribute.IsFeatured)
                    .Select(a => new { a.Attribute.Id, a.Attribute, Product = p }))
                .GroupBy(x => x.Id)
                .Select(g => new CuratedProductGroupModel
                {
                    AttributeId = g.Key,
                    AttributeName = g.First().Attribute.Name,
                    AttributeValue = g.First().Attribute.Value,
                    AttributeDescription = g.First().Attribute.Description,
                    ImagePreviewPath = g.First().Attribute.ImagePreview.Directory + g.First().Attribute.ImagePreview.FileName,
                    Products = g.Select(x => new ProductDisplayModel
                    {
                        Id = x.Product.Id,
                        CreateUserId = x.Product.CreateUserId,
                        UpdateUserId = x.Product.UpdateUserId,
                        Name = x.Product.Name,
                        MetaKeywords = x.Product.MetaKeywords,
                        MetaTitle = x.Product.MetaTitle,
                        FullDescription = x.Product.FullDescription,
                        AdminComment = x.Product.AdminComment,
                        MetaDescription = x.Product.MetaDescription,
                        DeliveryDateType = x.Product.DeliveryDateType,
                        TaxCategoryId = x.Product.TaxCategoryId,
                        TaxCategoryName = x.Product.TaxCategory?.Name,
                        StockQuantity = x.Product.StockQuantity,
                        MinStockQuantity = x.Product.MinStockQuantity,
                        OrderMinimumQuantity = x.Product.OrderMinimumQuantity,
                        OrderMaximumQuantity = x.Product.OrderMaximumQuantity,
                        SellUnitPrice = x.Product.SellUnitPrice,
                        OldSellUnitPrice = x.Product.OldSellUnitPrice,
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
                        StockType = x.Product.StockType,
                        ImagePreviewPath = x.Product.ImagePreview.Directory + x.Product.ImagePreview.FileName,
                        CategoryNames = x.Product.ProductCategories.Select(c => c.Category.Name).ToList(),
                        ManufacturerNames = x.Product.ProductManufacturers.Select(c => c.Manufacturer.Name).ToList(),
                        Attributes = x.Product.ProductAttributes.Select(c => c.Attribute).Select(z => new ProductAttributeDisplayModel()
                        {
                            Id = z.Id,
                            AttributeType = z.AttributeType,
                            Description = z.Description,
                            DisplayOrder = z.DisplayOrder,
                            Name = z.Name,
                            ImagePreviewPath = z.ImagePreview.Directory + z.ImagePreview.FileName,
                            Value = z.Value,
                        }).ToList(),
                        ProductTags = x.Product.ProductProductTags.Select(x => x.ProductTag).Select(cat => cat.Name).ToList(),
                    }).Take(4).ToList()
                })
                .ToList();

            result.Data = groups;

            return result;
        }

        /// <summary>
        /// Retrieves an admin paginated list of products (all products except deleted ones) for grid listing.
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public async Task<Result<PaginatedList<ProductModel>>> GetList(GridDataBound dataGrid)
        {
            var result = new Result<PaginatedList<ProductModel>>();

            var list = await (from product in _queryRepository.Table<Ecommerce.Core.Domain.Product>()
                                .Include(x => x.ProductInventories)
                                .ThenInclude(x => x.ProductAttribute)
                                .Include(x => x.ProductCategories)
                                .Include(x => x.ProductManufacturers)
                                .Include(x => x.ProductAttributes)
                                .Where(x => !x.Deleted)
                              select new ProductModel()
                              {
                                  Id = product.Id,
                                  CreateUserId = product.CreateUserId,
                                  UpdateUserId = product.UpdateUserId,
                                  Name = product.Name,
                                  MetaKeywords = product.MetaKeywords,
                                  MetaTitle = product.MetaTitle,
                                  FullDescription = product.FullDescription,
                                  AdminComment = product.AdminComment,
                                  MetaDescription = product.MetaDescription,
                                  DeliveryDateType = product.DeliveryDateType,
                                  TaxCategoryId = product.TaxCategoryId,
                                  TaxCategoryName = product.TaxCategory.Name,
                                  StockQuantity = product.StockQuantity,
                                  MinStockQuantity = product.MinStockQuantity,
                                  NotifyAdminForQuantityBelow = product.NotifyAdminForQuantityBelow,
                                  OrderMinimumQuantity = product.OrderMinimumQuantity,
                                  OrderMaximumQuantity = product.OrderMaximumQuantity,
                                  SellUnitPrice = product.SellUnitPrice,
                                  OldSellUnitPrice = product.OldSellUnitPrice,
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
                                  StockType = product.StockType,
                                  ImagePreviewId = product.ImagePreviewId,
                                  CategoryIds = product.ProductCategories.Select(c => c.CategoryId).ToList(),
                                  ManufacturerIds = product.ProductManufacturers.Select(c => c.ManufacturerId).ToList(),
                                  AttributeIds = product.ProductAttributes.Select(c => c.AttributeId).ToList(),
                                  Inventories = product.ProductInventories.Select(x => new ProductInventoryModel()
                                  {
                                      Id = x.Id,
                                      ProductId = x.ProductId,
                                      AttributeId = x.AttributeId,
                                      AttributeName = x.ProductAttribute.Name,
                                      StockQuantity = x.StockQuantity,
                                      ReservedQuantity = x.ReservedQuantity
                                  }).ToList(),
                                  TagIds = product.ProductProductTags.Select(c => c.ProductTagId).ToList()

                              }).OrderByDescending(x => x.Id).Cacheable().ToPaginatedListAsync(dataGrid);

            result.Data = list;

            return result;
        }

        /// <summary>
        /// Retrieves a single product by identifier, including related data (categories, images, attributes, tags, inventories).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<ProductModel>> GetById(int id)
        {
            var result = new Result<ProductModel>();
            var product = await _queryRepository.Table<Ecommerce.Core.Domain.Product>()
                .Include(x => x.CreateUser)
                .Include(x => x.UpdateUser)
                .Include(x => x.ProductCategories)
                .Include(x => x.ProductManufacturers)
                .Include(x => x.ProductImages)
                .Include(x => x.ProductAttributes)
                .Include(x => x.ProductProductTags).ThenInclude(x => x.ProductTag)
                .Include(x => x.ProductInventories).ThenInclude(x => x.ProductAttribute)
                .Include(x => x.RelatedProductProductId1Navigations).FirstOrDefaultAsync(x => x.Id == id);

            var productModel = new ProductModel()
            {
                Id = product.Id,
                CreateUserId = product.CreateUserId,
                UpdateUserId = product.UpdateUserId,
                Name = product.Name,
                MetaKeywords = product.MetaKeywords,
                MetaTitle = product.MetaTitle,
                ShortDescription = product.ShortDescription,
                FullDescription = product.FullDescription,
                AdminComment = product.AdminComment,
                MetaDescription = product.MetaDescription,
                DeliveryDateType = product.DeliveryDateType,
                TaxCategoryId = product.TaxCategoryId,
                StockQuantity = product.StockQuantity,
                MinStockQuantity = product.MinStockQuantity,
                NotifyAdminForQuantityBelow = product.NotifyAdminForQuantityBelow,
                OrderMinimumQuantity = product.OrderMinimumQuantity,
                OrderMaximumQuantity = product.OrderMaximumQuantity,
                SellUnitPrice = product.SellUnitPrice,
                OldSellUnitPrice = product.OldSellUnitPrice,
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
                StockType = product.StockType,
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
                RelatedProductIds = product.RelatedProductProductId1Navigations.Select(cat => cat.ProductId2).ToList(),
                TagIds = product.ProductProductTags.Select(c => c.ProductTagId).ToList(),
                Inventories = product.ProductInventories.Select(x => new ProductInventoryModel()
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    AttributeId = x.AttributeId,
                    AttributeName = x.ProductAttribute.Name,
                    StockQuantity = x.StockQuantity,
                    ReservedQuantity = x.ReservedQuantity,

                }).ToList(),

            };
            result.Data = productModel;

            return result;
        }

        /// <summary>
        /// Retrieves a list of products by their identifiers. Returns minimal information for select lists.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
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
            }).ToListAsync();
            result.Data = products;

            return result;
        }

        /// <summary>
        /// Searches products by a free-text input and returns up to 10 matching products for autocomplete/select.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Result<List<ProductModel>>> GetProductsByInput(string input)
        {
            var result = new Result<List<ProductModel>>();

            var quary = _queryRepository.Table<Ecommerce.Core.Domain.Product>();

            var id = 0;
            int.TryParse(input, out id);

            quary = quary.Where(x => x.Name.Contains(input) || x.MetaKeywords.Contains(input) || x.MetaTitle.Contains(input) || (id > 0 && x.Id == id));


            var list = await quary.Take(10).Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Id + " | " + x.Name,
            }).ToListAsync();

            result.Data = list;

            return result;
        }

        /// <summary>
        /// Adds a new product and related mappings (categories, manufacturers, images, attributes, related products, tags, inventories).
        /// Validates input and returns validation errors when present.
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        public async Task<Result<ProductModel>> Add(ProductModel productModel)
        {
            var result = new Result<ProductModel>();
            try
            {
                var currentDateTime = DateTime.UtcNow;

                // Validation
                var validationErrors = new List<Error>();
                if (string.IsNullOrWhiteSpace(productModel.Name))
                    validationErrors.Add(new Error(nameof(productModel.Name), "Product name is required."));

                if (productModel.SellUnitPrice < 0)
                    validationErrors.Add(new Error(nameof(productModel.SellUnitPrice), "SellUnitPrice must be zero or positive."));

                if (productModel.OldSellUnitPrice < 0)
                    validationErrors.Add(new Error(nameof(productModel.OldSellUnitPrice), "OldSellUnitPrice must be zero or positive."));

                if (!productModel.Inventories.Any())
                    validationErrors.Add(new Error(nameof(productModel.Inventories), "input Inventory(StockQuantity) is required."));

                if (productModel.Inventories != null)
                {
                    for (int i = 0; i < productModel.Inventories.Count; i++)
                    {
                        var inv = productModel.Inventories[i];
                        if (inv.StockQuantity < 0)
                            validationErrors.Add(new Error($"Inventories[{i}].StockQuantity", "StockQuantity cannot be negative."));
                        if (inv.BuyUnitPrice < 0)
                            validationErrors.Add(new Error($"Inventories[{i}].BuyUnitPrice", "BuyUnitPrice cannot be negative."));
                    }
                }

                if (!productModel.ManufacturerIds.Any())
                    validationErrors.Add(new Error(nameof(productModel.ManufacturerIds), "Manufacturer is required."));

                if (!productModel.CategoryIds.Any())
                    validationErrors.Add(new Error(nameof(productModel.CategoryIds), "Category is required."));

                if (productModel.MeasureType == null)
                    validationErrors.Add(new Error(nameof(productModel.MeasureType), "MeasureType is required."));

                if (productModel.CurrencyType == null)
                    validationErrors.Add(new Error(nameof(productModel.CurrencyType), "CurrencyType is required."));

                if (validationErrors.Any())
                {
                    result.Status = ResultStatusEnum.InvalidValidation;
                    result.Message = "Validation failed.";
                    result.Errors.AddRange(validationErrors);
                    return result;
                }

                // create product entity
                var product = new Ecommerce.Core.Domain.Product()
                {
                    CreateUserId = productModel.CreateUserId,
                    UpdateUserId = null,
                    Name = productModel.Name,
                    MetaKeywords = productModel.MetaKeywords,
                    MetaTitle = productModel.MetaTitle,
                    ShortDescription = productModel.ShortDescription,
                    FullDescription = productModel.FullDescription,
                    AdminComment = productModel.AdminComment,
                    MetaDescription = productModel.MetaDescription,
                    DeliveryDateType = productModel.DeliveryDateType,
                    TaxCategoryId = productModel.TaxCategoryId,
                    StockQuantity = productModel.StockQuantity,
                    MinStockQuantity = productModel.MinStockQuantity,
                    NotifyAdminForQuantityBelow = productModel.NotifyAdminForQuantityBelow,
                    OrderMinimumQuantity = productModel.OrderMinimumQuantity,
                    OrderMaximumQuantity = productModel.OrderMaximumQuantity,
                    SellUnitPrice = productModel.SellUnitPrice,
                    OldSellUnitPrice = productModel.OldSellUnitPrice,
                    CurrencyType = productModel.CurrencyType,
                    ImagePreviewId = productModel.ImagePreviewId,
                    AvailableStartDateTimeUtc = productModel.AvailableStartDateTimeUtc,
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
                    DisplayStockQuantity = productModel.DisplayStockQuantity,
                    DisableBuyButton = productModel.DisableBuyButton,
                    DisableWishlistButton = productModel.DisableWishlistButton,
                    AvailableForPreOrder = productModel.AvailableForPreOrder,
                    CallForPrice = productModel.CallForPrice,
                    Published = productModel.Published,
                    StockType = productModel.StockType,
                    Deleted = false,
                    CreatedOnUtc = currentDateTime,
                    UpdatedOnUtc = null
                };

                // use a transaction and minimize SaveChanges calls
                var transaction = await _commandRepository.BeginTransactionAsync();
                try
                {
                    // insert base product and persist to obtain Id
                    await _commandRepository.InsertAsync(product);
                    await _commandRepository.SaveChangesAsync();

                    // prepare related entities in-memory
                    var categories = (productModel.CategoryIds ?? Enumerable.Empty<int>())
                        .Select((catId, idx) => new ProductCategory { ProductId = product.Id, CategoryId = catId, DisplayOrder = idx })
                        .ToList();

                    var manufacturers = (productModel.ManufacturerIds ?? Enumerable.Empty<int>())
                        .Select((mId, idx) => new ProductManufacturer { ProductId = product.Id, ManufacturerId = mId, DisplayOrder = idx })
                        .ToList();

                    var images = (productModel.Images ?? Enumerable.Empty<ProductImageModel>())
                        .Select((img, idx) => new ProductImage { ProductId = product.Id, ImageId = img.ImageId, DisplayOrder = img.DisplayOrder != 0 ? img.DisplayOrder : idx })
                        .ToList();

                    var attributes = (productModel.AttributeIds ?? Enumerable.Empty<int>())
                        .Select(attrId => new ProductProductAttribute { ProductId = product.Id, AttributeId = attrId })
                        .ToList();

                    var related = (productModel.RelatedProductIds ?? Enumerable.Empty<int>())
                        .Select((rid, idx) => new RelatedProduct { ProductId1 = product.Id, ProductId2 = rid, DisplayOrder = idx })
                        .ToList();

                    var tags = (productModel.TagIds ?? Enumerable.Empty<int>())
                        .Select(tagId => new ProductProductTag { ProductId = product.Id, ProductTagId = tagId })
                        .ToList();

                    var inventories = (productModel.Inventories ?? Enumerable.Empty<ProductInventoryModel>())
                        .Select(inv => new ProductInventory { ProductId = product.Id, AttributeId = inv.AttributeId, StockQuantity = inv.StockQuantity, ReservedQuantity = inv.ReservedQuantity, BuyUnitPrice = inv.BuyUnitPrice })
                        .ToList();

                    // bulk insert related entities where applicable
                    var insertTasks = new List<Task>();

                    if (categories.Any())
                        insertTasks.Add(_commandRepository.InsertAsync(categories));
                    if (manufacturers.Any())
                        insertTasks.Add(_commandRepository.InsertAsync(manufacturers));
                    if (images.Any())
                        insertTasks.Add(_commandRepository.InsertAsync(images));
                    if (attributes.Any())
                        insertTasks.Add(_commandRepository.InsertAsync(attributes));
                    if (related.Any())
                        insertTasks.Add(_commandRepository.InsertAsync(related));
                    if (tags.Any())
                        insertTasks.Add(_commandRepository.InsertAsync(tags));
                    if (inventories.Any())
                        insertTasks.Add(_commandRepository.InsertAsync(inventories));

                    if (insertTasks.Any())
                        await Task.WhenAll(insertTasks);

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

        /// <summary>
        /// Updates an existing product and its related mappings. Performs updates using helper methods for each relation.
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        public async Task<Result<ProductModel>> Update(ProductModel productModel)
        {
            var result = new Result<ProductModel>();
            try
            {
                // Validation
                var validationErrors = new List<Error>();
                if (string.IsNullOrWhiteSpace(productModel.Name))
                    validationErrors.Add(new Error(nameof(productModel.Name), "Product name is required."));

                if (productModel.SellUnitPrice < 0)
                    validationErrors.Add(new Error(nameof(productModel.SellUnitPrice), "SellUnitPrice must be zero or positive."));

                if (productModel.OldSellUnitPrice < 0)
                    validationErrors.Add(new Error(nameof(productModel.OldSellUnitPrice), "OldSellUnitPrice must be zero or positive."));

                if (!productModel.Inventories.Any())
                    validationErrors.Add(new Error(nameof(productModel.Inventories), "input Inventory(StockQuantity) is required."));

                if (productModel.Inventories != null)
                {
                    for (int i = 0; i < productModel.Inventories.Count; i++)
                    {
                        var inv = productModel.Inventories[i];
                        if (inv.StockQuantity < 0)
                            validationErrors.Add(new Error($"Inventories[{i}].StockQuantity", "StockQuantity cannot be negative."));
                        if (inv.BuyUnitPrice < 0)
                            validationErrors.Add(new Error($"Inventories[{i}].BuyUnitPrice", "BuyUnitPrice cannot be negative."));
                    }
                }

                if (!productModel.ManufacturerIds.Any())
                    validationErrors.Add(new Error(nameof(productModel.ManufacturerIds), "Manufacturer is required."));

                if (!productModel.CategoryIds.Any())
                    validationErrors.Add(new Error(nameof(productModel.CategoryIds), "Category is required."));

                if (productModel.MeasureType == null)
                    validationErrors.Add(new Error(nameof(productModel.MeasureType), "MeasureType is required."));

                if (productModel.CurrencyType == null)
                    validationErrors.Add(new Error(nameof(productModel.CurrencyType), "CurrencyType is required."));

                if (validationErrors.Any())
                {
                    result.Status = ResultStatusEnum.InvalidValidation;
                    result.Message = "Validation failed.";
                    result.Errors.AddRange(validationErrors);
                    return result;
                }

                var product = await _queryRepository.Table<Ecommerce.Core.Domain.Product>().AsNoTracking().FirstAsync(x => x.Id == productModel.Id);
                if (product is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The Product not found";
                    return result;
                }

                var currentDateTime = DateTime.UtcNow;

                //if (productModel.StockQuantity != product.StockQuantity)
                //{
                //    var productInventory = await _queryRepository.Table<ProductInventory>().FirstAsync(x => x.ProductId == productModel.Id);
                //    productInventory.StockQuantity = productModel.StockQuantity;
                //    _commandRepository.UpdateAsync(productInventory);
                //}

                product.UpdateUserId = productModel.UpdateUserId;
                product.Name = productModel.Name;
                product.MetaKeywords = productModel.MetaKeywords;
                product.MetaTitle = productModel.MetaTitle;
                product.ShortDescription = productModel.ShortDescription;
                product.FullDescription = productModel.FullDescription;
                product.AdminComment = productModel.AdminComment;
                product.MetaDescription = productModel.MetaDescription;
                product.DeliveryDateType = productModel.DeliveryDateType;
                product.TaxCategoryId = productModel.TaxCategoryId;
                product.StockQuantity = productModel.StockQuantity;
                product.MinStockQuantity = productModel.MinStockQuantity;
                product.NotifyAdminForQuantityBelow = productModel.NotifyAdminForQuantityBelow;
                product.OrderMinimumQuantity = productModel.OrderMinimumQuantity;
                product.OrderMaximumQuantity = productModel.OrderMaximumQuantity;
                product.SellUnitPrice = productModel.SellUnitPrice;
                product.OldSellUnitPrice = productModel.OldSellUnitPrice;
                product.CurrencyType = productModel.CurrencyType;
                //product.Weight = productModel.Weight;
                //product.Length = productModel.Length;
                //product.Width = productModel.Width;
                //product.Height = productModel.Height;
                product.AvailableStartDateTimeUtc = productModel.AvailableStartDateTimeUtc;
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
                product.DisplayStockQuantity = productModel.DisplayStockQuantity;
                product.DisableBuyButton = productModel.DisableBuyButton;
                product.DisableWishlistButton = productModel.DisableWishlistButton;
                product.AvailableForPreOrder = productModel.AvailableForPreOrder;
                product.CallForPrice = productModel.CallForPrice;
                product.Published = productModel.Published;
                product.UpdatedOnUtc = currentDateTime;
                product.UpdateUserId = productModel.UpdateUserId;


                _commandRepository.UpdateAsync(product);

                await UpdateProductCategory(product.Id, productModel.CategoryIds.ToArray());

                await UpdateProductAttribute(product.Id, productModel.AttributeIds.ToArray());

                await UpdateProductManufacturer(product.Id, productModel.ManufacturerIds.ToArray());

                await UpdateProductPicture(product.Id, productModel.Images.ToArray());

                await UpdateProductTags(product.Id, productModel.TagIds.ToArray());

                await UpdateProductInventories(product.Id, productModel.Inventories);

                var productInventory = _queryRepository.Table<ProductInventory>().FirstOrDefault(x => x.ProductId == product.Id);
                productInventory.StockQuantity = product.StockQuantity;


                await UpdateRelatedProducts(product.Id, productModel.RelatedProductIds.ToArray());

                await _commandRepository.SaveChangesAsync();


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

        /// <summary>
        /// Replaces categories associated with the specified product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newCategories"></param>
        /// <returns></returns>
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
                        _commandRepository.DeleteAsync(cat);
                    }
                    foreach (var id in newCategories)
                    {
                        await _commandRepository.InsertAsync(new ProductCategory()
                        {
                            ProductId = productId,
                            CategoryId = id
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

        /// <summary>
        /// Replaces manufacturers associated with the specified product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newManufacturers"></param>
        /// <returns></returns>
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
                    {
                        _commandRepository.DeleteAsync(cat);
                    }
                    foreach (var id in newManufacturers)
                    {
                        await _commandRepository.InsertAsync(new ProductManufacturer()
                        {
                            ProductId = productId,
                            ManufacturerId = id
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
        /// <summary>
        /// Replaces product tags for the specified product with the provided tag identifiers.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newTags"></param>
        /// <returns></returns>
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
                    {
                        _commandRepository.DeleteAsync(cat);
                    }
                    foreach (var id in newTags)
                    {
                        await _commandRepository.InsertAsync(new ProductProductTag()
                        {
                            ProductId = productId,
                            ProductTagId = id
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
        /// <summary>
        /// Replaces product attributes associated with the specified product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newAttributes"></param>
        /// <returns></returns>
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
                    {
                        _commandRepository.DeleteAsync(cat);
                    }
                    foreach (var id in newAttributes)
                    {
                        await _commandRepository.InsertAsync(new ProductProductAttribute()
                        {
                            ProductId = productId,
                            AttributeId = id
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

        /// <summary>
        /// Updates product image mappings including display order. Existing mappings are replaced when the new order differs.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newPictures"></param>
        /// <returns></returns>
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
                    {
                        _commandRepository.DeleteAsync(pic);
                    }

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
        /// <summary>
        /// Synchronizes product inventory records: updates existing, deletes removed, and inserts new records.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="inventoris"></param>
        /// <returns></returns>
        private async Task<Result> UpdateProductInventories(int productId, List<ProductInventoryModel> inventoris)
        {
            var result = new Result();
            try
            {
                var productInventories = _queryRepository.Table<ProductInventory>().Where(x => x.ProductId == productId).ToList();

                var notExistInventories = productInventories.Where(x => !inventoris.Select(c => c.Id).Contains(x.Id));

                var existedInventories = productInventories.Where(x => inventoris.Select(c => c.Id).Contains(x.Id));

                foreach (var product in notExistInventories)
                {
                    _commandRepository.DeleteAsync(product);
                }

                existedInventories = existedInventories.Where(x => !notExistInventories.Select(c => c.Id).Contains(x.Id));

                foreach (var product in existedInventories)
                {
                    var newProduct = inventoris.FirstOrDefault(x => x.Id == product.Id);

                    product.StockQuantity = newProduct.StockQuantity;

                    _commandRepository.UpdateAsync(product);
                }
                var newInventories = inventoris.Where(x => x.Id == 0);

                foreach (var newInv in newInventories)
                {
                    await _commandRepository.InsertAsync(new ProductInventory()
                    {
                        ProductId = productId,
                        AttributeId = newInv.AttributeId,
                        StockQuantity = newInv.StockQuantity,
                        ReservedQuantity = 0
                    });
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
        /// <summary>
        /// Replaces related products for the specified product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newRelateds"></param>
        /// <returns></returns>
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
                    {
                        _commandRepository.DeleteAsync(cat);
                    }
                    foreach (var id in newRelateds)
                    {
                        await _commandRepository.InsertAsync(new RelatedProduct()
                        {
                            ProductId1 = productId,
                            ProductId2 = id
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

        /// <summary>
        /// Marks the specified product as deleted by setting its deleted flag.
        /// </summary>
        /// <remarks>This method performs a soft delete by updating the product's deleted flag rather than
        /// removing it from the database.</remarks>
        /// <param name="id">The unique identifier of the product to delete. Must correspond to an existing product.</param>
        /// <returns>A Result object indicating the outcome of the delete operation. The status is set to NotFound if the product
        /// does not exist.</returns>
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
            _commandRepository.UpdateAsync(product);
            await _commandRepository.SaveChangesAsync();

            return result;
        }

        /// <summary>
        /// Removes the product with the specified identifier from the data store.
        /// </summary>
        /// <param name="id">The unique identifier of the product to remove.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result"/>
        /// indicating the outcome of the remove operation. If the product is not found, the result status is set to
        /// <see cref="ResultStatusEnum.NotFound"/>.</returns>
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

            _commandRepository.DeleteAsync(product);
            await _commandRepository.SaveChangesAsync();

            return result;
        }

    }
}
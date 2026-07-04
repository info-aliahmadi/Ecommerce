using Hydra.Ecommerce.Core.Domain;
using Hydra.Ecommerce.Core.Enums;
using Hydra.FileStorage.Core.Models;
using Hydra.Kernel.GeneralModels;


namespace Hydra.Product.Core.Models
{
    public class ProductDisplayModel
    {

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreateUserBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? UpdateUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UpdateUserBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? ImagePreviewPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FullDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AdminComment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DeliveryDateType DeliveryDateType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TaxCategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TaxCategoryName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TaxRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MinStockQuantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StockType StockType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OrderMinimumQuantity { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int OrderMaximumQuantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SellUnitPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal OldSellUnitPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CurrencyType CurrencyType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ApprovedRatingSum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int NotApprovedRatingSum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ApprovedTotalReviews { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int NotApprovedTotalReviews { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasDiscountsApplied { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool MarkAsNew { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? MarkAsNewStartDateTimeUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? MarkAsNewEndDateTimeUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool NotReturnable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool AllowedQuantities { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsTaxExempt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowOnHomepage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFreeShipping { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool AllowCustomerReviews { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DisplayStockQuantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DisableBuyButton { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DisableWishlistButton { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool AvailableForPreOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool CallForPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdatedOnUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<int> CategoryIds { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public List<string> CategoryNames { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public List<int> ManufacturerIds { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public List<string> ManufacturerNames { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public List<ProductAttributeDisplayModel> Attributes { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        public MeasureType MeasureType { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public List<ProductInventoryModel> Inventories { get; set; } = new();


        /// <summary>
        /// 
        /// </summary>

        public List<string> ImagePaths { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>

        public List<int> RelatedProductIds { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>

        public List<string> ProductTags { get; set; } = new();

    }

}
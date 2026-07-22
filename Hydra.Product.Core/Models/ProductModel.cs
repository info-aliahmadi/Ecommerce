using Hydra.Ecommerce.Core.Domain;
using Hydra.Ecommerce.Core.Enums;
using Hydra.FileStorage.Core.Models;
using Hydra.Kernel.Enums;
using Hydra.Kernel.GeneralModels;


namespace Hydra.Product.Core.Models
{
    public class ProductModel
    {

        /// <summary>
        /// The unique identifier of the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The display name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// Identifier of the user who created the product.
        /// </summary>
        public int? CreateUserId { get; set; }

        /// <summary>
        /// Basic information about the user who created the product.
        /// </summary>
        public AuthorModel CreateUser { get; set; }

        /// <summary>
        /// Identifier of the user who last updated the product (if any).
        /// </summary>
        public int? UpdateUserId { get; set; }

        /// <summary>
        /// Basic information about the user who last updated the product.
        /// </summary>
        public AuthorModel UpdateUser { get; set; }

        /// <summary>
        /// File id of the preview image for the product (if any).
        /// </summary>
        public int? ImagePreviewId { get; set; }

        /// <summary>
        /// File id of the preview image for the product (if any).
        /// </summary>
        public FileUploadModel? ImagePreview { get; set; }

        /// <summary>
        /// SEO meta keywords for the product.
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// SEO meta title for the product.
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// SEO meta description for the product.
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// A short description shown in listings and previews.
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// The full description or details of the product.
        /// </summary>
        public string FullDescription { get; set; }

        /// <summary>
        /// Admin-only comments about the product.
        /// </summary>
        public string AdminComment { get; set; }

        /// <summary>
        /// Type of delivery for the product.
        /// </summary>
        public DeliveryDateType DeliveryDateType { get; set; }

        /// <summary>
        /// Localized/display name for the delivery date type.
        /// </summary>
        public string DeliveryDateName { get; set; }

        /// <summary>
        /// Identifier of the tax category applied to this product.
        /// </summary>
        public int TaxCategoryId { get; set; }

        /// <summary>
        /// Display name of the tax category.
        /// </summary>
        public string TaxCategoryName { get; set; }


        /// <summary>
        /// Minimum stock quantity before low-stock notifications are triggered.
        /// </summary>
        public int MinStockQuantity { get; set; }

        /// <summary>
        /// Indicates whether admin should be notified when stock falls below the minimum.
        /// </summary>
        public bool NotifyAdminForQuantityBelow { get; set; }

        /// <summary>
        /// Minimum quantity allowed per order.
        /// </summary>
        public int OrderMinimumQuantity { get; set; }

        /// <summary>
        /// Maximum quantity allowed per order.
        /// </summary>
        public int OrderMaximumQuantity { get; set; }

        /// <summary>
        /// Currency type used for pricing.
        /// </summary>
        public CurrencyType CurrencyType { get; set; }

        /// <summary>
        /// The UTC datetime when the product becomes available for sale.
        /// </summary>
        public DateTime? AvailableStartDateTimeUtc { get; set; }

        /// <summary>
        /// The UTC datetime when the product is no longer available for sale.
        /// </summary>
        public DateTime? AvailableEndDateTimeUtc { get; set; }

        /// <summary>
        /// Display order for sorting in lists.
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Sum of approved ratings for the product (for quick ranking calculations).
        /// </summary>
        public int ApprovedRatingSum { get; set; }

        /// <summary>
        /// Sum of not-approved ratings (if stored separately).
        /// </summary>
        public int NotApprovedRatingSum { get; set; }

        /// <summary>
        /// Total number of approved reviews.
        /// </summary>
        public int ApprovedTotalReviews { get; set; }

        /// <summary>
        /// Total number of not-approved reviews.
        /// </summary>
        public int NotApprovedTotalReviews { get; set; }

        /// <summary>
        /// Whether any discounts are currently applied to the product.
        /// </summary>
        public bool HasDiscountsApplied { get; set; }

        /// <summary>
        /// Indicates the product is marked as new.
        /// </summary>
        public bool MarkAsNew { get; set; }

        /// <summary>
        /// UTC start date for the "new" mark.
        /// </summary>
        public DateTime? MarkAsNewStartDateTimeUtc { get; set; }

        /// <summary>
        /// UTC end date for the "new" mark.
        /// </summary>
        public DateTime? MarkAsNewEndDateTimeUtc { get; set; }

        /// <summary>
        /// Indicates whether the product can be returned.
        /// </summary>
        public bool NotReturnable { get; set; }

        /// <summary>
        /// Whether allowed quantities (predefined quantity options) are used.
        /// </summary>
        public bool AllowedQuantities { get; set; }

        /// <summary>
        /// Whether the product is exempt from tax.
        /// </summary>
        public bool IsTaxExempt { get; set; }

        /// <summary>
        /// Whether the product should be shown on the homepage.
        /// </summary>
        public bool ShowOnHomepage { get; set; }

        /// <summary>
        /// Whether the product has free shipping.
        /// </summary>
        public bool IsFreeShipping { get; set; }

        /// <summary>
        /// Whether customers are allowed to leave reviews.
        /// </summary>
        public bool AllowCustomerReviews { get; set; }

        /// <summary>
        /// Whether to display stock quantity to customers.
        /// </summary>
        public bool DisplayStockQuantity { get; set; }

        /// <summary>
        /// Whether the buy button is disabled for this product.
        /// </summary>
        public bool DisableBuyButton { get; set; }

        /// <summary>
        /// Whether the wishlist button is disabled for this product.
        /// </summary>
        public bool DisableWishlistButton { get; set; }

        /// <summary>
        /// Whether the product is available for pre-order.
        /// </summary>
        public bool AvailableForPreOrder { get; set; }

        /// <summary>
        /// Whether the product requires customers to call for price.
        /// </summary>
        public bool CallForPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is published.
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Whether the product is deleted (soft delete).
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// UTC datetime when the product was created.
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// UTC datetime when the product was last updated.
        /// </summary>
        public DateTime? UpdatedOnUtc { get; set; }

        /// <summary>
        /// List of category identifiers the product belongs to.
        /// </summary>
        public List<int> CategoryIds { get; set; } = new();
        /// <summary>
        /// List of category identifiers the product belongs to.
        /// </summary>
        public List<string> CategoryNames { get; set; } = new();

        /// <summary>
        /// List of manufacturer identifiers associated with the product.
        /// </summary>
        public List<int> ManufacturerIds { get; set; } = new();
        /// <summary>
        /// List of manufacturer identifiers associated with the product.
        /// </summary>
        public List<string> ManufacturerNames { get; set; } = new();

        /// <summary>
        /// List of attribute identifiers assigned to the product.
        /// </summary>
        public List<int> AttributeIds { get; set; } = new();
        /// <summary>
        /// List of attribute identifiers assigned to the product.
        /// </summary>
        public List<string> AttributeNames { get; set; } = new();

        /// <summary>
        /// Measurement unit used for dimensions/weight.
        /// </summary>
        public MeasureType MeasureType { get; set; }

        /// <summary>
        /// Inventory records for product attributes/warehouses.
        /// </summary>
        public List<ProductVariantModel> Variants { get; set; } = new();

        /// <summary>
        /// Images associated with the product including file id and display order.
        /// </summary>
        public List<ProductImageModel> Images { get; set; } = new();

        /// <summary>
        /// Related product identifiers.
        /// </summary>
        public List<int> RelatedProductIds { get; set; } = new();

        /// <summary>
        /// Product tag identifiers.
        /// </summary>
        public List<int> TagIds { get; set; } = new();

        /// <summary>
        /// Product tag identifiers.
        /// </summary>
        public List<string> TagNames { get; set; } = new();

        public decimal StockQuantity { get; set; }
    }

}
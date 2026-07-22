using Hydra.Ecommerce.Core.Enums;
using Hydra.FileStorage.Core.Models;
using Hydra.Kernel.Enums;


namespace Hydra.Product.Core.Models
{
    public class ProductDisplayModel
    {
        /// <summary>
        /// Product unique identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product title or name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Uniqe Code of Product
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// Identifier of the user who created the product.
        /// </summary>
        public int? CreateUserId { get; set; }

        /// <summary>
        /// Display name of the user who created the product.
        /// </summary>
        public string CreateUserBy { get; set; }

        /// <summary>
        /// Identifier of the user who last updated the product, if any.
        /// </summary>
        public int? UpdateUserId { get; set; }

        /// <summary>
        /// Display name of the user who last updated the product.
        /// </summary>
        public string UpdateUserBy { get; set; }

        /// <summary>
        /// URL or path to the preview image for the product.
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
        /// Short description used in listings.
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Full detailed description of the product.
        /// </summary>
        public string FullDescription { get; set; }

        /// <summary>
        /// Administrative comments for internal use.
        /// </summary>
        public string AdminComment { get; set; }

        /// <summary>
        /// Delivery date type for the product.
        /// </summary>
        public DeliveryDateType DeliveryDateType { get; set; }

        /// <summary>
        /// Identifier of the tax category applied to this product.
        /// </summary>
        public int TaxCategoryId { get; set; }

        /// <summary>
        /// Display name of the tax category.
        /// </summary>
        public string TaxCategoryName { get; set; }

        /// <summary>
        /// Tax rate percentage (if applicable).
        /// </summary>
        public int TaxRate { get; set; }

        /// <summary>
        /// Minimum stock threshold for alerts.
        /// </summary>
        public int MinStockQuantity { get; set; }

        /// <summary>
        /// Minimum quantity allowed per order.
        /// </summary>
        public int OrderMinimumQuantity { get; set; }

        /// <summary>
        /// Maximum quantity allowed per order.
        /// </summary>
        public int OrderMaximumQuantity { get; set; }

        /// <summary>
        /// Currency used for product pricing.
        /// </summary>
        public CurrencyType CurrencyType { get; set; }

        /// <summary>
        /// Display ordering weight for lists.
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Sum of approved rating values for quick ranking.
        /// </summary>
        public int ApprovedRatingSum { get; set; }

        /// <summary>
        /// Sum of not-approved rating values.
        /// </summary>
        public int NotApprovedRatingSum { get; set; }

        /// <summary>
        /// Number of approved reviews.
        /// </summary>
        public int ApprovedTotalReviews { get; set; }

        /// <summary>
        /// Number of not-approved reviews.
        /// </summary>
        public int NotApprovedTotalReviews { get; set; }

        /// <summary>
        /// Whether the product currently has discounts applied.
        /// </summary>
        public bool HasDiscountsApplied { get; set; }

        /// <summary>
        /// Indicates if the product is marked as new.
        /// </summary>
        public bool MarkAsNew { get; set; }

        /// <summary>
        /// UTC start date when the product is considered new.
        /// </summary>
        public DateTime? MarkAsNewStartDateTimeUtc { get; set; }

        /// <summary>
        /// UTC end date when the product is no longer new.
        /// </summary>
        public DateTime? MarkAsNewEndDateTimeUtc { get; set; }

        /// <summary>
        /// Whether the product is returnable.
        /// </summary>
        public bool NotReturnable { get; set; }

        /// <summary>
        /// Whether predefined allowed quantities are used.
        /// </summary>
        public bool AllowedQuantities { get; set; }

        /// <summary>
        /// Whether the product is exempt from taxes.
        /// </summary>
        public bool IsTaxExempt { get; set; }

        /// <summary>
        /// Whether the product should appear on the homepage.
        /// </summary>
        public bool ShowOnHomepage { get; set; }

        /// <summary>
        /// Whether shipping is free for this product.
        /// </summary>
        public bool IsFreeShipping { get; set; }

        /// <summary>
        /// Whether customers can submit reviews for this product.
        /// </summary>
        public bool AllowCustomerReviews { get; set; }

        /// <summary>
        /// Whether stock quantity is visible to customers.
        /// </summary>
        public bool DisplayStockQuantity { get; set; }

        /// <summary>
        /// Whether the buy button is disabled.
        /// </summary>
        public bool DisableBuyButton { get; set; }

        /// <summary>
        /// Whether the wishlist button is disabled.
        /// </summary>
        public bool DisableWishlistButton { get; set; }

        /// <summary>
        /// Whether the product is available for pre-order.
        /// </summary>
        public bool AvailableForPreOrder { get; set; }

        /// <summary>
        /// Whether customers must call to get the price.
        /// </summary>
        public bool CallForPrice { get; set; }

        /// <summary>
        /// UTC date when the product was created.
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// UTC date when the product was last updated.
        /// </summary>
        public DateTime? UpdatedOnUtc { get; set; }

        /// <summary>
        /// Identifiers of categories this product belongs to.
        /// </summary>
        public List<int> CategoryIds { get; set; } = new();

        /// <summary>
        /// Names of categories this product belongs to.
        /// </summary>
        public List<CategoryDisplayModel> Categories { get; set; } = new();

        /// <summary>
        /// Identifiers of manufacturers associated with this product.
        /// </summary>
        public List<int> ManufacturerIds { get; set; } = new();

        /// <summary>
        /// Names of manufacturers associated with this product.
        /// </summary>
        public List<string> ManufacturerNames { get; set; } = new();

        /// <summary>
        /// List of attribute display models for the product.
        /// </summary>
        public List<ProductAttributeDisplayModel> Attributes { get; set; } = new();

        /// <summary>
        /// Measurement unit for dimensions and weight.
        /// </summary>
        public MeasureType MeasureType { get; set; }

        /// <summary>
        /// Inventory display records for the product.
        /// </summary>
        public List<ProductVariantDisplayModel> Variants { get; set; } = new();

        /// <summary>
        /// Paths to product images.
        /// </summary>
        public List<string> ImagePaths { get; set; } = new();

        /// <summary>
        /// Identifiers of related products.
        /// </summary>
        public List<int> RelatedProductIds { get; set; } = new();

        /// <summary>
        /// Product tag names associated with the product.
        /// </summary>
        public List<string> ProductTags { get; set; } = new();

        /// <summary>
        /// موجودی
        /// </summary>
        public decimal StockQuantity { get; set; }
    }

}
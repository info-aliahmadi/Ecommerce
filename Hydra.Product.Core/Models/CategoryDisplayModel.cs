using Hydra.FileStorage.Core.Models;

namespace Hydra.Product.Core.Models
{
    public class CategoryDisplayModel
    {
        /// <summary>
        /// The unique identifier of the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The display name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A unique key for the category used in URLs or lookups.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// SEO meta keywords for the category.
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// SEO meta title for the category.
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Description of the category.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// SEO meta description for the category.
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Identifier of the parent category if present.
        /// </summary>
        public int? ParentCategoryId { get; set; }

        /// <summary>
        /// URL or path to the category preview image.
        /// </summary>
        public string? ImagePreviewPath { get; set; }

        /// <summary>
        /// Hex or named color associated with the category for UI.
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// Whether the category should be shown on the homepage.
        /// </summary>
        public bool ShowOnHomepage { get; set; }

        /// <summary>
        /// Display order used for sorting categories.
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Number of products under this category.
        /// </summary>
        public int ProductsCount { get; set; }

        /// <summary>
        /// Number of discounts applied to this category.
        /// </summary>
        public int Discounts { get; set; }

        /// <summary>
        /// Child categories for display purposes.
        /// </summary>
        public List<CategoryDisplayModel>? Childs { get; set; } = new List<CategoryDisplayModel>();
    }
}
using Hydra.FileStorage.Core.Models;

namespace Hydra.Product.Core.Models
{
    public class CategoryModel
    {
        /// <summary>
        /// Indicates whether the category is currently being edited in the UI.
        /// </summary>
        public bool IsEdited { get; set; } = false;

        /// <summary>
        /// The unique identifier of the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The display name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A unique key for the category (often used in URLs or lookups).
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
        /// A short or long description of the category.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// SEO meta description for the category.
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Identifier of the parent category when this category is a child; otherwise null.
        /// </summary>
        public int? ParentCategoryId { get; set; }

        /// <summary>
        /// File id of the preview image for the category (if any).
        /// </summary>
        public int? ImagePreviewId { get; set; }

        /// <summary>
        /// Hex or named color associated with this category for UI purposes.
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// Preview image information for the category.
        /// </summary>
        public FileUploadModel? ImagePreview { get; set; }

        /// <summary>
        /// Whether the category should be shown on the homepage.
        /// </summary>
        public bool ShowOnHomepage { get; set; }

        /// <summary>
        /// Whether the category is published and visible to customers.
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Whether the category is soft-deleted.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Display order used for sorting categories in lists.
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// UTC datetime when the category was created.
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// UTC datetime when the category was last updated.
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Number of discounts currently applied to this category.
        /// </summary>
        public int Discounts { get; set; }

        /// <summary>
        /// Child categories of this category.
        /// </summary>
        public List<CategoryModel>? Childs { get; set; } = new List<CategoryModel>();

    }
}


using Hydra.Ecommerce.Core.Enums;
using Hydra.FileStorage.Core.Models;

namespace Hydra.Product.Core.Models
{
    public class ProductAttributeDisplayModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AttributeType AttributeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FileUploadModel? ImagePreview { get; set; }

        /// <summary>
        /// show in homepage
        /// </summary>
        public bool IsFeatured { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Description { get; set; }
    }
}
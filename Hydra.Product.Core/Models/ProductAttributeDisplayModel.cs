

using Hydra.Ecommerce.Core.Enums;
using Hydra.FileStorage.Core.Models;

namespace Hydra.Product.Core.Models
{
    public class ProductAttributeDisplayModel
    {
        public ProductAttributeDisplayModel()
        {
            
        }
        public ProductAttributeDisplayModel(Ecommerce.Core.Domain.ProductAttribute? productAttribute)
        {
            if (productAttribute != null)
            {
                Id = productAttribute.Id;
                DisplayName = productAttribute.DisplayName;
                Key = productAttribute.Key;
                AttributeType = productAttribute.AttributeType;
                DisplayOrder = productAttribute.DisplayOrder;
                Description = productAttribute.Description;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

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
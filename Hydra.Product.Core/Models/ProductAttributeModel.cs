using Hydra.Ecommerce.Core.Domain;
using Hydra.Ecommerce.Core.Enums;
using Hydra.FileStorage.Core.Models;

namespace Hydra.Product.Core.Models
{
    public class ProductAttributeModel
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
        public int? ImagePreviewId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FileUploadModel? ImagePreview { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowOnHomepage { get; set; }
        
    }
}
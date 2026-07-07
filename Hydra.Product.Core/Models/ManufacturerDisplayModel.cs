using Hydra.FileStorage.Core.Models;

namespace Hydra.Product.Core.Models
{
    public class ManufacturerDisplayModel
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
        public string MetaKeywords { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string MetaTitle { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string MetaDescription { get; set; }


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
        public int ProductsCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Discounts { get; set; }

    }
}
using Hydra.Ecommerce.Core.Enums;
using Hydra.FileStorage.Core.Models;
using Hydra.Product.Core.Models;

namespace Hydra.Order.Core.Models
{
    public class ShoppingCartItemModel
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int ProductVariantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ProductVariantDisplayModel Variant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FileUploadModel? Image { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ShoppingCartTypeEnum ShoppingCartTypeId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CategoryDisplayModel>? Categories { get; set; }

    }
}
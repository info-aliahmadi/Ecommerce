using Hydra.Ecommerce.Core.Enums;

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
        public int ProductVariantId { get; set; }


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
        public DateTime CreatedOnUtc { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime UpdatedOnUtc { get; set; }


    }
}
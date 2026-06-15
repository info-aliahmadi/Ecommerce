using Hydra.Ecommerce.Core.Domain;

namespace Hydra.Product.Core.Models
{
    public class ProductInventoryModel
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? AttributeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? AttributeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal StockQuantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ReservedQuantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal BuyUnitPrice { get; set; }

    }
}
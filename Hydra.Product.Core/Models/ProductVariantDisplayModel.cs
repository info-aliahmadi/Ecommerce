
namespace Hydra.Product.Core.Models
{
    public class ProductVariantDisplayModel
    {

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// like : SKU001 or TS-RED-M based on SKU of product and attribute
        /// for one product we have one variant without attribute and for multiple stock we have multiple variants with multiple attribute
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SellPrice { get; set; }

        /// <summary>
        /// Discount
        /// </summary>
        public decimal OldSellPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProductInventoryDisplayModel ProductInventory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ProductAttributeDisplayModel> ProductAttributes { get; set; } = new();

    }
}
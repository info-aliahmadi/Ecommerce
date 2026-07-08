namespace Hydra.Product.Core.Models
{
    public class ProductBundleModel
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Display order for this product; lower values show first.
        /// </summary>
        public int DisplayOrder { get; set; }

    }
}
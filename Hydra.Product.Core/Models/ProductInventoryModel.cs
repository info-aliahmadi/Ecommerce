namespace Hydra.Product.Core.Models
{
    public class ProductInventoryModel
    {
        public int Id { get; set; }

        public int VariantId { get; set; }

        public decimal StockQuantity { get; set; }

        public decimal ReservedQuantity { get; set; }
    }
}

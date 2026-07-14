using Hydra.Ecommerce.Core.Enums;

namespace Hydra.Inventory.Core.Models
{
    public class StockAdjustmentModel
    {
        public int VariantId { get; set; }

        public decimal Quantity { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}

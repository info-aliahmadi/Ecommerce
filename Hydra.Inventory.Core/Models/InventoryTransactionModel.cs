using Hydra.Ecommerce.Core.Enums;

namespace Hydra.Inventory.Core.Models
{
    public class InventoryTransactionModel
    {
        public int Id { get; set; }

        public int VariantId { get; set; }

        public TransactionType TransactionType { get; set; }

        public decimal StockQuantity { get; set; }

        public decimal ReservedQuantity { get; set; }

        public DateTime CreatedDatetime { get; set; }
    }
}

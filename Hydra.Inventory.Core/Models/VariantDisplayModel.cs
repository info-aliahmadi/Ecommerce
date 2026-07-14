namespace Hydra.Inventory.Core.Models
{
    public class VariantDisplayModel
    {
        public int Id { get; set; }

        public string SKU { get; set; }

        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public decimal SellPrice { get; set; }

        public decimal OldSellPrice { get; set; }

        public decimal StockQuantity { get; set; }

        public decimal ReservedQuantity { get; set; }

        public List<VariantAttributeDisplayModel> Attributes { get; set; } = new();
    }
}

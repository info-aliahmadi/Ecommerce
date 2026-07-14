using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class ProductVariant : BaseEntity<int>
{
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
    public Product Product { get; set; }

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
    public ProductInventory ProductInventory { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<ProductVariantAttribute> VariantAttributes { get; set; }

}
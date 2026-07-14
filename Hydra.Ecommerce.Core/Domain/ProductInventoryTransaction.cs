using Hydra.Ecommerce.Core.Enums;
using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class ProductInventoryTransaction : BaseEntity<int>
{
    /// <summary>
    /// 
    /// </summary>
    public int ProductInventoryId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public virtual ProductInventory ProductInventory { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public TransactionType TransactionType { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public decimal StockQuantity { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal ReservedQuantity { get; set; }

    /// <summary>
    /// تاریخ ساخت
    /// </summary>
    public DateTime CreatedDatetime { get; set; }


}

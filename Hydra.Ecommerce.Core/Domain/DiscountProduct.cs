using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class DiscountProduct : BaseEntity<int>
{
    public int DiscountId { get; set; }
    public Discount Discount { get; set; }
    
    public int ProductId { get; set; }

    public virtual Product Product { get; set; }
}
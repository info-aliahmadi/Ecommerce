using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class DiscountCategory : BaseEntity<int>
{
    public int DiscountId { get; set; }
    public Discount Discount { get; set; }
    
    public int CategoryId { get; set; }

    public virtual Category Category { get; set; }
}
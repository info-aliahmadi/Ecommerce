using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class DiscountManufacturer : BaseEntity<int>
{
    public int DiscountId { get; set; }
    public Discount Discount { get; set; }
    
    public int ManufacturerId { get; set; }

    public virtual Manufacturer Manufacturer { get; set; }
}
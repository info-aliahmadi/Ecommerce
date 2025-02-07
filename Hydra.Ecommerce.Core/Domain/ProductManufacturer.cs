using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class ProductManufacturer : BaseEntity<int>
{
    public int ManufacturerId { get; set; }

    public int ProductId { get; set; }

    public int DisplayOrder { get; set; }

    public virtual Manufacturer Manufacturer { get; set; }

    public virtual Product Product { get; set; }
}
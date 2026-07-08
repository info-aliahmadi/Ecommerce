using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class ProductBundle : BaseEntity<int>
{
    public int BundleId { get; set; }

    public int ProductId { get; set; }

    public int DisplayOrder { get; set; }

    public virtual Bundle Bundle { get; set; }

    public virtual Product Product { get; set; }
}
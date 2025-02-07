using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class ShippingMethod : BaseEntity<int>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public int DisplayOrder { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
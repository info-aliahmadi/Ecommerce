using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class ProductVariantAttribute : BaseEntity<int>
{
    public int VariantId { get; set; }

    public int AttributeId { get; set; }

    public ProductVariant Variant { get; set; }

    public ProductAttribute Attribute { get; set; }

}
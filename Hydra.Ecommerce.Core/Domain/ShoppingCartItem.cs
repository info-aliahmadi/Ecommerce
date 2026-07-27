using Hydra.Auth.Domain;
using Hydra.Ecommerce.Core.Enums;
using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class ShoppingCartItem : BaseEntity<int>
{
    public int UserId { get; set; }

    public int ProductVariantId { get; set; }

    public ShoppingCartTypeEnum ShoppingCartTypeId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime UpdatedOnUtc { get; set; }

    public virtual ProductVariant ProductVariant { get; set; }

    public virtual User User { get; set; }
}
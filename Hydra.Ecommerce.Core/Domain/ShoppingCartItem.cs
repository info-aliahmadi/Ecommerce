﻿using Hydra.Auth.Domain;
using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class ShoppingCartItem : BaseEntity<int>
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public byte ShoppingCartTypeId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime UpdatedOnUtc { get; set; }

    public virtual Product Product { get; set; }

    public virtual User User { get; set; }
}
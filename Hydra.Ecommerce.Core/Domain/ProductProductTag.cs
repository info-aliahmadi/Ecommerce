﻿using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class ProductProductTag
{
    public int ProductTagId { get; set; }
    public ProductTag ProductTag { get; set; }
    
    public int ProductId { get; set; }

    public virtual Product Product { get; set; }
}
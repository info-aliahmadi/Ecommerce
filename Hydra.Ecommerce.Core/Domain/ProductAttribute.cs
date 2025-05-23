﻿using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class ProductAttribute : BaseEntity<int>
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public AttributeType AttributeType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? PictureId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// 
    /// </summary>

    public virtual List<ProductProductAttribute> ProductAttributes { get; set; } = new();
    /// <summary>
    /// 
    /// </summary>
    public virtual List<ProductInventory> ProductInventories { get; set; } = new();
}
/// <summary>
/// 
/// </summary>
public enum AttributeType
{
    Color = 0,
    Size = 1,
    Weight = 2,
    Length = 3,
    Width = 4,
    Height = 5,
    Material = 6,
    Style = 7,
    Pattern = 8,
    Brand = 9,
    Model = 10,

}
using Hydra.Ecommerce.Core.Enums;
using Hydra.FileStorage.Core.Domain;
using Hydra.Kernel.Data;

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
    public int? ImagePreviewId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public FileUpload? ImagePreview { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// show in homepage
    /// </summary>
    public bool ShowOnHomepage { get; set; } = false;

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
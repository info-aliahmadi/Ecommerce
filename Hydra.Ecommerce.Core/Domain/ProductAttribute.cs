using Hydra.Ecommerce.Core.Enums;
using Hydra.FileStorage.Core.Domain;
using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class ProductAttribute : BaseEntity<int>
{
    /// <summary>
    /// 
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// Key Or SKU
    /// </summary>
    public string Key { get; set; }

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
    /// 
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// specific attribute that nit related to inventory like style or pattern or brand
    /// </summary>
    public virtual List<ProductProductAttribute> ProductAttributes { get; set; } = new();


    public List<ProductVariantAttribute> VariantAttributes { get; set; }

}
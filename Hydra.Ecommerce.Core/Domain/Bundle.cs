using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class Bundle : BaseEntity<int>
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// show in homepage
    /// </summary>
    public bool ShowOnHomepage { get; set; } = false;

    public DateTime CreatedOnUtc { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<ProductBundle> ProductBundles { get; set; } = new List<ProductBundle>();
}
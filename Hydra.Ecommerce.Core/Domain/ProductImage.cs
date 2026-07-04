using Hydra.FileStorage.Core.Domain;
using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class ProductImage : BaseEntity<int>
{
    public int ImageId { get; set; }
    public FileUpload Image { get; set; }
    
    public int ProductId { get; set; }

    public int DisplayOrder { get; set; }

    public virtual Product Product { get; set; }
}
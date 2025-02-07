using Hydra.FileStorage.Core.Domain;
using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class ProductPicture : BaseEntity<int>
{
    public int PictureId { get; set; }
    public FileUpload Picture { get; set; }
    
    public int ProductId { get; set; }

    public int DisplayOrder { get; set; }

    public virtual Product Product { get; set; }
}
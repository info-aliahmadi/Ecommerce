using Hydra.FileStorage.Core.Models;

namespace Hydra.Product.Core.Models
{
    public class CuratedStyleProductModel
    {
        public int AttributeId { get; set; }

        public string AttributeName { get; set; }

        public string? AttributeValue { get; set; }

        public string? AttributeDescription { get; set; }

        public FileUploadModel? ImagePreview { get; set; }

        public List<ProductDisplayModel> Products { get; set; } = new();
    }
}

namespace Hydra.Product.Core.Models
{
    public class CuratedProductGroupModel
    {
        public int AttributeId { get; set; }

        public string AttributeName { get; set; }

        public string? AttributeValue { get; set; }

        public string? AttributeDescription { get; set; }

        public string? ImagePreviewPath { get; set; }

        public List<ProductDisplayModel> Products { get; set; } = new();
    }
}

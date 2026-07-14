using Hydra.Ecommerce.Core.Enums;
using Hydra.FileStorage.Core.Models;

namespace Hydra.Inventory.Core.Models
{
    public class VariantAttributeDisplayModel
    {
        public int Id { get; set; }

        public int AttributeId { get; set; }

        public string DisplayName { get; set; }

        public string Key { get; set; }

        public AttributeType AttributeType { get; set; }

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        public FileUploadModel? ImagePreview { get; set; }
    }
}

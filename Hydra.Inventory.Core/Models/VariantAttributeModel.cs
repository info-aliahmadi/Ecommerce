using Hydra.Ecommerce.Core.Enums;

namespace Hydra.Inventory.Core.Models
{
    public class VariantAttributeModel
    {
        public int AttributeId { get; set; }

        public string? AttributeName { get; set; }

        public string? AttributeValue { get; set; }

        public AttributeType AttributeType { get; set; }
    }
}

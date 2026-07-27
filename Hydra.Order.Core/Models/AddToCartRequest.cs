
namespace Hydra.Order.Core.Models
{
    public class AddToCartRequest
    {
        public int VariantId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}

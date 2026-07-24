
namespace Hydra.Order.Core.Models
{
    public class AddToCartRequest
    {
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}

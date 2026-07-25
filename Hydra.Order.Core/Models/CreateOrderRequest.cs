using Hydra.Ecommerce.Core.Enums;

namespace Hydra.Order.Core.Models
{
    public class CreateOrderRequest
    {
        public int? AddressId { get; set; }
        public ShippingMethod? ShippingMethodId { get; set; }
        public PaymentMethod? PaymentMethodId { get; set; }
        public string? OrderNote { get; set; }
        public int? DiscountId { get; set; }
        public List<CreateOrderItemRequest> Items { get; set; } = new();
    }

    public class CreateOrderItemRequest
    {
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

using Hydra.Ecommerce.Core.Enums;

namespace Hydra.Order.Core.Models
{
    public class CreateOrderRequest
    {
        public int? AddressId { get; set; }
        public int? ShippingMethodId { get; set; }
        public PaymentMethod? PaymentMethodId { get; set; }
        public List<CreateOrderItemRequest> Items { get; set; } = new();
    }

    public class CreateOrderItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

using Hydra.Ecommerce.Core.Enums;

namespace Hydra.Order.Core.Models
{
    public class OrderChangeStatusModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int OrderId { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ShippingMethod? ShippingMethodId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OrderStatus OrderStatusId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ShippingStatus ShippingStatusId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PaymentStatus PaymentStatusId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PaymentMethod? PaymentMethodId { get; set; }

    }
}

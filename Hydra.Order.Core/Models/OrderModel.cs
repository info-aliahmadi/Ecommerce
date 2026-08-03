using Hydra.Ecommerce.Core.Enums;
using Hydra.Kernel.Enums;
using Microsoft.OpenApi.Extensions;

namespace Hydra.Order.Core.Models
{
    public class OrderModel
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string UserAvatar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? ShipmentId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? AddressId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string AddressSnapshot { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CurrencyType UserCurrencyType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal FinalPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal RefundedAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string CustomerIp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool AllowStoringCreditCardNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Deleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? PaymentDateUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> OrderNotes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ShippingTax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ShippingAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ShippingAmountTax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? TransactionTrackingCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? PaymentTrackingCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? TrackingNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<OrderItemModel> Items { get; set; }


    }
}

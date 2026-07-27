namespace Hydra.Ecommerce.Core.Enums
{
    /// <summary>
    /// Represents a payment status enumeration
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// Initial state — order created, payment not yet initiated or awaiting customer action (e.g., redirect to payment gateway).
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Payment authorized (hold placed on funds) but not yet captured. Common for delayed-capture flows (e.g., pre-orders, manual review).
        /// </summary>
        Authorized = 2,

        /// <summary>
        /// Payment fully captured/succeeded. Funds transferred. Order can proceed to fulfillment.
        /// </summary>
        Paid = 3,

        /// <summary>
        /// Failed in payment
        /// </summary>
        Failed = 4,

        /// <summary>
        /// A portion of the paid amount has been refunded to the customer.
        /// </summary>
        PartiallyRefunded = 5,

        /// <summary>
        /// Full amount refunded. Order typically moves to cancelled/returned state.
        /// </summary>
        Refunded = 6,

        /// <summary>
        /// Authorization cancelled before capture (no money moved). Used when order is cancelled before settlement.
        /// </summary>
        Voided = 7
    }
}
namespace Hydra.Order.Core.Models
{
    public class ProcessPaymentRequest
    {
        public int OrderId { get; set; }
        public byte? PaymentMethodId { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardCvv2 { get; set; }
        public string CardExpirationMonth { get; set; }
        public string CardExpirationYear { get; set; }
    }
}

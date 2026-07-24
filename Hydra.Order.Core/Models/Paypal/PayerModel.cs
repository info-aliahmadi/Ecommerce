namespace Hydra.Order.Core.Models.Paypal
{
    public sealed class PayerModel
    {
        public NameModel name { get; set; }
        public string email_address { get; set; }
        public string payer_id { get; set; }
    }

}

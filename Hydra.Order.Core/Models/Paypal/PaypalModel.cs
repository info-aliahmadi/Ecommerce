namespace Hydra.Order.Core.Models.Paypal
{
    public sealed class PaypalModel
    {
        public NameModel name { get; set; }
        public string email_address { get; set; }
        public string account_id { get; set; }
    }

}

namespace Hydra.Order.Core.Models.Paypal
{
    public sealed class SellerProtectionModel
    {
        public string status { get; set; }
        public List<string> dispute_categories { get; set; }
    }

}

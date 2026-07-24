namespace Hydra.Order.Core.Models.Paypal
{
    public sealed class CaptureOrderResponseModel
    {
        public string id { get; set; }
        public string status { get; set; }
        public PaymentSourceModel payment_source { get; set; }
        public List<PurchaseUnitModel> purchase_units { get; set; }
        public PayerModel payer { get; set; }
        public List<LinkModel> links { get; set; }
    }

}

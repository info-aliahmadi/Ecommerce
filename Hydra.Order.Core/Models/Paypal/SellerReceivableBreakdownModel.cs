namespace Hydra.Order.Core.Models.Paypal
{
    public sealed class SellerReceivableBreakdownModel
    {
        public AmountModel gross_amount { get; set; }
        public PaypalFeeModel paypal_fee { get; set; }
        public AmountModel net_amount { get; set; }
    }

}

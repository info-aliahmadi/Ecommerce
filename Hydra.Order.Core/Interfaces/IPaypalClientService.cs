using Hydra.Order.Core.Models.Paypal;

namespace Hydra.Order.Core.Interfaces.Paypal
{
    public interface IPaypalClientService
    {
        Task<AuthResponseModel> Authenticate();
        Task<CaptureOrderResponseModel> CaptureOrder(string orderId);
        Task<CreateOrderResponseModel> CreateOrder(string value, string currency, string reference);
    }
}

using Hydra.Kernel.GeneralModels;
using Hydra.Order.Core.Models;

namespace Hydra.Order.Core.Interfaces
{
    public interface IPaymentService
    {
        Task<Result<PaymentViewModel>> GetOrderPaymentById(int id);
        Task<Result<List<PaymentStatusModel>>> GetAllPaymentStatus();
        Task<Result<PaginatedList<PaymentModel>>> GetList(GridDataBound dataGrid);
        Task<Result<PaymentModel>> GetById(int id);
        Task<Result<PaymentModel>> Add(PaymentModel paymentModel);
        Task<Result<PaymentModel>> Update(PaymentModel paymentModel);
        Task<Result> Delete(int id);

        // User-facing methods
        Task<Result<List<PaymentModel>>> GetMyPayments(int userId);
        Task<Result<PaymentViewModel>> GetMyPaymentById(int userId, int paymentId);
        Task<Result<PaymentModel>> ProcessPayment(int userId, ProcessPaymentRequest request);
    }
}

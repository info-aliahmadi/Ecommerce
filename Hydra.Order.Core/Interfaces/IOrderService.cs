using Hydra.Kernel.GeneralModels;
using Hydra.Order.Core.Models;

namespace Hydra.Order.Core.Interfaces
{
    public interface IOrderService
    {

        // User-facing methods
        Task<Result<List<OrderModel>>> GetMyOrders(int userId);
        Task<Result<OrderModel>> GetMyOrderById(int userId, int orderId);
        Task<Result<List<OrderItemModel>>> GetMyOrderItems(int userId, int orderId);
        Task<Result<OrderModel>> CreateOrder(int userId, CreateOrderRequest request);
        Task<Result> ConfirmOrder(int userId, int orderId);
        Task<Result> CancelOrder(int userId, int orderId);



        Task<Result<PaginatedList<OrderModel>>> GetList(GridDataBound dataGrid);
        Task<Result<OrderModel>> GetById(int id);
        Task<Result<OrderModel>> Update(OrderModel orderModel);
        Task<Result<OrderChangeStatusModel>> UpdateState(OrderChangeStatusModel orderModel);
        Task<Result> Delete(int id);
    }
}

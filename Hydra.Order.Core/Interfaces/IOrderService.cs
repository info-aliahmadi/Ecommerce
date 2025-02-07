using Hydra.Kernel.GeneralModels;
using Hydra.Order.Core.Models;

namespace Hydra.Order.Core.Interfaces
{
    public interface IOrderService
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<PaginatedList<OrderModel>>> GetList(GridDataBound dataGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<OrderModel>> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        Task<Result<OrderModel>> Add(OrderModel orderModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        Task<Result<OrderModel>> Update(OrderModel orderModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        Task<Result<OrderModel>> UpdateState(OrderModel orderModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Result<List<OrderStatusModel>>> GetAllOrderStatus();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Result<List<ShippingStatusModel>>> GetAllShippingStatus();

    }
}
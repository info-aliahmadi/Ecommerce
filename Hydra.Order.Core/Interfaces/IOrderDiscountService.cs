using Hydra.Kernel.GeneralModels;

using Hydra.Order.Core.Models;

namespace Hydra.Order.Core.Interfaces
{
    public interface IOrderDiscountService
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<PaginatedList<OrderDiscountModel>>> GetList(GridDataBound dataGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<OrderDiscountModel>> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderDiscountModel"></param>
        /// <returns></returns>
        Task<Result<OrderDiscountModel>> Add(OrderDiscountModel orderDiscountModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderDiscountModel"></param>
        /// <returns></returns>
        Task<Result<OrderDiscountModel>> Update(OrderDiscountModel orderDiscountModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

    }
}
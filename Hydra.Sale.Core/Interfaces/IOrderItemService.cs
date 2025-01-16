﻿using Hydra.Infrastructure.GeneralModels;

using Hydra.Sale.Core.Models;

namespace Hydra.Sale.Core.Interfaces
{
    public interface IOrderItemService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<Result<Tuple<List<OrderItemModel>, SumOrderItemsModel>>> GetListByOrderId(int orderId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<OrderItemModel>> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderItemModel"></param>
        /// <returns></returns>
        Task<Result<OrderItemModel>> Add(OrderItemModel orderItemModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderItemModel"></param>
        /// <returns></returns>
        Task<Result<OrderItemModel>> Update(OrderItemModel orderItemModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<SumOrderItemsModel> SumAmountOrderItemsByOrderId(int orderId);
    }
}
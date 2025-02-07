﻿using Hydra.Kernel.GeneralModels;
using Hydra.Order.Core.Models;

namespace Hydra.Order.Core.Interfaces
{
    public interface IShippingMethodService
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<PaginatedList<ShippingMethodModel>>> GetList(GridDataBound dataGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<ShippingMethodModel>> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="shippingMethodModel"></param>
        /// <returns></returns>
        Task<Result<ShippingMethodModel>> Add(ShippingMethodModel shippingMethodModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="shippingMethodModel"></param>
        /// <returns></returns>
        Task<Result<ShippingMethodModel>> Update(ShippingMethodModel shippingMethodModel);

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
        Task<Result<List<ShippingMethodPairModel>>> GetAllShippingMethods();
    }
}
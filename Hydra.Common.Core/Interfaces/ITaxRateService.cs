﻿using Hydra.Kernel.GeneralModels;

using Hydra.Common.Core.Models;

namespace Hydra.Common.Core.Interfaces
{
    public interface ITaxRateService
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<PaginatedList<TaxRateModel>>> GetList(GridDataBound dataGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<TaxRateModel>> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="taxRateModel"></param>
        /// <returns></returns>
        Task<Result<TaxRateModel>> Add(TaxRateModel taxRateModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="taxRateModel"></param>
        /// <returns></returns>
        Task<Result<TaxRateModel>> Update(TaxRateModel taxRateModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

    }
}
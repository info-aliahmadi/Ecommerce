﻿using Hydra.Infrastructure.GeneralModels;

using Hydra.Sale.Core.Models;

namespace Hydra.Sale.Core.Interfaces
{
    public interface ITaxCategoryService
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<List<TaxCategoryModel>>> GetList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<TaxCategoryModel>> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="taxCategoryModel"></param>
        /// <returns></returns>
        Task<Result<TaxCategoryModel>> Add(TaxCategoryModel taxCategoryModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="taxCategoryModel"></param>
        /// <returns></returns>
        Task<Result<TaxCategoryModel>> Update(TaxCategoryModel taxCategoryModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

    }
}
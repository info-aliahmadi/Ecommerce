﻿using Hydra.Infrastructure.Data.Extension;
using Hydra.Infrastructure.GeneralModels;
using Hydra.Sale.Core.Models;

namespace Hydra.Sale.Core.Interfaces
{
    public interface ICountryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<string> GetCountrySeed();

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<PaginatedList<CountryModel>>> GetList(GridDataBound dataGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<CountryModel>> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="countryModel"></param>
        /// <returns></returns>
        Task<Result<CountryModel>> Add(CountryModel countryModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="countryModel"></param>
        /// <returns></returns>
        Task<Result<CountryModel>> Update(CountryModel countryModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

    }
}
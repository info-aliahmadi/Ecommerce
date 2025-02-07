﻿using Hydra.Kernel.Extension;
using Hydra.Kernel.GeneralModels;
using Hydra.Common.Core.Models;

namespace Hydra.Common.Core.Interfaces
{
    public interface IAddressService
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<PaginatedList<AddressModel>>> GetList(GridDataBound dataGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<AddressModel>> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="addressModel"></param>
        /// <returns></returns>
        Task<Result<AddressModel>> Add(AddressModel addressModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="addressModel"></param>
        /// <returns></returns>
        Task<Result<AddressModel>> Update(AddressModel addressModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

    }
}
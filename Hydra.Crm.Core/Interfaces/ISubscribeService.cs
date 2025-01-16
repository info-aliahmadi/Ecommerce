﻿using Hydra.Crm.Core.Models.Subscribe;
using Hydra.Infrastructure.Data.Extension;
using Hydra.Infrastructure.GeneralModels;


namespace Hydra.Crm.Core.Interfaces
{
    public interface ISubscribeService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<PaginatedList<SubscribeModel>>> GetList(GridDataBound dataGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<SubscribeModel>> GetById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscribeModel"></param>
        /// <returns></returns>
        Task<Result<SubscribeModel>> Add(SubscribeModel subscribeModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscribeModel"></param>
        /// <returns></returns>
        Task<Result<SubscribeModel>> Update(SubscribeModel subscribeModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(long id);
    }
}
﻿using Hydra.Cms.Core.Models;
using Hydra.Infrastructure.Data.Extension;
using Hydra.Infrastructure.GeneralModels;


namespace Hydra.Cms.Core.Interfaces
{
    public interface ITagService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<PaginatedList<TagModel>>> GetList(GridDataBound dataGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        Task<Result<List<TagModel>>> GetListByTitle(string[] tags);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Result<List<TagModel>>> GetListForSelect();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Result<List<TagModel>>> GetAllList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<TagModel>> GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        Task<Result<TagModel>> GetByTitle(string title);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagModel"></param>
        /// <returns></returns>
        Task<Result<TagModel>> Add(TagModel tagModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        Task<Result> Add(string[] tags);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagModel"></param>
        /// <returns></returns>
        Task<Result<TagModel>> Update(TagModel tagModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

    }
}

﻿using Hydra.Cms.Core.Models;
using Hydra.Infrastructure.GeneralModels;


namespace Hydra.Cms.Core.Interfaces
{
    public interface ITopicService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Result<List<TopicModel>>> GetHierarchy();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Result<List<TopicModel>>> GetListForSelect();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Result<List<TopicModel>>> GetList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<TopicModel>> GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        Task<Result<TopicModel>> GetByTitle(string title);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicModel"></param>
        /// <returns></returns>
        Task<Result<TopicModel>> Add(TopicModel topicModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicModel"></param>
        /// <returns></returns>
        Task<Result<TopicModel>> Update(TopicModel topicModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);


    }
}

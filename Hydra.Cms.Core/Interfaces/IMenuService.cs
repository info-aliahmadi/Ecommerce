﻿using Hydra.Cms.Core.Models;
using Hydra.Infrastructure.GeneralModels;


namespace Hydra.Cms.Core.Interfaces
{
    public interface IMenuService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Result<List<MenuModel>>> GetMenu();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Result<List<MenuModel>>> GetHierarchy();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<MenuModel>> GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        Task<Result<MenuModel>> Add(MenuModel menuModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        Task<Result<MenuModel>> Update(MenuModel menuModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuModelList"></param>
        /// <returns></returns>
        Task<Result<List<MenuModel>>> UpdateOrder(List<MenuModel> menuModelList);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);


    }
}

using Hydra.Kernel.Extension;
using Hydra.Kernel.GeneralModels;

using Hydra.Product.Core.Models;

namespace Hydra.Product.Core.Interfaces
{
    public interface IManufacturerService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<List<ManufacturerModel>>> GetManufacturersList();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Result<List<ManufacturerModel>>> GetListForSelect();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<ManufacturerModel> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="manufacturerModel"></param>
        /// <returns></returns>
        Task<Result<ManufacturerModel>> Add(ManufacturerModel manufacturerModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="manufacturerModel"></param>
        /// <returns></returns>
        Task<Result<ManufacturerModel>> Update(ManufacturerModel manufacturerModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linkModelList"></param>
        /// <returns></returns>
        Task<Result<List<ManufacturerModel>>> UpdateOrder(List<ManufacturerModel> linkModelList);
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

    }
}
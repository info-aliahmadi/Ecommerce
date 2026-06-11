using Hydra.Common.Core.Models;
using Hydra.Kernel.GeneralModels;

namespace Hydra.Common.Core.Interfaces
{
    public interface IShippingMethodService
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<List<ShippingMethodModel>>> GetList();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Result<List<ShippingMethodModel>>> GetShippingMethodListForSelect();

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

    }
}
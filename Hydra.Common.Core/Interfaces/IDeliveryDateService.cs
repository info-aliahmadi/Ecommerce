using Hydra.Kernel.GeneralModels;

using Hydra.Common.Core.Models;

namespace Hydra.Common.Core.Interfaces
{
    public interface IDeliveryDateService
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<List<DeliveryDateModel>>> GetList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<DeliveryDateModel>> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="deliveryDateModel"></param>
        /// <returns></returns>
        Task<Result<DeliveryDateModel>> Add(DeliveryDateModel deliveryDateModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="deliveryDateModel"></param>
        /// <returns></returns>
        Task<Result<DeliveryDateModel>> Update(DeliveryDateModel deliveryDateModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

    }
}
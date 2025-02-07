using Hydra.Kernel.GeneralModels;

using Hydra.Inventory.Core.Models;

namespace Hydra.Inventory.Core.Interfaces
{
    public interface IInventoryService
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<PaginatedList<InventoryModel>>> GetList(GridDataBound dataGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<InventoryModel>> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="inventoryModel"></param>
        /// <returns></returns>
        Task<Result<InventoryModel>> Add(InventoryModel inventoryModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="inventoryModel"></param>
        /// <returns></returns>
        Task<Result<InventoryModel>> Update(InventoryModel inventoryModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

    }
}
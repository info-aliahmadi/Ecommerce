using Hydra.Inventory.Core.Models;
using Hydra.Kernel.GeneralModels;

namespace Hydra.Inventory.Core.Interfaces
{
    public interface IVariantService
    {
        Task<Result<PaginatedList<VariantModel>>> GetList(GridDataBound dataGrid);

        Task<Result<List<VariantModel>>> GetListByProductId(int productId);

        Task<Result<VariantModel>> GetById(int id);

        Task<Result<VariantModel>> Add(VariantModel model);

        Task<Result<VariantModel>> Update(VariantModel model);

        Task<Result> Delete(int id);

        Task<Result> UpdateStock(StockAdjustmentModel model);

        Task<Result<List<InventoryTransactionModel>>> GetInventoryTransactions(int variantId);
    }
}

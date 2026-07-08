using Hydra.Kernel.GeneralModels;
using Hydra.Product.Core.Models;

namespace Hydra.Product.Core.Interfaces
{
    public interface IBundleService
    {
        Task<Result<List<BundleDisplayModel>>> GetPublishedBundles();
        Task<Result<List<BundleModel>>> GetBundlesList();
        Task<Result<List<BundleModel>>> GetListForSelect();
        Task<Result<BundleModel>> GetById(int id);
        Task<Result<BundleModel>> Add(BundleModel bundleModel);
        Task<Result<BundleModel>> Update(BundleModel bundleModel);
        Task<Result<List<BundleModel>>> UpdateOrder(List<BundleModel> linkModelList);
        Task<Result> Delete(int id);
    }
}

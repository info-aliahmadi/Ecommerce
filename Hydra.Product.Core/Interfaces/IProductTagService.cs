using Hydra.Kernel.GeneralModels;

using Hydra.Product.Core.Models;

namespace Hydra.Product.Core.Interfaces
{
    public interface IProductTagService
    {
        /// <summary>
        /// Retrieves a list of published product tags.
        /// </summary>
        /// <returns>A <see cref="Result{T}"/> containing a list of <see cref="ProductTagModel"/> objects representing the
        /// published product tags. If no tags are published, the list will be empty. The result includes status
        /// information about the operation.</returns>
        Result<List<ProductTagModel>> GetPublishedList();

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<PaginatedList<ProductTagModel>>> GetList(GridDataBound dataGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Result<List<ProductTagModel>>> GetListForSelect();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<ProductTagModel>> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="productTagModel"></param>
        /// <returns></returns>
        Task<Result<ProductTagModel>> Add(ProductTagModel productTagModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        Task<Result<List<ProductTagModel>>> Add(string[] tags);

        /// <summary>
        ///
        /// </summary>
        /// <param name="productTagModel"></param>
        /// <returns></returns>
        Task<Result<ProductTagModel>> Update(ProductTagModel productTagModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

    }
}
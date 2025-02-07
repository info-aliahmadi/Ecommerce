using Hydra.Kernel.GeneralModels;

using Hydra.Product.Core.Models;

namespace Hydra.Product.Core.Interfaces
{
    public interface IProductReviewHelpfulnessService
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<PaginatedList<ProductReviewHelpfulnessModel>>> GetList(GridDataBound dataGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<ProductReviewHelpfulnessModel>> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="productReviewHelpfulnessModel"></param>
        /// <returns></returns>
        Task<Result<ProductReviewHelpfulnessModel>> Add(ProductReviewHelpfulnessModel productReviewHelpfulnessModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="productReviewHelpfulnessModel"></param>
        /// <returns></returns>
        Task<Result<ProductReviewHelpfulnessModel>> Update(ProductReviewHelpfulnessModel productReviewHelpfulnessModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

    }
}
using Hydra.Kernel.GeneralModels;

using Hydra.Product.Core.Models;

namespace Hydra.Product.Core.Interfaces
{
    public interface IProductService
    {

        /// <summary>
        /// Retrieves a paginated list of products that match the specified filter criteria.
        /// </summary>
        /// <param name="productFilter">An object containing the criteria used to filter and paginate the list of products. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a Result object with a
        /// PaginatedList of ProductModel instances that match the filter. The list may be empty if no products meet the
        /// criteria.</returns>
        Task<Result<PaginatedList<ProductDisplayModel>>> GetPublishedProducts(ProductFilterDisplayModel productFilter);

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<PaginatedList<ProductModel>>> GetList(GridDataBound dataGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<ProductModel>> GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<Result<List<ProductModel>>> GetByIds(int[] ids);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<Result<List<ProductModel>>> GetProductsByInput(string input);

        /// <summary>
        ///
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        Task<Result<ProductModel>> Add(ProductModel productModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        Task<Result<ProductModel>> Update(ProductModel productModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Remove(int id);

        /// <summary>
        /// Retrieves published products grouped by featured Style attributes.
        /// Returns up to 5 products per attribute.
        /// </summary>
        Task<Result<List<CuratedProductGroupModel>>> GetPublishedCuratedProducts();

    }
}
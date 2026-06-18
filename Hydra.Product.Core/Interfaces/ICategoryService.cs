using Hydra.Kernel.GeneralModels;

using Hydra.Product.Core.Models;

namespace Hydra.Product.Core.Interfaces
{
    public interface ICategoryService
    {

        /// <summary>
        /// Retrieves a list of all published categories.
        /// </summary>
        /// <returns>A <see cref="Result{T}"/> containing a list of <see cref="CategoryModel"/> objects representing the
        /// published categories. If no categories are published, the list will be empty.</returns>
        Result<List<CategoryModel>> GetPublishedCategories();
        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Result<List<CategoryModel>> GetList();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Result<List<CategoryModel>> GetHierarchy();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Result<List<CategoryModel>> GetListForSelect();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<CategoryModel> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryModel"></param>
        /// <returns></returns>
        Task<Result<CategoryModel>> Add(CategoryModel categoryModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryModel"></param>
        /// <returns></returns>
        Task<Result<CategoryModel>> Update(CategoryModel categoryModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linkModelList"></param>
        /// <returns></returns>
        Task<Result<List<CategoryModel>>> UpdateOrder(List<CategoryModel> linkModelList);
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

    }
}
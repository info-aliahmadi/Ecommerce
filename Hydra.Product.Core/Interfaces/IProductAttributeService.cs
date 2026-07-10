using Hydra.Ecommerce.Core.Domain;
using Hydra.Ecommerce.Core.Enums;
using Hydra.Kernel.GeneralModels;

using Hydra.Product.Core.Models;

namespace Hydra.Product.Core.Interfaces
{
    public interface IProductAttributeService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeTypes"></param>
        /// <returns></returns>
        Result<List<ProductAttributeModel>> GetPublishedAttributeByAttributeTypesList(AttributeType[] attributeTypes);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Result<List<ProductAttributeModel>> GetPublishedProductAttributesList();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Result<List<ProductAttributeModel>> GetList();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Result<List<ProductAttributeModel>> GetListForSelect();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<ProductAttributeModel> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="productAttributeModel"></param>
        /// <returns></returns>
        Task<Result<ProductAttributeModel>> Add(ProductAttributeModel productAttributeModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="productAttributeModel"></param>
        /// <returns></returns>
        Task<Result<ProductAttributeModel>> Update(ProductAttributeModel productAttributeModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

    }
}
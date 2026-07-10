using Hydra.Kernel.GeneralModels;

using Hydra.Product.Core.Models;

namespace Hydra.Product.Core.Interfaces
{
    public interface IProductService
    {

        /// <summary>
        /// Retrieves a paginated list of published products that match the specified filter criteria.
        /// This is used by customer-facing endpoints and only returns products that are published and not deleted.
        /// </summary>
        /// <param name="productFilter">An object containing the criteria used to filter and paginate the list of products. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a Result object with a
        /// PaginatedList of ProductDisplayModel instances that match the filter. The list may be empty if no products meet the
        /// criteria.</returns>
        Task<Result<PaginatedList<ProductDisplayModel>>> GetPublishedProducts(ProductFilterDisplayModel productFilter);

        /// <summary>
        /// Retrieves an admin paginated list of products (all products except deleted ones) for grid listing.
        /// </summary>
        /// <param name="dataGrid">Grid and paging parameters.</param>
        /// <returns>Paginated list of ProductModel for admin listing.</returns>
        Task<Result<PaginatedList<ProductModel>>> GetList(GridDataBound dataGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<ProductDisplayModel>> GetPublishedProductById(int id);
        /// <summary>
        /// Retrieves a single product by identifier, including related data (categories, images, attributes, tags, inventories).
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <returns>ProductModel with related data.</returns>
        Task<Result<ProductModel>> GetById(int id);

        /// <summary>
        /// Retrieves a list of products by their identifiers. Returns minimal information suitable for selects/autocomplete.
        /// </summary>
        /// <param name="ids">Array of product ids.</param>
        /// <returns>List of ProductModel entries with minimal info.</returns>
        Task<Result<List<ProductModel>>> GetByIds(int[] ids);

        /// <summary>
        /// Searches products by a free-text input and returns up to 10 matching products for autocomplete/select.
        /// </summary>
        /// <param name="input">Search input.</param>
        /// <returns>List of matching ProductModel entries.</returns>
        Task<Result<List<ProductModel>>> GetProductsByInput(string input);

        /// <summary>
        /// Adds a new product and related mappings (categories, manufacturers, images, attributes, related products, tags, inventories).
        /// Validates input and returns validation errors when present.
        /// </summary>
        /// <param name="productModel">Product model to add.</param>
        /// <returns>Result containing the created ProductModel or validation errors.</returns>
        Task<Result<ProductModel>> Add(ProductModel productModel);

        /// <summary>
        /// Updates an existing product and its related mappings. Performs updates using helper methods for each relation.
        /// </summary>
        /// <param name="productModel">Product model with updated data.</param>
        /// <returns>Result containing the updated ProductModel or validation errors.</returns>
        Task<Result<ProductModel>> Update(ProductModel productModel);

        /// <summary>
        /// Marks the specified product as deleted by setting its deleted flag (soft delete).
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        /// <returns>Operation result.</returns>
        Task<Result> Delete(int id);

        /// <summary>
        /// Removes the product with the specified identifier from the data store (permanent delete).
        /// </summary>
        /// <param name="id">The unique identifier of the product to remove.</param>
        /// <returns>Operation result.</returns>
        Task<Result> Remove(int id);

        /// <summary>
        /// Retrieves published products grouped by featured Style attributes.
        /// Returns up to 5 products per attribute.
        /// </summary>
        /// <returns>List of curated product groups.</returns>
        Task<Result<List<CuratedStyleProductModel>>> GetPublishedCuratedStyleProducts();

    }
}
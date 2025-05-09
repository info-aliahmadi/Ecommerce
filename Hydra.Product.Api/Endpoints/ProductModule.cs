using Hydra.Ecommerce.Core.Constants;
using Hydra.Infrastructure.ModuleExtension;
using Hydra.Infrastructure.Security.Extension;
using Hydra.Product.Api.Handler;
using Hydra.Product.Api.Services;
using Hydra.Product.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.Product.Api.Endpoints
{
    public class ProductModule : IModule
    {
        private const string API_SCHEMA = "/Product";
        public IServiceCollection RegisterModules(IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IManufacturerService, ManufacturerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductAttributeService, ProductAttributeService>();
            services.AddScoped<IProductReviewService, ProductReviewService>();
            services.AddScoped<IProductReviewHelpfulnessService, ProductReviewHelpfulnessService>();
            services.AddScoped<IProductTagService, ProductTagService>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(API_SCHEMA + "/GetCategoryList", CategoryHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_CATEGORY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/GetCategoryListForSelect", CategoryHandler.GetListForSelect).RequirePermission(EcommercePermissionTypes.SALE_CATEGORY_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetCategoryHierarchy", CategoryHandler.GetCategoryHierarchy).RequirePermission(EcommercePermissionTypes.SALE_CATEGORY_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetCategoryById", CategoryHandler.GetCategoryById).RequirePermission(EcommercePermissionTypes.SALE_CATEGORY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddCategory", CategoryHandler.AddCategory).RequirePermission(EcommercePermissionTypes.SALE_CATEGORY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateCategory", CategoryHandler.UpdateCategory).RequirePermission(EcommercePermissionTypes.SALE_CATEGORY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateCategoryOrders", CategoryHandler.UpdateOrders).RequirePermission(EcommercePermissionTypes.SALE_CATEGORY_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteCategory", CategoryHandler.DeleteCategory).RequirePermission(EcommercePermissionTypes.SALE_CATEGORY_MANAGEMENT);


            endpoints.MapPost(API_SCHEMA + "/GetProductAttributeList", ProductAttributeHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_ATTRIBUTE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/GetProductAttributesForSelect", ProductAttributeHandler.GetListForSelect).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_ATTRIBUTE_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetProductAttributeById", ProductAttributeHandler.GetProductAttributeById).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_ATTRIBUTE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddProductAttribute", ProductAttributeHandler.AddProductAttribute).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_ATTRIBUTE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateProductAttribute", ProductAttributeHandler.UpdateProductAttribute).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_ATTRIBUTE_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteProductAttribute", ProductAttributeHandler.DeleteProductAttribute).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_ATTRIBUTE_MANAGEMENT);


            endpoints.MapPost(API_SCHEMA + "/GetProductList", ProductHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetProductById", ProductHandler.GetProductById).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetProductsByIds", ProductHandler.GetProductsByIds).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetProductsByInput", ProductHandler.GetProductsByInput).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddProduct", ProductHandler.AddProduct).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateProduct", ProductHandler.UpdateProduct).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteProduct", ProductHandler.DeleteProduct).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/RemoveProduct", ProductHandler.RemoveProduct).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_MANAGEMENT);


            endpoints.MapPost(API_SCHEMA + "/GetProductReviewList", ProductReviewHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_REVIEW_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetProductReviewById", ProductReviewHandler.GetProductReviewById).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_REVIEW_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddProductReview", ProductReviewHandler.AddProductReview).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_REVIEW_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateProductReview", ProductReviewHandler.UpdateProductReview).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_REVIEW_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteProductReview", ProductReviewHandler.DeleteProductReview).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_REVIEW_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetProductReviewHelpfulnessList", ProductReviewHelpfulnessHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_REVIEW_HELPFULNESS_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetProductReviewHelpfulnessById", ProductReviewHelpfulnessHandler.GetProductReviewHelpfulnessById).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_REVIEW_HELPFULNESS_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddProductReviewHelpfulness", ProductReviewHelpfulnessHandler.AddProductReviewHelpfulness).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_REVIEW_HELPFULNESS_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateProductReviewHelpfulness", ProductReviewHelpfulnessHandler.UpdateProductReviewHelpfulness).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_REVIEW_HELPFULNESS_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteProductReviewHelpfulness", ProductReviewHelpfulnessHandler.DeleteProductReviewHelpfulness).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_REVIEW_HELPFULNESS_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetProductTagList", ProductTagHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_TAG_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/GetProductTagListForSelect", ProductTagHandler.GetListForSelect).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_TAG_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetProductTagById", ProductTagHandler.GetProductTagById).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_TAG_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddProductTag", ProductTagHandler.AddProductTag).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_TAG_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateProductTag", ProductTagHandler.UpdateProductTag).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_TAG_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteProductTag", ProductTagHandler.DeleteProductTag).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_TAG_MANAGEMENT);



            endpoints.MapPost(API_SCHEMA + "/GetManufacturerList", ManufacturerHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_MANUFACTURER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/GetManufacturersForSelect", ManufacturerHandler.GetListForSelect).RequirePermission(EcommercePermissionTypes.SALE_MANUFACTURER_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetManufacturerById", ManufacturerHandler.GetManufacturerById).RequirePermission(EcommercePermissionTypes.SALE_MANUFACTURER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddManufacturer", ManufacturerHandler.AddManufacturer).RequirePermission(EcommercePermissionTypes.SALE_MANUFACTURER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateManufacturer", ManufacturerHandler.UpdateManufacturer).RequirePermission(EcommercePermissionTypes.SALE_MANUFACTURER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateManufacturerOrders", ManufacturerHandler.UpdateOrders).RequirePermission(EcommercePermissionTypes.SALE_MANUFACTURER_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteManufacturer", ManufacturerHandler.DeleteManufacturer).RequirePermission(EcommercePermissionTypes.SALE_MANUFACTURER_MANAGEMENT);

            return endpoints;
        }

    }
}
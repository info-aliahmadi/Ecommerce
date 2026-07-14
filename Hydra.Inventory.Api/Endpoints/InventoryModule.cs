using Hydra.Ecommerce.Core.Constants;
using Hydra.Infrastructure.ModuleExtension;
using Hydra.Infrastructure.Security.Extension;
using Hydra.Inventory.Api.Handler;
using Hydra.Inventory.Api.Services;
using Hydra.Inventory.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.Inventory.Api.Endpoints
{
    public class VariantModule : IModule
    {
        private const string API_SCHEMA = "/Variant";

        public IServiceCollection RegisterModules(IServiceCollection services)
        {
            services.AddScoped<IVariantService, VariantService>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(API_SCHEMA + "/GetVariantList", VariantHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_VARIANT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetVariantsByProductId", VariantHandler.GetListByProductId).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_VARIANT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetVariantById", VariantHandler.GetVariantById).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_VARIANT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddVariant", VariantHandler.AddVariant).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_VARIANT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateVariant", VariantHandler.UpdateVariant).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_VARIANT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteVariant", VariantHandler.DeleteVariant).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_VARIANT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateStock", VariantHandler.UpdateStock).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_VARIANT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetInventoryTransactions", VariantHandler.GetInventoryTransactions).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_VARIANT_MANAGEMENT);

            return endpoints;
        }
    }
}

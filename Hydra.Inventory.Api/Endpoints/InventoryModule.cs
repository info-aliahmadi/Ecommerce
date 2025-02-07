using Hydra.Ecommerce.Core.Constants;
using Hydra.Infrastructure.ModuleExtension;
using Hydra.Infrastructure.Security.Extension;
using Hydra.Inventory.Core.Interfaces;
using Hydra.Product.Api.Handler;
using Hydra.Product.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.Product.Api.Endpoints
{
    public class InventoryModule : IModule
    {
        private const string API_SCHEMA = "/Product";
        public IServiceCollection RegisterModules(IServiceCollection services)
        {
            services.AddScoped<IInventoryService, InventoryService>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {

            endpoints.MapPost(API_SCHEMA + "/GetProductInventoryList", InventoryHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_INVENTORY_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetProductInventoryById", InventoryHandler.GetProductInventoryById).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_INVENTORY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddProductInventory", InventoryHandler.AddProductInventory).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_INVENTORY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateProductInventory", InventoryHandler.UpdateProductInventory).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_INVENTORY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteProductInventory", InventoryHandler.DeleteProductInventory).RequirePermission(EcommercePermissionTypes.SALE_PRODUCT_INVENTORY_MANAGEMENT);

            return endpoints;
        }

    }
}
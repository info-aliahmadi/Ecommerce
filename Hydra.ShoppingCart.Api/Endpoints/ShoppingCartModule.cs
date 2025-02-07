using Hydra.Ecommerce.Core.Constants;
using Hydra.Infrastructure.ModuleExtension;
using Hydra.Infrastructure.Security.Extension;
using Hydra.Sale.Api.Handler;
using Hydra.ShoppingCart.Api.Services;
using Hydra.ShoppingCart.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.ShoppingCart.Api.Endpoints
{
    public class ShoppingCartModule : IModule
    {
        private const string API_SCHEMA = "/ShoppingCart";
        public IServiceCollection RegisterModules(IServiceCollection services)
        {
            services.AddScoped<IShoppingCartItemService, ShoppingCartItemService>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {

            endpoints.MapPost(API_SCHEMA + "/GetShoppingCartItemList", ShoppingCartItemHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetShoppingCartItemById", ShoppingCartItemHandler.GetShoppingCartItemById).RequirePermission(EcommercePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddShoppingCartItem", ShoppingCartItemHandler.AddShoppingCartItem).RequirePermission(EcommercePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateShoppingCartItem", ShoppingCartItemHandler.UpdateShoppingCartItem).RequirePermission(EcommercePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteShoppingCartItem", ShoppingCartItemHandler.DeleteShoppingCartItem).RequirePermission(EcommercePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT);

            return endpoints;
        }

    }
}
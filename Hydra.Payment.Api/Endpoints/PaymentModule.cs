using Hydra.Ecommerce.Core.Constants;
using Hydra.Infrastructure.ModuleExtension;
using Hydra.Infrastructure.Security.Extension;
using Hydra.Payment.Api.Handler;
using Hydra.Payment.Api.Services;
using Hydra.Payment.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.Payment.Api.Endpoints
{
    public class PaymentModule : IModule
    {
        private const string API_SCHEMA = "/Payment";
        public IServiceCollection RegisterModules(IServiceCollection services)
        {

            services.AddScoped<IPaymentService, PaymentService>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
          
            endpoints.MapGet(API_SCHEMA + "/GetOrderPaymentById", PaymentHandler.GetOrderPaymentById).RequirePermission(EcommercePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/GetPaymentList", PaymentHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_PAYMENT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetPaymentById", PaymentHandler.GetPaymentById).RequirePermission(EcommercePermissionTypes.SALE_PAYMENT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetAllPaymentStatus", PaymentHandler.GetAllPaymentStatus).RequirePermission(EcommercePermissionTypes.SALE_PAYMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddPayment", PaymentHandler.AddPayment).RequirePermission(EcommercePermissionTypes.SALE_PAYMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdatePayment", PaymentHandler.UpdatePayment).RequirePermission(EcommercePermissionTypes.SALE_PAYMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeletePayment", PaymentHandler.DeletePayment).RequirePermission(EcommercePermissionTypes.SALE_PAYMENT_MANAGEMENT);

            return endpoints;
        }

    }
}
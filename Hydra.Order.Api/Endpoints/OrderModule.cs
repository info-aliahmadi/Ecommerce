using Hydra.Ecommerce.Core.Constants;
using Hydra.Infrastructure.ModuleExtension;
using Hydra.Infrastructure.Security.Extension;
using Hydra.Order.Api.Handler;
using Hydra.Order.Api.Services;
using Hydra.Order.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.Order.Api.Endpoints
{
    public class OrderModule : IModule
    {
        private const string API_SCHEMA = "/Order";
        public IServiceCollection RegisterModules(IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDiscountService, OrderDiscountService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IOrderNoteService, OrderNoteService>();
            services.AddScoped<IShipmentService, ShipmentService>();
            services.AddScoped<IShipmentItemService, ShipmentItemService>();
            services.AddScoped<IShippingMethodService, ShippingMethodService>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {

            endpoints.MapPost(API_SCHEMA + "/GetOrderList", OrderHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetOrderById", OrderHandler.GetOrderById).RequirePermission(EcommercePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetAllOrderStatus", OrderHandler.GetAllOrderStatus).RequirePermission(EcommercePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetAllShippingStatus", OrderHandler.GetAllShippingStatus).RequirePermission(EcommercePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddOrder", OrderHandler.AddOrder).RequirePermission(EcommercePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateOrder", OrderHandler.UpdateOrder).RequirePermission(EcommercePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateOrderState", OrderHandler.UpdateOrderState).RequirePermission(EcommercePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteOrder", OrderHandler.DeleteOrder).RequirePermission(EcommercePermissionTypes.SALE_ORDER_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetOrderDiscountList", OrderDiscountHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_ORDER_DISCOUNT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetOrderDiscountById", OrderDiscountHandler.GetOrderDiscountById).RequirePermission(EcommercePermissionTypes.SALE_ORDER_DISCOUNT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddOrderDiscount", OrderDiscountHandler.AddOrderDiscount).RequirePermission(EcommercePermissionTypes.SALE_ORDER_DISCOUNT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateOrderDiscount", OrderDiscountHandler.UpdateOrderDiscount).RequirePermission(EcommercePermissionTypes.SALE_ORDER_DISCOUNT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteOrderDiscount", OrderDiscountHandler.DeleteOrderDiscount).RequirePermission(EcommercePermissionTypes.SALE_ORDER_DISCOUNT_MANAGEMENT);

            endpoints.MapGet(API_SCHEMA + "/GetOrderItemList", OrderItemHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_ORDER_ITEM_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetOrderItemById", OrderItemHandler.GetOrderItemById).RequirePermission(EcommercePermissionTypes.SALE_ORDER_ITEM_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddOrderItem", OrderItemHandler.AddOrderItem).RequirePermission(EcommercePermissionTypes.SALE_ORDER_ITEM_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateOrderItem", OrderItemHandler.UpdateOrderItem).RequirePermission(EcommercePermissionTypes.SALE_ORDER_ITEM_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteOrderItem", OrderItemHandler.DeleteOrderItem).RequirePermission(EcommercePermissionTypes.SALE_ORDER_ITEM_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetOrderNoteList", OrderNoteHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_ORDERNOTE_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetOrderNoteById", OrderNoteHandler.GetOrderNoteById).RequirePermission(EcommercePermissionTypes.SALE_ORDERNOTE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddOrderNote", OrderNoteHandler.AddOrderNote).RequirePermission(EcommercePermissionTypes.SALE_ORDERNOTE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateOrderNote", OrderNoteHandler.UpdateOrderNote).RequirePermission(EcommercePermissionTypes.SALE_ORDERNOTE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteOrderNote", OrderNoteHandler.DeleteOrderNote).RequirePermission(EcommercePermissionTypes.SALE_ORDERNOTE_MANAGEMENT);


            endpoints.MapPost(API_SCHEMA + "/GetShipmentList", ShipmentHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetShipmentById", ShipmentHandler.GetShipmentById).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddShipment", ShipmentHandler.AddShipment).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateShipment", ShipmentHandler.UpdateShipment).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteShipment", ShipmentHandler.DeleteShipment).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetShipmentItemList", ShipmentItemHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetShipmentItemById", ShipmentItemHandler.GetShipmentItemById).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddShipmentItem", ShipmentItemHandler.AddShipmentItem).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateShipmentItem", ShipmentItemHandler.UpdateShipmentItem).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteShipmentItem", ShipmentItemHandler.DeleteShipmentItem).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_MANAGEMENT);



            endpoints.MapPost(API_SCHEMA + "/GetShippingMethodList", ShippingMethodHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetAllShippingMethods", ShippingMethodHandler.GetAllShippingMethods).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetShippingMethodById", ShippingMethodHandler.GetShippingMethodById).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddShippingMethod", ShippingMethodHandler.AddShippingMethod).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateShippingMethod", ShippingMethodHandler.UpdateShippingMethod).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteShippingMethod", ShippingMethodHandler.DeleteShippingMethod).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);


            return endpoints;
        }

    }
}
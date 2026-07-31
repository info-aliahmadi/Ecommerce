using Hydra.Ecommerce.Core.Constants;
using Hydra.Infrastructure.ModuleExtension;
using Hydra.Infrastructure.Security.Extension;
using Hydra.Order.Api.Handler;
using Hydra.Order.Api.Services;
using Hydra.Order.Core.Interfaces;
using Hydra.Payment.Api.Services;
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
            services.AddScoped<IShoppingCartItemService, ShoppingCartItemService>();
            services.AddScoped<IPaymentService, PaymentService>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {

            // User-facing shopping cart & wishlist endpoints (authenticated, user operates on own data)
            endpoints.MapGet(API_SCHEMA + "/GetMyCartItems", ShoppingCartItemHandler.GetMyCartItems).RequireAuthorization();
            endpoints.MapGet(API_SCHEMA + "/GetMyWishlistItems", ShoppingCartItemHandler.GetMyWishlistItems).RequireAuthorization();
            endpoints.MapGet(API_SCHEMA + "/GetAllMyShoppingItems", ShoppingCartItemHandler.GetAllMyShoppingItems).RequireAuthorization();
            endpoints.MapPost(API_SCHEMA + "/AddToCart", ShoppingCartItemHandler.AddToCart).RequireAuthorization();
            endpoints.MapPost(API_SCHEMA + "/AddToWishlist", ShoppingCartItemHandler.AddToWishlist).RequireAuthorization();
            endpoints.MapPost(API_SCHEMA + "/RemoveFromCart", ShoppingCartItemHandler.RemoveFromCart).RequireAuthorization();
            endpoints.MapPost(API_SCHEMA + "/RemoveFromWishlist", ShoppingCartItemHandler.RemoveFromWishlist).RequireAuthorization();
            endpoints.MapPost(API_SCHEMA + "/ClearCart", ShoppingCartItemHandler.ClearCart).RequireAuthorization();
            endpoints.MapPost(API_SCHEMA + "/ClearWishlist", ShoppingCartItemHandler.ClearWishlist).RequireAuthorization();
            endpoints.MapPost(API_SCHEMA + "/UpdateCartItemQuantity", ShoppingCartItemHandler.UpdateCartItemQuantity).RequireAuthorization();

            // User-facing order endpoints
            endpoints.MapGet(API_SCHEMA + "/GetMyOrders", OrderHandler.GetMyOrders).RequireAuthorization();
            endpoints.MapGet(API_SCHEMA + "/GetMyOrderById", OrderHandler.GetMyOrderById).RequireAuthorization();
            endpoints.MapGet(API_SCHEMA + "/GetMyOrderItems", OrderHandler.GetMyOrderItems).RequireAuthorization();
            endpoints.MapPost(API_SCHEMA + "/CreateOrder", OrderHandler.CreateOrder).RequireAuthorization();
            endpoints.MapPost(API_SCHEMA + "/ConfirmOrder", OrderHandler.ConfirmOrder).RequireAuthorization();
            endpoints.MapPost(API_SCHEMA + "/CancelMyOrder", OrderHandler.CancelMyOrder).RequireAuthorization();

            // User-facing payment endpoints
            endpoints.MapGet(API_SCHEMA + "/GetMyPayments", PaymentHandler.GetMyPayments).RequireAuthorization();
            endpoints.MapGet(API_SCHEMA + "/GetMyPaymentById", PaymentHandler.GetMyPaymentById).RequireAuthorization();
            endpoints.MapPost(API_SCHEMA + "/ProcessPayment", PaymentHandler.ProcessPayment).RequireAuthorization();




            endpoints.MapPost(API_SCHEMA + "/GetOrderList", OrderHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetOrderById", OrderHandler.GetOrderById).RequirePermission(EcommercePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetAllOrderStatus", OrderHandler.GetAllOrderStatus).RequirePermission(EcommercePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetAllShippingStatus", OrderHandler.GetAllShippingStatus).RequirePermission(EcommercePermissionTypes.SALE_ORDER_MANAGEMENT);
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


            endpoints.MapPost(API_SCHEMA + "/GetShoppingCartItemList", ShoppingCartItemHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetShoppingCartItemById", ShoppingCartItemHandler.GetShoppingCartItemById).RequirePermission(EcommercePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddShoppingCartItem", ShoppingCartItemHandler.AddShoppingCartItem).RequirePermission(EcommercePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateShoppingCartItem", ShoppingCartItemHandler.UpdateShoppingCartItem).RequirePermission(EcommercePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteShoppingCartItem", ShoppingCartItemHandler.DeleteShoppingCartItem).RequirePermission(EcommercePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT);




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
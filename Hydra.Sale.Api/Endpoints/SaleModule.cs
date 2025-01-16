using Hydra.Crm.Core.Constants;
using Hydra.Infrastructure.ModuleExtension;
using Hydra.Infrastructure.Security.Extension;
using Hydra.Sale.Api.Handler;
using Hydra.Sale.Api.Services;
using Hydra.Sale.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.Sale.Api.Endpoints
{
    public class SaleModule : IModule
    {
        private const string API_SCHEMA = "/Sale";
        public IServiceCollection RegisterModules(IServiceCollection services)
        {
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IDeliveryDateService, DeliveryDateService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IManufacturerService, ManufacturerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDiscountService, OrderDiscountService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IOrderNoteService, OrderNoteService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductInventoryService, ProductInventoryService>();
            services.AddScoped<IProductAttributeService, ProductAttributeService>();
            services.AddScoped<IProductReviewService, ProductReviewService>();
            services.AddScoped<IProductReviewHelpfulnessService, ProductReviewHelpfulnessService>();
            services.AddScoped<IProductTagService, ProductTagService>();
            services.AddScoped<ISearchTermService, SearchTermService>();
            services.AddScoped<IShipmentService, ShipmentService>();
            services.AddScoped<IShipmentItemService, ShipmentItemService>();
            services.AddScoped<IShippingMethodService, ShippingMethodService>();
            services.AddScoped<IShoppingCartItemService, ShoppingCartItemService>();
            services.AddScoped<IStateProvinceService, StateProvinceService>();
            services.AddScoped<ITaxCategoryService, TaxCategoryService>();
            services.AddScoped<ITaxRateService, TaxRateService>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(API_SCHEMA + "/GetCategoryList", CategoryHandler.GetList).RequirePermission(SalePermissionTypes.SALE_CATEGORY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/GetCategoryListForSelect", CategoryHandler.GetListForSelect).RequirePermission(SalePermissionTypes.SALE_CATEGORY_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetCategoryHierarchy", CategoryHandler.GetCategoryHierarchy).RequirePermission(SalePermissionTypes.SALE_CATEGORY_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetCategoryById", CategoryHandler.GetCategoryById).RequirePermission(SalePermissionTypes.SALE_CATEGORY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddCategory", CategoryHandler.AddCategory).RequirePermission(SalePermissionTypes.SALE_CATEGORY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateCategory", CategoryHandler.UpdateCategory).RequirePermission(SalePermissionTypes.SALE_CATEGORY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteCategory", CategoryHandler.DeleteCategory).RequirePermission(SalePermissionTypes.SALE_CATEGORY_MANAGEMENT);


            endpoints.MapPost(API_SCHEMA + "/GetProductAttributeList", ProductAttributeHandler.GetList).RequirePermission(SalePermissionTypes.SALE_PRODUCT_ATTRIBUTE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/GetProductAttributesForSelect", ProductAttributeHandler.GetListForSelect).RequirePermission(SalePermissionTypes.SALE_PRODUCT_ATTRIBUTE_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetProductAttributeById", ProductAttributeHandler.GetProductAttributeById).RequirePermission(SalePermissionTypes.SALE_PRODUCT_ATTRIBUTE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddProductAttribute", ProductAttributeHandler.AddProductAttribute).RequirePermission(SalePermissionTypes.SALE_PRODUCT_ATTRIBUTE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateProductAttribute", ProductAttributeHandler.UpdateProductAttribute).RequirePermission(SalePermissionTypes.SALE_PRODUCT_ATTRIBUTE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteProductAttribute", ProductAttributeHandler.DeleteProductAttribute).RequirePermission(SalePermissionTypes.SALE_PRODUCT_ATTRIBUTE_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetOrderList", OrderHandler.GetList).RequirePermission(SalePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetOrderById", OrderHandler.GetOrderById).RequirePermission(SalePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetOrderPaymentById", OrderHandler.GetOrderPaymentById).RequirePermission(SalePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetAllOrderStatus", OrderHandler.GetAllOrderStatus).RequirePermission(SalePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetAllShippingStatus", OrderHandler.GetAllShippingStatus).RequirePermission(SalePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddOrder", OrderHandler.AddOrder).RequirePermission(SalePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateOrder", OrderHandler.UpdateOrder).RequirePermission(SalePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateOrderState", OrderHandler.UpdateOrderState).RequirePermission(SalePermissionTypes.SALE_ORDER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteOrder", OrderHandler.DeleteOrder).RequirePermission(SalePermissionTypes.SALE_ORDER_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetOrderDiscountList", OrderDiscountHandler.GetList).RequirePermission(SalePermissionTypes.SALE_ORDER_DISCOUNT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetOrderDiscountById", OrderDiscountHandler.GetOrderDiscountById).RequirePermission(SalePermissionTypes.SALE_ORDER_DISCOUNT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddOrderDiscount", OrderDiscountHandler.AddOrderDiscount).RequirePermission(SalePermissionTypes.SALE_ORDER_DISCOUNT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateOrderDiscount", OrderDiscountHandler.UpdateOrderDiscount).RequirePermission(SalePermissionTypes.SALE_ORDER_DISCOUNT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteOrderDiscount", OrderDiscountHandler.DeleteOrderDiscount).RequirePermission(SalePermissionTypes.SALE_ORDER_DISCOUNT_MANAGEMENT);

            endpoints.MapGet(API_SCHEMA + "/GetOrderItemList", OrderItemHandler.GetList).RequirePermission(SalePermissionTypes.SALE_ORDER_ITEM_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetOrderItemById", OrderItemHandler.GetOrderItemById).RequirePermission(SalePermissionTypes.SALE_ORDER_ITEM_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddOrderItem", OrderItemHandler.AddOrderItem).RequirePermission(SalePermissionTypes.SALE_ORDER_ITEM_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateOrderItem", OrderItemHandler.UpdateOrderItem).RequirePermission(SalePermissionTypes.SALE_ORDER_ITEM_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteOrderItem", OrderItemHandler.DeleteOrderItem).RequirePermission(SalePermissionTypes.SALE_ORDER_ITEM_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetOrderNoteList", OrderNoteHandler.GetList).RequirePermission(SalePermissionTypes.SALE_ORDERNOTE_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetOrderNoteById", OrderNoteHandler.GetOrderNoteById).RequirePermission(SalePermissionTypes.SALE_ORDERNOTE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddOrderNote", OrderNoteHandler.AddOrderNote).RequirePermission(SalePermissionTypes.SALE_ORDERNOTE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateOrderNote", OrderNoteHandler.UpdateOrderNote).RequirePermission(SalePermissionTypes.SALE_ORDERNOTE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteOrderNote", OrderNoteHandler.DeleteOrderNote).RequirePermission(SalePermissionTypes.SALE_ORDERNOTE_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetPaymentList", PaymentHandler.GetList).RequirePermission(SalePermissionTypes.SALE_PAYMENT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetPaymentById", PaymentHandler.GetPaymentById).RequirePermission(SalePermissionTypes.SALE_PAYMENT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetAllPaymentStatus", PaymentHandler.GetAllPaymentStatus).RequirePermission(SalePermissionTypes.SALE_PAYMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddPayment", PaymentHandler.AddPayment).RequirePermission(SalePermissionTypes.SALE_PAYMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdatePayment", PaymentHandler.UpdatePayment).RequirePermission(SalePermissionTypes.SALE_PAYMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeletePayment", PaymentHandler.DeletePayment).RequirePermission(SalePermissionTypes.SALE_PAYMENT_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetProductList", ProductHandler.GetList).RequirePermission(SalePermissionTypes.SALE_PRODUCT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetProductById", ProductHandler.GetProductById).RequirePermission(SalePermissionTypes.SALE_PRODUCT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetProductsByIds", ProductHandler.GetProductsByIds).RequirePermission(SalePermissionTypes.SALE_PRODUCT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetProductsByInput", ProductHandler.GetProductsByInput).RequirePermission(SalePermissionTypes.SALE_PRODUCT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddProduct", ProductHandler.AddProduct).RequirePermission(SalePermissionTypes.SALE_PRODUCT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateProduct", ProductHandler.UpdateProduct).RequirePermission(SalePermissionTypes.SALE_PRODUCT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/DeleteProduct", ProductHandler.DeleteProduct).RequirePermission(SalePermissionTypes.SALE_PRODUCT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/RemoveProduct", ProductHandler.RemoveProduct).RequirePermission(SalePermissionTypes.SALE_PRODUCT_MANAGEMENT);


            endpoints.MapPost(API_SCHEMA + "/GetProductInventoryList", ProductInventoryHandler.GetList).RequirePermission(SalePermissionTypes.SALE_PRODUCT_INVENTORY_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetProductInventoryById", ProductInventoryHandler.GetProductInventoryById).RequirePermission(SalePermissionTypes.SALE_PRODUCT_INVENTORY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddProductInventory", ProductInventoryHandler.AddProductInventory).RequirePermission(SalePermissionTypes.SALE_PRODUCT_INVENTORY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateProductInventory", ProductInventoryHandler.UpdateProductInventory).RequirePermission(SalePermissionTypes.SALE_PRODUCT_INVENTORY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteProductInventory", ProductInventoryHandler.DeleteProductInventory).RequirePermission(SalePermissionTypes.SALE_PRODUCT_INVENTORY_MANAGEMENT);


            endpoints.MapPost(API_SCHEMA + "/GetProductReviewList", ProductReviewHandler.GetList).RequirePermission(SalePermissionTypes.SALE_PRODUCT_REVIEW_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetProductReviewById", ProductReviewHandler.GetProductReviewById).RequirePermission(SalePermissionTypes.SALE_PRODUCT_REVIEW_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddProductReview", ProductReviewHandler.AddProductReview).RequirePermission(SalePermissionTypes.SALE_PRODUCT_REVIEW_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateProductReview", ProductReviewHandler.UpdateProductReview).RequirePermission(SalePermissionTypes.SALE_PRODUCT_REVIEW_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteProductReview", ProductReviewHandler.DeleteProductReview).RequirePermission(SalePermissionTypes.SALE_PRODUCT_REVIEW_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetProductReviewHelpfulnessList", ProductReviewHelpfulnessHandler.GetList).RequirePermission(SalePermissionTypes.SALE_PRODUCT_REVIEW_HELPFULNESS_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetProductReviewHelpfulnessById", ProductReviewHelpfulnessHandler.GetProductReviewHelpfulnessById).RequirePermission(SalePermissionTypes.SALE_PRODUCT_REVIEW_HELPFULNESS_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddProductReviewHelpfulness", ProductReviewHelpfulnessHandler.AddProductReviewHelpfulness).RequirePermission(SalePermissionTypes.SALE_PRODUCT_REVIEW_HELPFULNESS_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateProductReviewHelpfulness", ProductReviewHelpfulnessHandler.UpdateProductReviewHelpfulness).RequirePermission(SalePermissionTypes.SALE_PRODUCT_REVIEW_HELPFULNESS_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteProductReviewHelpfulness", ProductReviewHelpfulnessHandler.DeleteProductReviewHelpfulness).RequirePermission(SalePermissionTypes.SALE_PRODUCT_REVIEW_HELPFULNESS_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetProductTagList", ProductTagHandler.GetList).RequirePermission(SalePermissionTypes.SALE_PRODUCT_TAG_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/GetProductTagListForSelect", ProductTagHandler.GetListForSelect).RequirePermission(SalePermissionTypes.SALE_PRODUCT_TAG_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetProductTagById", ProductTagHandler.GetProductTagById).RequirePermission(SalePermissionTypes.SALE_PRODUCT_TAG_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddProductTag", ProductTagHandler.AddProductTag).RequirePermission(SalePermissionTypes.SALE_PRODUCT_TAG_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateProductTag", ProductTagHandler.UpdateProductTag).RequirePermission(SalePermissionTypes.SALE_PRODUCT_TAG_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteProductTag", ProductTagHandler.DeleteProductTag).RequirePermission(SalePermissionTypes.SALE_PRODUCT_TAG_MANAGEMENT);


            endpoints.MapPost(API_SCHEMA + "/GetSearchTermList", SearchTermHandler.GetList).RequirePermission(SalePermissionTypes.SALE_SEARCH_TERM_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetSearchTermById", SearchTermHandler.GetSearchTermById).RequirePermission(SalePermissionTypes.SALE_SEARCH_TERM_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddSearchTerm", SearchTermHandler.AddSearchTerm).RequirePermission(SalePermissionTypes.SALE_SEARCH_TERM_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateSearchTerm", SearchTermHandler.UpdateSearchTerm).RequirePermission(SalePermissionTypes.SALE_SEARCH_TERM_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteSearchTerm", SearchTermHandler.DeleteSearchTerm).RequirePermission(SalePermissionTypes.SALE_SEARCH_TERM_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetShipmentList", ShipmentHandler.GetList).RequirePermission(SalePermissionTypes.SALE_SHIPMENT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetShipmentById", ShipmentHandler.GetShipmentById).RequirePermission(SalePermissionTypes.SALE_SHIPMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddShipment", ShipmentHandler.AddShipment).RequirePermission(SalePermissionTypes.SALE_SHIPMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateShipment", ShipmentHandler.UpdateShipment).RequirePermission(SalePermissionTypes.SALE_SHIPMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteShipment", ShipmentHandler.DeleteShipment).RequirePermission(SalePermissionTypes.SALE_SHIPMENT_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetShipmentItemList", ShipmentItemHandler.GetList).RequirePermission(SalePermissionTypes.SALE_SHIPMENT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetShipmentItemById", ShipmentItemHandler.GetShipmentItemById).RequirePermission(SalePermissionTypes.SALE_SHIPMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddShipmentItem", ShipmentItemHandler.AddShipmentItem).RequirePermission(SalePermissionTypes.SALE_SHIPMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateShipmentItem", ShipmentItemHandler.UpdateShipmentItem).RequirePermission(SalePermissionTypes.SALE_SHIPMENT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteShipmentItem", ShipmentItemHandler.DeleteShipmentItem).RequirePermission(SalePermissionTypes.SALE_SHIPMENT_MANAGEMENT);


            endpoints.MapPost(API_SCHEMA + "/GetShoppingCartItemList", ShoppingCartItemHandler.GetList).RequirePermission(SalePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetShoppingCartItemById", ShoppingCartItemHandler.GetShoppingCartItemById).RequirePermission(SalePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddShoppingCartItem", ShoppingCartItemHandler.AddShoppingCartItem).RequirePermission(SalePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateShoppingCartItem", ShoppingCartItemHandler.UpdateShoppingCartItem).RequirePermission(SalePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteShoppingCartItem", ShoppingCartItemHandler.DeleteShoppingCartItem).RequirePermission(SalePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT);

            #region BASE Entities

            endpoints.MapPost(API_SCHEMA + "/GetCurrencyList", CurrencyHandler.GetList).RequirePermission(SalePermissionTypes.SALE_CURRENCY_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetAllCurrencies", CurrencyHandler.GetAllCurrencies).RequirePermission(SalePermissionTypes.SALE_CURRENCY_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetCurrencyById", CurrencyHandler.GetCurrencyById).RequirePermission(SalePermissionTypes.SALE_CURRENCY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddCurrency", CurrencyHandler.AddCurrency).RequirePermission(SalePermissionTypes.SALE_CURRENCY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateCurrency", CurrencyHandler.UpdateCurrency).RequirePermission(SalePermissionTypes.SALE_CURRENCY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteCurrency", CurrencyHandler.DeleteCurrency).RequirePermission(SalePermissionTypes.SALE_CURRENCY_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetDeliveryDateList", DeliveryDateHandler.GetList).RequirePermission(SalePermissionTypes.SALE_DELIVERY_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetDeliveryDateById", DeliveryDateHandler.GetDeliveryDateById).RequirePermission(SalePermissionTypes.SALE_DELIVERY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddDeliveryDate", DeliveryDateHandler.AddDeliveryDate).RequirePermission(SalePermissionTypes.SALE_DELIVERY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateDeliveryDate", DeliveryDateHandler.UpdateDeliveryDate).RequirePermission(SalePermissionTypes.SALE_DELIVERY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteDeliveryDate", DeliveryDateHandler.DeleteDeliveryDate).RequirePermission(SalePermissionTypes.SALE_DELIVERY_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetDiscountList", DiscountHandler.GetList).RequirePermission(SalePermissionTypes.SALE_DISCOUNT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/GetDiscountListForSelect", DiscountHandler.GetListForSelect).RequirePermission(SalePermissionTypes.SALE_DISCOUNT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetDiscountById", DiscountHandler.GetDiscountById).RequirePermission(SalePermissionTypes.SALE_DISCOUNT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddDiscount", DiscountHandler.AddDiscount).RequirePermission(SalePermissionTypes.SALE_DISCOUNT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateDiscount", DiscountHandler.UpdateDiscount).RequirePermission(SalePermissionTypes.SALE_DISCOUNT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteDiscount", DiscountHandler.DeleteDiscount).RequirePermission(SalePermissionTypes.SALE_DISCOUNT_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetManufacturerList", ManufacturerHandler.GetList).RequirePermission(SalePermissionTypes.SALE_MANUFACTURER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/GetManufacturersForSelect", ManufacturerHandler.GetListForSelect).RequirePermission(SalePermissionTypes.SALE_MANUFACTURER_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetManufacturerById", ManufacturerHandler.GetManufacturerById).RequirePermission(SalePermissionTypes.SALE_MANUFACTURER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddManufacturer", ManufacturerHandler.AddManufacturer).RequirePermission(SalePermissionTypes.SALE_MANUFACTURER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateManufacturer", ManufacturerHandler.UpdateManufacturer).RequirePermission(SalePermissionTypes.SALE_MANUFACTURER_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteManufacturer", ManufacturerHandler.DeleteManufacturer).RequirePermission(SalePermissionTypes.SALE_MANUFACTURER_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetShippingMethodList", ShippingMethodHandler.GetList).RequirePermission(SalePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetAllShippingMethods", ShippingMethodHandler.GetAllShippingMethods).RequirePermission(SalePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetShippingMethodById", ShippingMethodHandler.GetShippingMethodById).RequirePermission(SalePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddShippingMethod", ShippingMethodHandler.AddShippingMethod).RequirePermission(SalePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateShippingMethod", ShippingMethodHandler.UpdateShippingMethod).RequirePermission(SalePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteShippingMethod", ShippingMethodHandler.DeleteShippingMethod).RequirePermission(SalePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);


            endpoints.MapPost(API_SCHEMA + "/GetCountryList", CountryHandler.GetList).RequirePermission(SalePermissionTypes.SALE_COUNTRY_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetCountryById", CountryHandler.GetCountryById).RequirePermission(SalePermissionTypes.SALE_COUNTRY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddCountry", CountryHandler.AddCountry).RequirePermission(SalePermissionTypes.SALE_COUNTRY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateCountry", CountryHandler.UpdateCountry).RequirePermission(SalePermissionTypes.SALE_COUNTRY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteCountry", CountryHandler.DeleteCountry).RequirePermission(SalePermissionTypes.SALE_COUNTRY_MANAGEMENT);


            endpoints.MapPost(API_SCHEMA + "/GetStateProvinceList", StateProvinceHandler.GetList).RequirePermission(SalePermissionTypes.SALE_STATE_PROVINCE_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetStateProvinceById", StateProvinceHandler.GetStateProvinceById).RequirePermission(SalePermissionTypes.SALE_STATE_PROVINCE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddStateProvince", StateProvinceHandler.AddStateProvince).RequirePermission(SalePermissionTypes.SALE_STATE_PROVINCE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateStateProvince", StateProvinceHandler.UpdateStateProvince).RequirePermission(SalePermissionTypes.SALE_STATE_PROVINCE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteStateProvince", StateProvinceHandler.DeleteStateProvince).RequirePermission(SalePermissionTypes.SALE_STATE_PROVINCE_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetAddressList", AddressHandler.GetList).RequirePermission(SalePermissionTypes.SALE_ADDRESS_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetAddressById", AddressHandler.GetAddressById).RequirePermission(SalePermissionTypes.SALE_ADDRESS_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddAddress", AddressHandler.AddAddress).RequirePermission(SalePermissionTypes.SALE_ADDRESS_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateAddress", AddressHandler.UpdateAddress).RequirePermission(SalePermissionTypes.SALE_ADDRESS_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteAddress", AddressHandler.DeleteAddress).RequirePermission(SalePermissionTypes.SALE_ADDRESS_MANAGEMENT);


            endpoints.MapPost(API_SCHEMA + "/GetTaxCategoryList", TaxCategoryHandler.GetList).RequirePermission(SalePermissionTypes.SALE_TAX_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetTaxCategoryById", TaxCategoryHandler.GetTaxCategoryById).RequirePermission(SalePermissionTypes.SALE_TAX_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddTaxCategory", TaxCategoryHandler.AddTaxCategory).RequirePermission(SalePermissionTypes.SALE_TAX_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateTaxCategory", TaxCategoryHandler.UpdateTaxCategory).RequirePermission(SalePermissionTypes.SALE_TAX_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteTaxCategory", TaxCategoryHandler.DeleteTaxCategory).RequirePermission(SalePermissionTypes.SALE_TAX_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetTaxRateList", TaxRateHandler.GetList).RequirePermission(SalePermissionTypes.SALE_TAX_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetTaxRateById", TaxRateHandler.GetTaxRateById).RequirePermission(SalePermissionTypes.SALE_TAX_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddTaxRate", TaxRateHandler.AddTaxRate).RequirePermission(SalePermissionTypes.SALE_TAX_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateTaxRate", TaxRateHandler.UpdateTaxRate).RequirePermission(SalePermissionTypes.SALE_TAX_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteTaxRate", TaxRateHandler.DeleteTaxRate).RequirePermission(SalePermissionTypes.SALE_TAX_MANAGEMENT);

            #endregion

            return endpoints;
        }

    }
}
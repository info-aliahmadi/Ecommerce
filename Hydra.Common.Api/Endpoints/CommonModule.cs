using Hydra.Common.Api.Handler;
using Hydra.Common.Api.Services;
using Hydra.Common.Core.Interfaces;
using Hydra.Ecommerce.Core.Constants;
using Hydra.Infrastructure.ModuleExtension;
using Hydra.Infrastructure.Security.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.Common.Api.Endpoints
{
    public class CommonModule : IModule
    {
        private const string API_SCHEMA = "/Common";
        public IServiceCollection RegisterModules(IServiceCollection services)
        {
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<ISearchTermService, SearchTermService>();
            services.AddScoped<IStateProvinceService, StateProvinceService>();
            services.AddScoped<ITaxCategoryService, TaxCategoryService>();
            services.AddScoped<ITaxRateService, TaxRateService>();
            services.AddScoped<IShippingMethodService, ShippingMethodService>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {


            endpoints.MapPost(API_SCHEMA + "/GetSearchTermList", SearchTermHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_SEARCH_TERM_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetSearchTermById", SearchTermHandler.GetSearchTermById).RequirePermission(EcommercePermissionTypes.SALE_SEARCH_TERM_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddSearchTerm", SearchTermHandler.AddSearchTerm).RequirePermission(EcommercePermissionTypes.SALE_SEARCH_TERM_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateSearchTerm", SearchTermHandler.UpdateSearchTerm).RequirePermission(EcommercePermissionTypes.SALE_SEARCH_TERM_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteSearchTerm", SearchTermHandler.DeleteSearchTerm).RequirePermission(EcommercePermissionTypes.SALE_SEARCH_TERM_MANAGEMENT);

            #region BASE Entities

            endpoints.MapPost(API_SCHEMA + "/GetDiscountList", DiscountHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_DISCOUNT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/GetDiscountListForSelect", DiscountHandler.GetListForSelect).RequirePermission(EcommercePermissionTypes.SALE_DISCOUNT_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetDiscountById", DiscountHandler.GetDiscountById).RequirePermission(EcommercePermissionTypes.SALE_DISCOUNT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddDiscount", DiscountHandler.AddDiscount).RequirePermission(EcommercePermissionTypes.SALE_DISCOUNT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateDiscount", DiscountHandler.UpdateDiscount).RequirePermission(EcommercePermissionTypes.SALE_DISCOUNT_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteDiscount", DiscountHandler.DeleteDiscount).RequirePermission(EcommercePermissionTypes.SALE_DISCOUNT_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetCountryList", CountryHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_COUNTRY_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetCountryById", CountryHandler.GetCountryById).RequirePermission(EcommercePermissionTypes.SALE_COUNTRY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddCountry", CountryHandler.AddCountry).RequirePermission(EcommercePermissionTypes.SALE_COUNTRY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateCountry", CountryHandler.UpdateCountry).RequirePermission(EcommercePermissionTypes.SALE_COUNTRY_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteCountry", CountryHandler.DeleteCountry).RequirePermission(EcommercePermissionTypes.SALE_COUNTRY_MANAGEMENT);


            endpoints.MapPost(API_SCHEMA + "/GetStateProvinceList", StateProvinceHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_STATE_PROVINCE_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetStateProvinceById", StateProvinceHandler.GetStateProvinceById).RequirePermission(EcommercePermissionTypes.SALE_STATE_PROVINCE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddStateProvince", StateProvinceHandler.AddStateProvince).RequirePermission(EcommercePermissionTypes.SALE_STATE_PROVINCE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateStateProvince", StateProvinceHandler.UpdateStateProvince).RequirePermission(EcommercePermissionTypes.SALE_STATE_PROVINCE_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteStateProvince", StateProvinceHandler.DeleteStateProvince).RequirePermission(EcommercePermissionTypes.SALE_STATE_PROVINCE_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetAddressList", AddressHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_ADDRESS_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetAddressById", AddressHandler.GetAddressById).RequirePermission(EcommercePermissionTypes.SALE_ADDRESS_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddAddress", AddressHandler.AddAddress).RequirePermission(EcommercePermissionTypes.SALE_ADDRESS_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateAddress", AddressHandler.UpdateAddress).RequirePermission(EcommercePermissionTypes.SALE_ADDRESS_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteAddress", AddressHandler.DeleteAddress).RequirePermission(EcommercePermissionTypes.SALE_ADDRESS_MANAGEMENT);


            endpoints.MapGet(API_SCHEMA + "/GetTaxCategoryList", TaxCategoryHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_TAX_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetTaxCategoryById", TaxCategoryHandler.GetTaxCategoryById).RequirePermission(EcommercePermissionTypes.SALE_TAX_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddTaxCategory", TaxCategoryHandler.AddTaxCategory).RequirePermission(EcommercePermissionTypes.SALE_TAX_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateTaxCategory", TaxCategoryHandler.UpdateTaxCategory).RequirePermission(EcommercePermissionTypes.SALE_TAX_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteTaxCategory", TaxCategoryHandler.DeleteTaxCategory).RequirePermission(EcommercePermissionTypes.SALE_TAX_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetTaxRateList", TaxRateHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_TAX_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetTaxRateById", TaxRateHandler.GetTaxRateById).RequirePermission(EcommercePermissionTypes.SALE_TAX_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddTaxRate", TaxRateHandler.AddTaxRate).RequirePermission(EcommercePermissionTypes.SALE_TAX_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateTaxRate", TaxRateHandler.UpdateTaxRate).RequirePermission(EcommercePermissionTypes.SALE_TAX_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteTaxRate", TaxRateHandler.DeleteTaxRate).RequirePermission(EcommercePermissionTypes.SALE_TAX_MANAGEMENT);

            endpoints.MapPost(API_SCHEMA + "/GetShippingMethodList", ShippingMethodHandler.GetList).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetAllShippingMethods", ShippingMethodHandler.GetAllShippingMethods).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);
            endpoints.MapGet(API_SCHEMA + "/GetShippingMethodById", ShippingMethodHandler.GetShippingMethodById).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/AddShippingMethod", ShippingMethodHandler.AddShippingMethod).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/UpdateShippingMethod", ShippingMethodHandler.UpdateShippingMethod).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);
            endpoints.MapPost(API_SCHEMA + "/DeleteShippingMethod", ShippingMethodHandler.DeleteShippingMethod).RequirePermission(EcommercePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT);


            #endregion

            return endpoints;
        }

    }
}
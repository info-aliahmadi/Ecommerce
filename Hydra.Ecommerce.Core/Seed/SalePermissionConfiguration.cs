using Hydra.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hydra.Ecommerce.Core.Constants;


namespace Hydra.Ecommerce.Core.Seed
{
    public class SalePermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public static int INCREMENTER = 5000;
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(new Permission()
            {
                Id = INCREMENTER + 1,
                Name = EcommercePermissionTypes.SALE_CATEGORY_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_CATEGORY_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 2,
                Name = EcommercePermissionTypes.SALE_PRODUCT_ATTRIBUTE_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_PRODUCT_ATTRIBUTE_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 3,
                Name = EcommercePermissionTypes.SALE_ORDER_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_ORDER_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 4,
                Name = EcommercePermissionTypes.SALE_ORDER_DISCOUNT_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_ORDER_DISCOUNT_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 5,
                Name = EcommercePermissionTypes.SALE_ORDER_ITEM_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_ORDER_ITEM_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 6,
                Name = EcommercePermissionTypes.SALE_ORDERNOTE_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_ORDERNOTE_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 7,
                Name = EcommercePermissionTypes.SALE_PAYMENT_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_PAYMENT_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 8,
                Name = EcommercePermissionTypes.SALE_PRODUCT_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_PRODUCT_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 9,
                Name = EcommercePermissionTypes.SALE_PRODUCT_INVENTORY_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_PRODUCT_INVENTORY_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 10,
                Name = EcommercePermissionTypes.SALE_PRODUCT_REVIEW_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_PRODUCT_REVIEW_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 11,
                Name = EcommercePermissionTypes.SALE_PRODUCT_REVIEW_HELPFULNESS_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_PRODUCT_REVIEW_HELPFULNESS_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 12,
                Name = EcommercePermissionTypes.SALE_PRODUCT_TAG_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_PRODUCT_TAG_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 13,
                Name = EcommercePermissionTypes.SALE_SEARCH_TERM_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_SEARCH_TERM_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 14,
                Name = EcommercePermissionTypes.SALE_SHIPMENT_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_SHIPMENT_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 15,
                Name = EcommercePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 16,
                Name = EcommercePermissionTypes.SALE_CURRENCY_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_CURRENCY_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 17,
                Name = EcommercePermissionTypes.SALE_DELIVERY_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_DELIVERY_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 18,
                Name = EcommercePermissionTypes.SALE_DISCOUNT_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_DISCOUNT_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 19,
                Name = EcommercePermissionTypes.SALE_MANUFACTURER_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_MANUFACTURER_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 20,
                Name = EcommercePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 21,
                Name = EcommercePermissionTypes.SALE_COUNTRY_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_COUNTRY_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 22,
                Name = EcommercePermissionTypes.SALE_STATE_PROVINCE_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_STATE_PROVINCE_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 23,
                Name = EcommercePermissionTypes.SALE_ADDRESS_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_ADDRESS_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 24,
                Name = EcommercePermissionTypes.SALE_TAX_MANAGEMENT,
                NormalizedName = EcommercePermissionTypes.SALE_TAX_MANAGEMENT,
            });
        }
    }
}

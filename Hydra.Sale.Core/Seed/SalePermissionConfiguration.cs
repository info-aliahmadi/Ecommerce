using Hydra.Crm.Core.Constants;
using Hydra.Infrastructure.Security.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hydra.Sale.Core.Seed
{
    public class SalePermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public static int INCREMENTER = 5000;
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(new Permission()
            {
                Id = INCREMENTER + 1,
                Name = SalePermissionTypes.SALE_CATEGORY_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_CATEGORY_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 2,
                Name = SalePermissionTypes.SALE_PRODUCT_ATTRIBUTE_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_PRODUCT_ATTRIBUTE_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 3,
                Name = SalePermissionTypes.SALE_ORDER_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_ORDER_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 4,
                Name = SalePermissionTypes.SALE_ORDER_DISCOUNT_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_ORDER_DISCOUNT_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 5,
                Name = SalePermissionTypes.SALE_ORDER_ITEM_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_ORDER_ITEM_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 6,
                Name = SalePermissionTypes.SALE_ORDERNOTE_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_ORDERNOTE_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 7,
                Name = SalePermissionTypes.SALE_PAYMENT_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_PAYMENT_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 8,
                Name = SalePermissionTypes.SALE_PRODUCT_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_PRODUCT_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 9,
                Name = SalePermissionTypes.SALE_PRODUCT_INVENTORY_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_PRODUCT_INVENTORY_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 10,
                Name = SalePermissionTypes.SALE_PRODUCT_REVIEW_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_PRODUCT_REVIEW_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 11,
                Name = SalePermissionTypes.SALE_PRODUCT_REVIEW_HELPFULNESS_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_PRODUCT_REVIEW_HELPFULNESS_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 12,
                Name = SalePermissionTypes.SALE_PRODUCT_TAG_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_PRODUCT_TAG_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 13,
                Name = SalePermissionTypes.SALE_SEARCH_TERM_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_SEARCH_TERM_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 14,
                Name = SalePermissionTypes.SALE_SHIPMENT_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_SHIPMENT_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 15,
                Name = SalePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_SHOPPING_CART_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 16,
                Name = SalePermissionTypes.SALE_CURRENCY_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_CURRENCY_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 17,
                Name = SalePermissionTypes.SALE_DELIVERY_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_DELIVERY_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 18,
                Name = SalePermissionTypes.SALE_DISCOUNT_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_DISCOUNT_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 19,
                Name = SalePermissionTypes.SALE_MANUFACTURER_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_MANUFACTURER_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 20,
                Name = SalePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_SHIPMENT_METHOD_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 21,
                Name = SalePermissionTypes.SALE_COUNTRY_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_COUNTRY_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 22,
                Name = SalePermissionTypes.SALE_STATE_PROVINCE_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_STATE_PROVINCE_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 23,
                Name = SalePermissionTypes.SALE_ADDRESS_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_ADDRESS_MANAGEMENT,
            }, new Permission()
            {
                Id = INCREMENTER + 24,
                Name = SalePermissionTypes.SALE_TAX_MANAGEMENT,
                NormalizedName = SalePermissionTypes.SALE_TAX_MANAGEMENT,
            });
        }
    }
}

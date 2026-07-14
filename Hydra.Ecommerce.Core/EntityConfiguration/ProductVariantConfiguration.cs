using Hydra.Ecommerce.Core.Domain;
using Hydra.Ecommerce.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> entity)
        {
            entity.ToTable("ProductVariant", "Sale");

            entity.HasIndex(e => e.SKU, "IX_Variant_SKU");
            entity.HasIndex(e => e.ProductId, "IX_Variant_ProductId");

            entity.Property(e => e.SKU)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.SellPrice)
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.OldSellPrice)
                .HasColumnType("decimal(18,2)");

            entity.HasOne(v => v.Product)
                .WithMany(x=>x.ProductVariants)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(v => v.ProductInventory)
                .WithOne(i => i.Variant)
                .HasForeignKey<ProductInventory>(i => i.VariantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Default variants for each seeded product
            entity.HasData(
                // Product 1000 - Wireless Headphones
                new ProductVariant { Id = 2000, ProductId = 1000, SKU = "BOOK-1000-DF", SellPrice = 129.99m, OldSellPrice = 0m },
                // Product 1001 - Smart Watch Pro
                new ProductVariant { Id = 2001, ProductId = 1001, SKU = "ELECTRONIK-1001-DF", SellPrice = 299.99m, OldSellPrice = 399.99m },
                // Product 1002 - Bluetooth Speaker
                new ProductVariant { Id = 2002, ProductId = 1002, SKU = "ELECTRONIK-1002-DF", SellPrice = 79.99m, OldSellPrice = 119.99m },
                // Product 1003 - Mechanical Keyboard
                new ProductVariant { Id = 2003, ProductId = 1003, SKU = "ELECTRONIK-1003-DF", SellPrice = 149.99m, OldSellPrice = 189.99m },
                // Product 1004 - Leather Jacket (default + color/size variants)
                new ProductVariant { Id = 2004, ProductId = 1004, SKU = "FASHION-1004-DF", SellPrice = 189.99m, OldSellPrice = 279.99m },
                new ProductVariant { Id = 2005, ProductId = 1004, SKU = "FASHION-1004-BLK-M", SellPrice = 189.99m, OldSellPrice = 279.99m },
                new ProductVariant { Id = 2006, ProductId = 1004, SKU = "FASHION-1004-BRN-L", SellPrice = 199.99m, OldSellPrice = 289.99m },
                // Product 1005 - Cotton T-Shirt (default + color/size variants)
                new ProductVariant { Id = 2007, ProductId = 1005, SKU = "FASION-1005-DF", SellPrice = 39.99m, OldSellPrice = 59.99m },
                new ProductVariant { Id = 2008, ProductId = 1005, SKU = "FASION-1005-WHT-S", SellPrice = 39.99m, OldSellPrice = 59.99m },
                new ProductVariant { Id = 2009, ProductId = 1005, SKU = "FASION-1005-BLK-M", SellPrice = 39.99m, OldSellPrice = 59.99m },
                new ProductVariant { Id = 2010, ProductId = 1005, SKU = "FASION-1005-RED-L", SellPrice = 44.99m, OldSellPrice = 64.99m },
                // Product 1006 - Designer Sunglasses
                new ProductVariant { Id = 2011, ProductId = 1006, SKU = "HOME-1006-DF", SellPrice = 129.99m, OldSellPrice = 179.99m },
                // Product 1007 - Desk Lamp
                new ProductVariant { Id = 2012, ProductId = 1007, SKU = "HOME-1007-DF", SellPrice = 89.99m, OldSellPrice = 129.99m },
                // Product 1008 - Plant Pot Set
                new ProductVariant { Id = 2013, ProductId = 1008, SKU = "HOME-1008-DF", SellPrice = 44.99m, OldSellPrice = 64.99m },
                // Product 1009 - Yoga Mat
                new ProductVariant { Id = 2014, ProductId = 1009, SKU = "SPORT-1009-DF", SellPrice = 49.99m, OldSellPrice = 69.99m },
                // Product 1010 - Skincare Kit
                new ProductVariant { Id = 2015, ProductId = 1010, SKU = "BEAUTY-1010-DF", SellPrice = 89.99m, OldSellPrice = 129.99m },
                // Product 1011 - Art of Modern Living
                new ProductVariant { Id = 2016, ProductId = 1011, SKU = "BOOK-1011-DF", SellPrice = 34.99m, OldSellPrice = 49.99m }
            );
        }
    }
}

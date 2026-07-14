using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class ProductInventoryConfiguration : IEntityTypeConfiguration<ProductInventory>
    {
        public void Configure(EntityTypeBuilder<ProductInventory> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("ProductInventory", "Sale");

            entity.HasIndex(e => e.VariantId, "IX_Inventory_VariantId");

            entity.Property(e => e.StockQuantity)
                .HasColumnType("decimal(18,3)");

            entity.Property(e => e.ReservedQuantity)
                .HasColumnType("decimal(18,3)");

            entity.HasOne(d => d.Variant)
                .WithOne(v => v.ProductInventory)
                .HasForeignKey<ProductInventory>(d => d.VariantId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(a => a.InventoryTransactions)
               .WithOne(x => x.ProductInventory)
               .HasForeignKey(a => a.ProductInventoryId)
               .OnDelete(DeleteBehavior.SetNull);

            var created = DateTime.SpecifyKind(DateTime.Parse("2026-04-23"), DateTimeKind.Utc);

            entity.HasData(
                new ProductInventory { Id = 3000, VariantId = 2000, StockQuantity = 45, ReservedQuantity = 0, CreatedDatetime = created },
                new ProductInventory { Id = 3001, VariantId = 2001, StockQuantity = 32, ReservedQuantity = 0, CreatedDatetime = created },
                new ProductInventory { Id = 3002, VariantId = 2002, StockQuantity = 78, ReservedQuantity = 0, CreatedDatetime = created },
                new ProductInventory { Id = 3003, VariantId = 2003, StockQuantity = 23, ReservedQuantity = 0, CreatedDatetime = created },
                new ProductInventory { Id = 3004, VariantId = 2004, StockQuantity = 5, ReservedQuantity = 0, CreatedDatetime = created },
                new ProductInventory { Id = 3005, VariantId = 2005, StockQuantity = 6, ReservedQuantity = 0, CreatedDatetime = created },
                new ProductInventory { Id = 3006, VariantId = 2006, StockQuantity = 4, ReservedQuantity = 0, CreatedDatetime = created },
                new ProductInventory { Id = 3007, VariantId = 2007, StockQuantity = 40, ReservedQuantity = 0, CreatedDatetime = created },
                new ProductInventory { Id = 3008, VariantId = 2008, StockQuantity = 30, ReservedQuantity = 0, CreatedDatetime = created },
                new ProductInventory { Id = 3009, VariantId = 2009, StockQuantity = 30, ReservedQuantity = 0, CreatedDatetime = created },
                new ProductInventory { Id = 3010, VariantId = 2010, StockQuantity = 20, ReservedQuantity = 0, CreatedDatetime = created },
                new ProductInventory { Id = 3011, VariantId = 2011, StockQuantity = 56, ReservedQuantity = 0, CreatedDatetime = created },
                new ProductInventory { Id = 3012, VariantId = 2012, StockQuantity = 34, ReservedQuantity = 0, CreatedDatetime = created },
                new ProductInventory { Id = 3013, VariantId = 2013, StockQuantity = 67, ReservedQuantity = 0, CreatedDatetime = created },
                new ProductInventory { Id = 3014, VariantId = 2014, StockQuantity = 45, ReservedQuantity = 0, CreatedDatetime = created },
                new ProductInventory { Id = 3015, VariantId = 2015, StockQuantity = 38, ReservedQuantity = 0, CreatedDatetime = created },
                new ProductInventory { Id = 3016, VariantId = 2016, StockQuantity = 65, ReservedQuantity = 0, CreatedDatetime = created }
            );
        }
    }
}

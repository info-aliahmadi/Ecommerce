using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class ProductInventoryTransactionConfiguration : IEntityTypeConfiguration<ProductInventoryTransaction>
    {
        public void Configure(EntityTypeBuilder<ProductInventoryTransaction> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("ProductInventoryTransaction", "Sale");

            entity.HasIndex(e => e.ProductInventoryId, "IX_InventoryTransaction_InventoryId");

            entity.Property(e => e.StockQuantity)
                .HasColumnType("decimal(18,3)");

            entity.Property(e => e.ReservedQuantity)
                .HasColumnType("decimal(18,3)");

        }
    }
}

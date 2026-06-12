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

            entity.HasIndex(e => e.ProductId);

            entity.HasOne(d => d.ProductAttribute).WithMany(p => p.ProductInventories)
            .HasForeignKey(d => d.AttributeId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductInventories)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

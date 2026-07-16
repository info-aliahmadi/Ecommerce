using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class ProductBundleConfiguration : IEntityTypeConfiguration<ProductBundle>
    {
        public void Configure(EntityTypeBuilder<ProductBundle> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK_Product_Bundle_Mapping");

            entity.ToTable("ProductBundle", "Sale");

            entity.HasIndex(e => new { e.BundleId, e.ProductId }, "IX_PCM_Product_and_Bundle").IsUnique();

            entity.HasIndex(e => e.BundleId, "IX_Product_Bundle_Mapping_BundleId");

            entity.HasIndex(e => e.ProductId, "IX_Product_Bundle_Mapping_ProductId");

            entity.HasOne(d => d.Bundle).WithMany(p => p.ProductBundles)
            .HasForeignKey(d => d.BundleId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_ProductBundle_Bundle");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductBundles)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_ProductBundle_Product");

            entity.HasData(
                new ProductBundle() { Id = 1, BundleId = 1, ProductId = 1001, DisplayOrder = 1 },
                new ProductBundle() { Id = 2, BundleId = 1, ProductId = 1002, DisplayOrder = 2 },
                new ProductBundle() { Id = 3, BundleId = 1, ProductId = 1003, DisplayOrder = 3 },
                new ProductBundle() { Id = 4, BundleId = 2, ProductId = 1004, DisplayOrder = 1 },
                new ProductBundle() { Id = 5, BundleId = 2, ProductId = 1005, DisplayOrder = 2 },
                new ProductBundle() { Id = 6, BundleId = 2, ProductId = 1006, DisplayOrder = 3 },
                new ProductBundle() { Id = 7, BundleId = 3, ProductId = 1007, DisplayOrder = 1 },
                new ProductBundle() { Id = 8, BundleId = 3, ProductId = 1008, DisplayOrder = 2 },
                new ProductBundle() { Id = 9, BundleId = 3, ProductId = 1009, DisplayOrder = 3 }
            );
        }
    }
}

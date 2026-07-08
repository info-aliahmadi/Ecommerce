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

            entity.HasIndex(e => new { e.BundleId, e.ProductId }, "IX_PCM_Product_and_Bundle");

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
        }
    }
}

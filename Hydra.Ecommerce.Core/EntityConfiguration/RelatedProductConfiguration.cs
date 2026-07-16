using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class RelatedProductConfiguration : IEntityTypeConfiguration<RelatedProduct>
    {
        public void Configure(EntityTypeBuilder<RelatedProduct> entity)
        {
            entity.ToTable("RelatedProduct", "Sale");

            entity.HasIndex(e => e.ProductId1, "IX_RelatedProduct_ProductId1");

            entity.HasIndex(e => new { e.ProductId1, e.ProductId2 }, "IX_RelatedProduct_ProductId1_ProductId2").IsUnique();

            entity.HasOne(d => d.ProductId1Navigation).WithMany(p => p.RelatedProduct1Navigation)
            .HasForeignKey(d => d.ProductId1)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_RelatedProduct1Navigation");

            entity.HasOne(d => d.ProductId2Navigation).WithMany(p => p.RelatedProduct2Navigation)
            .HasForeignKey(d => d.ProductId2)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_RelatedProduct2Navigation");
        }
    }
}

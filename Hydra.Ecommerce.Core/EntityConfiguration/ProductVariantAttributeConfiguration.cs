using Hydra.Ecommerce.Core.Domain;
using Hydra.Ecommerce.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class ProductVariantAttributeConfiguration : IEntityTypeConfiguration<ProductVariantAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductVariantAttribute> entity)
        {
            entity.ToTable("ProductVariantAttribute", "Sale");

            entity.HasIndex(e => new { e.VariantId, e.AttributeId }, "IX_VariantAttribute_VariantId_AttributeId").IsUnique();

            entity.HasOne(d => d.Variant)
                .WithMany(v => v.VariantAttributes)
                .HasForeignKey(d => d.VariantId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Attribute)
                .WithMany(d=>d.VariantAttributes)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Leather Jacket (1004) - Black/Medium variant (2005)
            // Attribute 4 = Black (Color), Attribute 7 = Large (Size)
            entity.HasData(
                new ProductVariantAttribute { Id = 4000, VariantId = 2005, AttributeId = 4 },
                new ProductVariantAttribute { Id = 4001, VariantId = 2005, AttributeId = 7 },
                // Leather Jacket - Brown/Large variant (2006)
                // Attribute 1 = Blue used as proxy for Brown
                new ProductVariantAttribute { Id = 4002, VariantId = 2006, AttributeId = 1 },
                new ProductVariantAttribute { Id = 4003, VariantId = 2006, AttributeId = 8 },
                // T-Shirt (1005) - White/Small variant (2008)
                // Attribute 3 = White (Color), Attribute 5 = Small (Size)
                new ProductVariantAttribute { Id = 4004, VariantId = 2008, AttributeId = 3 },
                new ProductVariantAttribute { Id = 4005, VariantId = 2008, AttributeId = 5 },
                // T-Shirt - Black/Medium variant (2009)
                new ProductVariantAttribute { Id = 4006, VariantId = 2009, AttributeId = 4 },
                new ProductVariantAttribute { Id = 4007, VariantId = 2009, AttributeId = 6 },
                // T-Shirt - Red/Large variant (2010)
                // Attribute 2 = Red (Color), Attribute 7 = Large (Size)
                new ProductVariantAttribute { Id = 4008, VariantId = 2010, AttributeId = 2 },
                new ProductVariantAttribute { Id = 4009, VariantId = 2010, AttributeId = 7 }
            );
        }
    }
}

using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK_Product_Image_Mapping");

            entity.ToTable("ProductImage", "Sale");

            entity.HasIndex(e => e.ImageId, "IX_Product_Image_Mapping_ImageId");

            entity.HasIndex(e => e.ProductId, "IX_Product_Image_Mapping_ProductId");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_ProductImage_Product");

            entity.HasOne(a => a.Image)
               .WithMany() // Leave empty if FileUpload doesn't have a collection of Articles
               .HasForeignKey(a => a.ImageId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);

        }
    }
}

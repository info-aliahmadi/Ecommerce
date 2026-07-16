using Hydra.Ecommerce.Core.Domain;
using Hydra.Ecommerce.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class ProductAttributeConfiguration : IEntityTypeConfiguration<Domain.ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<Domain.ProductAttribute> entity)
        {
            entity.ToTable("ProductAttribute", "Sale");

            entity.HasIndex(e => e.Key, "IX_Attribute_Key").IsUnique();

            entity.HasIndex(e => e.DisplayOrder, "IX_Attribute_DisplayOrder");
            entity.HasIndex(e => e.AttributeType, "IX_Attribute_AttributeType");

            entity.Property(e => e.DisplayName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Key)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(300);

            entity.HasOne(a => a.ImagePreview)
               .WithMany()
               .HasForeignKey(a => a.ImagePreviewId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(a => a.VariantAttributes)
               .WithOne(x=>x.Attribute)
               .HasForeignKey(a => a.AttributeId)
               .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(
                new ProductAttribute { Id = 1, DisplayOrder = 1, DisplayName = "Blue", Key = "blue", AttributeType = AttributeType.Color },
                new ProductAttribute { Id = 2, DisplayOrder = 2, DisplayName = "Red", Key = "red", AttributeType = AttributeType.Color },
                new ProductAttribute { Id = 3, DisplayOrder = 3, DisplayName = "White", Key = "white", AttributeType = AttributeType.Color },
                new ProductAttribute { Id = 4, DisplayOrder = 4, DisplayName = "Black", Key = "black", AttributeType = AttributeType.Color }, 
                new ProductAttribute { Id = 8, DisplayOrder = 8, DisplayName = "Green", Key = "green", AttributeType = AttributeType.Color },
                new ProductAttribute { Id = 9, DisplayOrder = 9, DisplayName = "Yellow", Key = "yellow", AttributeType = AttributeType.Color },
                new ProductAttribute { Id = 10, DisplayOrder = 10, DisplayName = "Purple", Key = "purple", AttributeType = AttributeType.Color },
                new ProductAttribute { Id = 11, DisplayOrder = 11, DisplayName = "Extra Small", Key = "XS", AttributeType = AttributeType.Size, Description = "Extra Small size" },
                new ProductAttribute { Id = 5, DisplayOrder = 5, DisplayName = "Small size", Key = "S", AttributeType = AttributeType.Size, Description = "Small Means S US Size" },
                new ProductAttribute { Id = 6, DisplayOrder = 6, DisplayName = "Medium", Key = "M", AttributeType = AttributeType.Size, Description = "Small Means M US Size" },
                new ProductAttribute { Id = 7, DisplayOrder = 7, DisplayName = "Large", Key = "L", AttributeType = AttributeType.Size, Description = "Large Means L US Size" },
                new ProductAttribute { Id = 12, DisplayOrder = 12, DisplayName = "Extra Large", Key = "XL", AttributeType = AttributeType.Size, Description = "Extra Large size" },
                new ProductAttribute { Id = 13, DisplayOrder = 13, DisplayName = "Weekend Casual", Key = "weekend-casual", AttributeType = AttributeType.Style, Description = "Weekend Casual Style" },
                new ProductAttribute { Id = 14, DisplayOrder = 14, DisplayName = "Office Professional", Key = "office-professional", AttributeType = AttributeType.Style, Description = "Office Professional Style" },
                new ProductAttribute { Id = 15, DisplayOrder = 15, DisplayName = "Evening Elegance", Key = "evening-elegance", AttributeType = AttributeType.Style, Description = "Evening Elegance Style" });
        }
    }
}

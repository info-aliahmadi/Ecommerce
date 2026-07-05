using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> entity)
        {
            entity.ToTable("ProductAttribute", "Sale");

            entity.HasIndex(e => e.DisplayOrder, "IX_Attribute_DisplayOrder");
            entity.HasIndex(e => e.AttributeType, "IX_Attribute_AttributeType");

            entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);
            entity.Property(e => e.Value)
            .IsRequired()
            .HasMaxLength(100);
            entity.Property(e => e.Description)
            .HasMaxLength(300);


            entity.HasData(new ProductAttribute()
            {
                Id = 1,
                DisplayOrder = 1,
                Name = "Blue",
                Value = "blue",
                AttributeType = AttributeType.Color
            }, new ProductAttribute()
            {
                Id = 2,
                DisplayOrder = 2,
                Name = "Red",
                Value = "red",
                AttributeType = AttributeType.Color
            }, new ProductAttribute()
            {
                Id = 3,
                DisplayOrder = 3,
                Name = "White",
                Value = "#fff",
                AttributeType = AttributeType.Color
            }, new ProductAttribute()
            {
                Id = 4,
                DisplayOrder = 4,
                Name = "Black",
                Value = "#000",
                AttributeType = AttributeType.Color
            }, new ProductAttribute()
            {
                Id = 5,
                DisplayOrder = 5,
                Name = "Small size",
                Value = "#Small",
                AttributeType = AttributeType.Size,
                Description = "Small Means S US Size"
            }, new ProductAttribute()
            {
                Id = 6,
                DisplayOrder = 6,
                Name = "Medium",
                Value = "#Medium",
                AttributeType = AttributeType.Size,
                Description = "Small Means M US Size"
            }, new ProductAttribute()
            {
                Id = 7,
                DisplayOrder = 7,
                Name = "Large",
                Value = "#Large",
                AttributeType = AttributeType.Size,
                Description = "Small Means XL US Size"
            }, new ProductAttribute()
            {
                Id = 8,
                DisplayOrder = 8,
                Name = "Green",
                Value = "green",
                AttributeType = AttributeType.Color
            }, new ProductAttribute()
            {
                Id = 9,
                DisplayOrder = 9,
                Name = "Yellow",
                Value = "yellow",
                AttributeType = AttributeType.Color
            }, new ProductAttribute()
            {
                Id = 10,
                DisplayOrder = 10,
                Name = "Purple",
                Value = "purple",
                AttributeType = AttributeType.Color
            }, new ProductAttribute()
            {
                Id = 11,
                DisplayOrder = 11,
                Name = "Extra Small",
                Value = "XS",
                AttributeType = AttributeType.Size,
                Description = "Extra Small size"
            }, new ProductAttribute()
            {
                Id = 12,
                DisplayOrder = 12,
                Name = "Extra Large",
                Value = "XL",
                AttributeType = AttributeType.Size,
                Description = "Extra Large size"
            }, new ProductAttribute()
            {
                Id = 13,
                DisplayOrder = 13,
                Name = "Weekend Casual",
                Value = "weekend-casual",
                AttributeType = AttributeType.Style,
                Description = "Weekend Casual Style"
            }, new ProductAttribute()
            {
                Id = 14,
                DisplayOrder = 14,
                Name = "Office Professional",
                Value = "office-professional",
                AttributeType = AttributeType.Style,
                Description = "Office Professional Style"
            }, new ProductAttribute()
            {
                Id = 15,
                DisplayOrder = 15,
                Name = "Evening Elegance",
                Value = "evening-elegance",
                AttributeType = AttributeType.Style,
                Description = "Evening Elegance Style"
            });
        }
    }
}

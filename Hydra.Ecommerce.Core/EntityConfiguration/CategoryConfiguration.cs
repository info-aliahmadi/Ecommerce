using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.ToTable("Category", "Sale");

            entity.HasIndex(e => e.DisplayOrder);

            entity.HasIndex(e => e.ParentCategoryId);

            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.MetaDescription).HasMaxLength(300);
            entity.Property(e => e.MetaKeywords).HasMaxLength(250);
            entity.Property(e => e.MetaTitle).HasMaxLength(250);
            entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(70);
            entity.Property(e => e.Key)
            .IsRequired()
            .HasMaxLength(70);
            entity.Property(e => e.UpdatedOnUtc).HasPrecision(6);

            entity.HasOne(d => d.ParentCategory).WithMany()
            .HasForeignKey(d => d.ParentCategoryId);

            entity.HasOne(a => a.ImagePreview)
               .WithMany() // Leave empty if FileUpload doesn't have a collection of Articles
               .HasForeignKey(a => a.ImagePreviewId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);

            entity.HasData(
                new Category()
                {
                    ParentCategoryId = null,
                    Id = 3,
                    DisplayOrder = 3,
                    Name = "Electronics",
                    Key = "electronics",
                    MetaDescription = "Electronic devices and accessories",
                    MetaKeywords = "electronics, devices, gadgets",
                    MetaTitle = "Electronics",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = true,
                    Description = "Electronic products category",
                    Published = true,
                    Color = "#6A5ACD"
                },
                // Computers is child of Electronics
                new Category()
                {
                    Id = 4,
                    DisplayOrder = 4,
                    Name = "Fashion",
                    Key = "fashion",
                    MetaDescription = "Fashion accessories",
                    MetaKeywords = "Fashion components",
                    MetaTitle = "Fashion",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = false,
                    Description = "Fashion products",
                    Published = true,
                    Color = "#E63946"
                },
                // Laptops is child of Computers
                new Category()
                {
                    ParentCategoryId = null,
                    Id = 5,
                    DisplayOrder = 5,
                    Name = "Home & Living",
                    Key = "home-living",
                    MetaDescription = "Home products, Living products",
                    MetaKeywords = "Home products, Living products",
                    MetaTitle = "Home products, Living products",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = false,
                    Description = "Home products, Living products",
                    Published = true,
                    Color = "#20B2AA"
                },
                // Phones is child of Electronics
                new Category()
                {
                    ParentCategoryId = null,
                    Id = 6,
                    DisplayOrder = 6,
                    Name = "Sports",
                    Key = "sports",
                    MetaDescription = "Sports products",
                    MetaKeywords = "Sports products",
                    MetaTitle = "Sports products",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = true,
                    Description = "Sports products",
                    Published = true,
                    Color = "#FFC107"
                },
                // Accessories is child of Electronics
                new Category()
                {
                    ParentCategoryId = null,
                    Id = 7,
                    DisplayOrder = 7,
                    Name = "Beauty",
                    Key = "beauty",
                    MetaDescription = "Beauty products",
                    MetaKeywords = "Beauty products",
                    MetaTitle = "Beauty products",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = false,
                    Description = "Beauty products",
                    Published = true,
                    Color = "#FF69B4"
                },
                // Home Appliances root category
                new Category()
                {
                    ParentCategoryId = null,
                    Id = 8,
                    DisplayOrder = 8,
                    Name = "Books",
                    Key = "books",
                    MetaDescription = "Books",
                    MetaKeywords = "Books",
                    MetaTitle = "Books",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = true,
                    Description = "Books",
                    Published = true,
                    Color = "#10B981"
                }
            );
        }
    }
}

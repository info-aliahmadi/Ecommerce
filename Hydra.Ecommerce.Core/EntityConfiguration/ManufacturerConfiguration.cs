using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> entity)
        {
            entity.ToTable("Manufacturer", "Sale");

            entity.HasIndex(e => e.DisplayOrder, "IX_Manufacturer_DisplayOrder");

            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.MetaDescription).HasMaxLength(300);
            entity.Property(e => e.MetaKeywords).HasMaxLength(250);
            entity.Property(e => e.MetaTitle).HasMaxLength(250);
            entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(70);

            entity.HasOne(a => a.ImagePreview)
               .WithMany() // Leave empty if FileUpload doesn't have a collection of Articles
               .HasForeignKey(a => a.ImagePreviewId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);

            entity.HasData(
                new Manufacturer()
                {
                    Id = 3,
                    DisplayOrder = 3,
                    Name = "Samsung",
                    MetaDescription = "Samsung electronics",
                    MetaKeywords = "samsung, electronics",
                    MetaTitle = "Samsung",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    Description = "Samsung Electronics",
                    Published = true
                },
                new Manufacturer()
                {
                    Id = 4,
                    DisplayOrder = 4,
                    Name = "Apple",
                    MetaDescription = "Apple products",
                    MetaKeywords = "apple, iphone, mac",
                    MetaTitle = "Apple",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    Description = "Apple Inc.",
                    Published = true
                },
                new Manufacturer()
                {
                    Id = 5,
                    DisplayOrder = 5,
                    Name = "LG",
                    MetaDescription = "LG Electronics",
                    MetaKeywords = "lg, electronics",
                    MetaTitle = "LG",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    Description = "LG Electronics",
                    Published = true
                },
                new Manufacturer()
                {
                    Id = 6,
                    DisplayOrder = 6,
                    Name = "Sony",
                    MetaDescription = "Sony electronics",
                    MetaKeywords = "sony, audio, tv",
                    MetaTitle = "Sony",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    Description = "Sony Corporation",
                    Published = true
                },
                new Manufacturer()
                {
                    Id = 7,
                    DisplayOrder = 7,
                    Name = "Bosch",
                    MetaDescription = "Bosch home appliances",
                    MetaKeywords = "bosch, appliances",
                    MetaTitle = "Bosch",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    Description = "Bosch Home Appliances",
                    Published = true
                },
                new Manufacturer()
                {
                    Id = 8,
                    DisplayOrder = 8,
                    Name = "Ikea",
                    MetaDescription = "IKEA furniture",
                    MetaKeywords = "ikea, furniture",
                    MetaTitle = "Ikea",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    Description = "IKEA",
                    Published = true
                }
            );

        }
    }
}

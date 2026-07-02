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
            entity.Property(e => e.NormalizedName)
            .IsRequired()
            .HasMaxLength(70);
            entity.Property(e => e.UpdatedOnUtc).HasPrecision(6);

            entity.HasOne(d => d.ParentCategory).WithMany()
            .HasForeignKey(d => d.ParentCategoryId);

            entity.HasData(
                new Category()
                {
                    Id = 3,
                    DisplayOrder = 3,
                    Name = "Electronics",
                    NormalizedName = "electronics",
                    MetaDescription = "Electronic devices and accessories",
                    MetaKeywords = "electronics, devices, gadgets",
                    MetaTitle = "Electronics",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = true,
                    Description = "Electronic products category",
                    Published = true
                },
                // Computers is child of Electronics
                new Category()
                {
                    Id = 4,
                    DisplayOrder = 4,
                    Name = "Computers",
                    NormalizedName = "computers",
                    ParentCategoryId = 3,
                    MetaDescription = "Desktops, components and accessories",
                    MetaKeywords = "computers, desktops, components",
                    MetaTitle = "Computers",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = false,
                    Description = "Computers and related products",
                    Published = true
                },
                // Laptops is child of Computers
                new Category()
                {
                    Id = 5,
                    DisplayOrder = 5,
                    Name = "Laptops",
                    NormalizedName = "laptops",
                    ParentCategoryId = 4,
                    MetaDescription = "Portable computers and notebooks",
                    MetaKeywords = "laptops, notebooks, ultrabooks",
                    MetaTitle = "Laptops",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = false,
                    Description = "Laptops and portable computers",
                    Published = true
                },
                // Phones is child of Electronics
                new Category()
                {
                    Id = 6,
                    DisplayOrder = 6,
                    Name = "Mobile Phones",
                    NormalizedName = "mobilephones",
                    ParentCategoryId = 3,
                    MetaDescription = "Smartphones and mobile devices",
                    MetaKeywords = "phones, smartphones, mobiles",
                    MetaTitle = "Mobile Phones",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = true,
                    Description = "Mobile phones and accessories",
                    Published = true
                },
                // Accessories is child of Electronics
                new Category()
                {
                    Id = 7,
                    DisplayOrder = 7,
                    Name = "Accessories",
                    NormalizedName = "accessories",
                    ParentCategoryId = 3,
                    MetaDescription = "Electronics accessories",
                    MetaKeywords = "accessories, chargers, cases",
                    MetaTitle = "Accessories",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = false,
                    Description = "Accessories for electronic devices",
                    Published = true
                },
                // Home Appliances root category
                new Category()
                {
                    Id = 8,
                    DisplayOrder = 8,
                    Name = "Home Appliances",
                    NormalizedName = "home-appliances",
                    MetaDescription = "Appliances for home use",
                    MetaKeywords = "home, appliances, kitchen",
                    MetaTitle = "Home Appliances",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = true,
                    Description = "Home and kitchen appliances",
                    Published = true
                },
                // Kitchen as child of Home Appliances
                new Category()
                {
                    Id = 9,
                    DisplayOrder = 9,
                    Name = "Kitchen",
                    NormalizedName = "kitchen",
                    ParentCategoryId = 8,
                    MetaDescription = "Kitchen appliances and tools",
                    MetaKeywords = "kitchen, appliances, cookware",
                    MetaTitle = "Kitchen",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = false,
                    Description = "Kitchen related appliances",
                    Published = true
                },
                // TV & Video as child of Electronics
                new Category()
                {
                    Id = 10,
                    DisplayOrder = 10,
                    Name = "TV & Video",
                    NormalizedName = "tv-video",
                    ParentCategoryId = 3,
                    MetaDescription = "Televisions and video equipment",
                    MetaKeywords = "tv, video, televisions",
                    MetaTitle = "TV & Video",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = false,
                    Description = "Televisions, projectors and video accessories",
                    Published = true
                },
                // Audio as child of Electronics
                new Category()
                {
                    Id = 11,
                    DisplayOrder = 11,
                    Name = "Audio",
                    NormalizedName = "audio",
                    ParentCategoryId = 3,
                    MetaDescription = "Speakers, headphones and audio devices",
                    MetaKeywords = "audio, speakers, headphones",
                    MetaTitle = "Audio",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = false,
                    Description = "Audio equipment and accessories",
                    Published = true
                },
                // Wearables as child of Electronics
                new Category()
                {
                    Id = 12,
                    DisplayOrder = 12,
                    Name = "Wearables",
                    NormalizedName = "wearables",
                    ParentCategoryId = 3,
                    MetaDescription = "Smartwatches and fitness trackers",
                    MetaKeywords = "wearables, smartwatches, trackers",
                    MetaTitle = "Wearables",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = false,
                    Description = "Wearable devices and accessories",
                    Published = true
                },
                // Small Appliances as child of Home Appliances
                new Category()
                {
                    Id = 13,
                    DisplayOrder = 13,
                    Name = "Small Appliances",
                    NormalizedName = "small-appliances",
                    ParentCategoryId = 8,
                    MetaDescription = "Small kitchen and home appliances",
                    MetaKeywords = "small appliances, blenders, toasters",
                    MetaTitle = "Small Appliances",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = false,
                    Description = "Small home and kitchen appliances",
                    Published = true
                },
                // Furniture root category
                new Category()
                {
                    Id = 14,
                    DisplayOrder = 14,
                    Name = "Furniture",
                    NormalizedName = "furniture",
                    MetaDescription = "Home and office furniture",
                    MetaKeywords = "furniture, sofa, table",
                    MetaTitle = "Furniture",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = true,
                    Description = "Furniture for home and office",
                    Published = true
                },
                // Living Room as child of Furniture
                new Category()
                {
                    Id = 15,
                    DisplayOrder = 15,
                    Name = "Living Room",
                    NormalizedName = "living-room",
                    ParentCategoryId = 14,
                    MetaDescription = "Sofas, coffee tables and more",
                    MetaKeywords = "living room, sofa, coffee table",
                    MetaTitle = "Living Room",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = false,
                    Description = "Living room furniture",
                    Published = true
                },
                // Outdoor & Garden root category
                new Category()
                {
                    Id = 16,
                    DisplayOrder = 16,
                    Name = "Outdoor & Garden",
                    NormalizedName = "outdoor-garden",
                    MetaDescription = "Garden tools and outdoor equipment",
                    MetaKeywords = "garden, outdoor, tools",
                    MetaTitle = "Outdoor & Garden",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    Deleted = false,
                    ShowOnHomepage = false,
                    Description = "Outdoor and garden supplies",
                    Published = true
                }
            );
        }
    }
}

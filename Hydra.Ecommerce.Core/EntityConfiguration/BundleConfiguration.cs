using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class BundleConfiguration : IEntityTypeConfiguration<Bundle>
    {
        public void Configure(EntityTypeBuilder<Bundle> entity)
        {
            entity.ToTable("Bundle", "Sale");

            entity.HasIndex(e => e.DisplayOrder);

            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

            entity.Property(e => e.CreatedOnUtc).HasPrecision(6);


            entity.HasData(
                new Bundle()
                {
                    Id = 1,
                    DisplayOrder = 1,
                    Name = "Summer Essentials Bundle",
                    Description = "Everything you need for the perfect summer",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    ShowOnHomepage = true,
                    ProductBundles = new List<ProductBundle>()
                    {
                        new ProductBundle()
                        {
                            ProductId = 1001,
                            DisplayOrder = 1
                        },
                        new ProductBundle()
                        {
                            ProductId = 1002,
                            DisplayOrder = 2
                        },
                        new ProductBundle()
                        {
                            ProductId = 1003,
                            DisplayOrder = 3
                        }
                    }
                },
                new Bundle()
                {
                    Id = 2,
                    DisplayOrder = 2,
                    Name = "Tech Workspace Bundle",
                    Description = "Level up your home office setup",
                    CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    ShowOnHomepage = true,
                    ProductBundles = new List<ProductBundle>()
                    {
                        new ProductBundle()
                        {
                            ProductId = 1004,
                            DisplayOrder = 1
                        },
                        new ProductBundle()
                        {
                            ProductId = 1005,
                            DisplayOrder = 2
                        },
                        new ProductBundle()
                        {
                            ProductId = 1006,
                            DisplayOrder = 3
                        }
                    }
                },
               new Bundle()
               {
                   Id = 3,
                   DisplayOrder = 3,
                   Name = "Self-Care Ritual Bundlee",
                   Description = "Treat yourself to a spa experience at home",
                   CreatedOnUtc = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                   ShowOnHomepage = true,
                   ProductBundles = new List<ProductBundle>()
                    {
                        new ProductBundle()
                        {
                            ProductId = 1007,
                            DisplayOrder = 1
                        },
                        new ProductBundle()
                        {
                            ProductId = 1008,
                            DisplayOrder = 2
                        },
                        new ProductBundle()
                        {
                            ProductId = 1009,
                            DisplayOrder = 3
                        }
                    }
               }
             );
        }
    }
}

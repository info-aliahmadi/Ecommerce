using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> entity)
        {
            entity.ToTable("ProductTag", "Sale");

            entity.HasIndex(e => e.Name, "IX_ProductTag_Name");

            entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(70);

            entity.HasData(new ProductTag()
            {
                Id = 1,
                Name = "Bestseller",
                NormalizedName = "bestseller"
            }, new ProductTag()
            {
                Id = 2,
                Name = "New",
                NormalizedName = "new"
            }, new ProductTag()
            {
                Id = 3,
                Name = "Popular",
                NormalizedName = "popular"
            }, new ProductTag()
            {
                Id = 4,
                Name = "Sale",
                NormalizedName = "sale"
            }, new ProductTag()
            {
                Id = 5,
                Name = "Sustainable",
                NormalizedName = "sustainable"
            }, new ProductTag()
            {
                Id = 6,
                Name = "Trending",
                NormalizedName = "trending"
            });
        }
    }
}

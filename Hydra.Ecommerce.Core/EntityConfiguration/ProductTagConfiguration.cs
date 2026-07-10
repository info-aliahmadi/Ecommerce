using Hydra.Ecommerce.Core.Domain;
using Hydra.Ecommerce.Core.Enums;
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

            entity.Property(e => e.Key)
            .IsRequired()
            .HasMaxLength(70);

            entity.HasData(new ProductTag()
            {
                Id = (int)ProductTagType.Bestseller,
                Name = "Bestseller",
                Key = "bestseller"
            }, new ProductTag()
            {
                Id = (int)ProductTagType.Popular,
                Name = "Popular",
                Key = "popular"
            }, new ProductTag()
            {
                Id = (int)ProductTagType.Sale,
                Name = "Sale",
                Key = "sale"
            }, new ProductTag()
            {
                Id = (int)ProductTagType.Sustainable,
                Name = "Sustainable",
                Key = "sustainable"
            }, new ProductTag()
            {
                Id = (int)ProductTagType.Trending,
                Name = "Trending",
                Key = "trending"
            }, new ProductTag()
            {
                Id = (int)ProductTagType.Featured,
                Name = "Featured",
                Key = "Featured"
            });
        }
    }
}

using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class ProductProductTagConfiguration : IEntityTypeConfiguration<ProductProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductProductTag> builder)
        {
            builder.ToTable("ProductProductTag", "Sale");

            builder.HasKey(p => p.Id);

            builder.HasIndex(e => new { e.ProductTagId, e.ProductId }, "IX_PCM_Product_and_Tag").IsUnique();

            builder.HasOne(x => x.Product).WithMany(x => x.ProductProductTags).HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.ProductTag).WithMany(x => x.ProductProductTags).HasForeignKey(x => x.ProductTagId);

        }
    }
}

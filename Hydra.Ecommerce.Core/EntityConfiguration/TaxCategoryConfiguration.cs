using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class TaxCategoryConfiguration : IEntityTypeConfiguration<TaxCategory>
    {
        public void Configure(EntityTypeBuilder<TaxCategory> entity)
        {
            entity.ToTable("TaxCategory", "Sale");

            entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

            entity.HasData(new TaxCategory()
            {
                Id = 1,
                Name = "5% Tax",
                DisplayOrder = 1
            }, new TaxCategory()
            {
                Id = 2,
                Name = "9% Tax",
                DisplayOrder = 2
            }, new TaxCategory()
            {
                Id = 3,
                Name = "20% Tax",
                DisplayOrder = 3
            });
        }
    }
}

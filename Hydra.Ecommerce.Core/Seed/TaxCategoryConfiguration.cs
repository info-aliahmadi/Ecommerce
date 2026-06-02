using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.Seed.EntityConfiguration
{
    public class TaxCategoryConfiguration : IEntityTypeConfiguration<TaxCategory>
    {
        public void Configure(EntityTypeBuilder<TaxCategory> entity)
        {
            entity.HasData(new TaxCategory()
            {
                Id = 1,
                Name = "0% Tax",
                DisplayOrder = 1
            }, new TaxCategory()
            {
                Id = 2,
                Name = "2% Tax",
                DisplayOrder = 2
            }, new TaxCategory()
            {
                Id = 5,
                Name = "5% Tax",
                DisplayOrder = 3
            }, new TaxCategory()
            {
                Id = 9,
                Name = "9% Tax",
                DisplayOrder = 4
            }, new TaxCategory()
            {
                Id = 20,
                Name = "20% Tax",
                DisplayOrder = 5
            });
        }
    }
}

using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.Seed.EntityConfiguration
{
    public class TaxRateConfiguration : IEntityTypeConfiguration<TaxRate>
    {
        public void Configure(EntityTypeBuilder<TaxRate> entity)
        {
            entity.HasData(new TaxRate()
            {
                Id = 1,
                CountryId = 100,
                TaxCategoryId = 1,
                Percentage = 0
            }, new TaxRate()
            {
                Id = 2,
                CountryId = 100,
                TaxCategoryId = 2,
                Percentage = 2
            }, new TaxRate()
            {
                Id = 3,
                CountryId = 100,
                TaxCategoryId = 5,
                Percentage = 5
            }, new TaxRate()
            {
                Id = 4,
                CountryId = 100,
                TaxCategoryId = 9,
                Percentage = 9
            }, new TaxRate()
            {
                Id = 5,
                CountryId = 100,
                TaxCategoryId = 20,
                Percentage = 20
            });
        }
    }
}

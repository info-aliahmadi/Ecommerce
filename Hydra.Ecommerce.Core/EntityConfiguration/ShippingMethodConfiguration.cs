using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class ShippingMethodConfiguration : IEntityTypeConfiguration<ShippingMethod>
    {
        public void Configure(EntityTypeBuilder<ShippingMethod> entity)
        {
            entity.ToTable("ShippingMethod", "Sale");

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(70);
        }
    }
}

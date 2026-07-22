using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> entity)
        {
            entity.ToTable("Address", "Sale");

            entity.HasIndex(e => e.CountryId);

            entity.HasIndex(e => e.StateProvinceId);

            entity.Property(e => e.Address1).HasMaxLength(300);
            entity.Property(e => e.Title).HasMaxLength(80);
            entity.Property(e => e.GeoLocation).HasMaxLength(40);
            entity.Property(e => e.City)
            .IsRequired()
            .HasMaxLength(70);
            entity.Property(e => e.County).HasMaxLength(70);
            entity.Property(e => e.CreatedOnUtc).HasPrecision(6);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.ZipPostalCode).HasMaxLength(30);

            entity.HasOne(d => d.Country).WithMany(p => p.Addresses)
            .HasForeignKey(d => d.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.StateProvince).WithMany(p => p.Addresses)
            .HasForeignKey(d => d.StateProvinceId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.User).WithMany()
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict);


        }
    }
}

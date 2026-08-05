using Hydra.Ecommerce.Core.Domain;
using Hydra.Ecommerce.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> entity)
        {
            entity.ToTable("Discount", "Sale");

            entity.Property(e => e.CouponCode).HasMaxLength(100);
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.EndDateUtc).HasPrecision(6);
            entity.Property(e => e.MaximumDiscountAmount).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);
            entity.Property(e => e.StartDateUtc).HasPrecision(6);

            entity.HasMany(d => d.DiscountCategories).WithOne(p => p.Discount).HasForeignKey(x => x.DiscountId);

            entity.HasMany(d => d.DiscountManufacturers).WithOne(p => p.Discount).HasForeignKey(x => x.DiscountId);

            entity.HasMany(d => d.DiscountProducts).WithOne(p => p.Discount).HasForeignKey(x => x.DiscountId);

          
            entity.HasData(new Discount()
            {
                Id = 1,
                Name = "Discount 1",
                CouponCode = "CoponCode1",
                AdminComment = "AdminComment",
                DiscountTypeId = DiscountType.AssignedToCategories,
                UsePercentage = true,
                DiscountPercentage = 4,
                DiscountAmount = 0,
                RequiresCouponCode = true,
                DiscountLimitationId = DiscountLimitationType.Unlimited,
                LimitationTimes = 1,
                IsActive = true


            }, new Discount()
            {
                Id = 2,
                Name = "Discount 2",
                CouponCode = "CoponCode2",
                AdminComment = "AdminComment",
                DiscountTypeId = DiscountType.AssignedToCategories,
                UsePercentage = true,
                DiscountPercentage = 6,
                DiscountAmount = 0,
                RequiresCouponCode = true,
                DiscountLimitationId = DiscountLimitationType.NTimesOnly,
                LimitationTimes = 1,
                IsActive = true
            });

        }
    }
}

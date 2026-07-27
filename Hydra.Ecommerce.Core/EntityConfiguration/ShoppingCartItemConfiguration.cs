using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> entity)
        {
            entity.ToTable("ShoppingCartItem", "Sale");

            entity.HasIndex(e => e.Id, "IX_ShoppingCartItem");

            entity.Property(e => e.CreatedOnUtc).HasPrecision(6);
            entity.Property(e => e.UpdatedOnUtc).HasPrecision(6);

            entity.HasOne(d => d.ProductVariant).WithMany(p => p.ShoppingCartItems)
            .HasForeignKey(d => d.ProductVariantId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_ShoppingCartItem_ProductVariant");

            entity.HasOne(d => d.User).WithMany()
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_ShoppingCartItem_User");
        }
    }
}

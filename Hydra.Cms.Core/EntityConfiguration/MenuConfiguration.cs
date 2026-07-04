using Hydra.Cms.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hydra.Cms.Core.EntityConfiguration
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu", "Cms");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Title).HasMaxLength(100);

            builder.Property(o => o.Url).HasMaxLength(300);

            builder.Property(o => o.Color).HasMaxLength(10);

            builder.HasData(
             new Menu()
             {
                 Id = 1,
                 Order = 1,
                 Title = "Home",
                 Url = "/",

             },
             // Shop (Parent)
             new Menu()
             {
                 Id = 2,
                 Order = 2,
                 Title = "Shop",
                 Url = "/Products",

             },
                // Shop Children
                new Menu()
                {
                    ParentId = 2,
                    Id = 22,
                    Order = 2,
                    Title = "Electronics",
                    Url = "/products/?category=electronics",
                    Color = "#6A5ACD"

                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 23,
                    Order = 3,
                    Title = "Fashion",
                    Url = "/products/?category=fashion",
                    Color = "#E63946"

                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 24,
                    Order = 3,
                    Title = "Home & Living",
                    Url = "/products/?category=home-living",
                    Color = "#20B2AA"

                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 25,
                    Order = 3,
                    Title = "Sports",
                    Url = "/products/?category=sports",
                    Color = "#FFC107"

                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 26,
                    Order = 3,
                    Title = "Beauty",
                    Url = "/products/?category=beauty",
                    Color = "#FF69B4"

                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 27,
                    Order = 3,
                    Title = "Books",
                    Url = "/products/?category=books",
                    Color = "#10B981"

                },
            new Menu()
            {
                Id = 3,
                Order = 3,
                Title = "Discover",
                Url = "/products",

            },
            new Menu()
            {
                Id = 4,
                Order = 4,
                Title = "Deals",
                Url = "/products/?sorting=price-lower",

            });

        }
    }
}

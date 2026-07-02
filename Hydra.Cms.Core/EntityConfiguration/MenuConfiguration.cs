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

            builder.HasData(
             new Menu()
             {
                 Id = 1,
                 Order = 1,
                 Title = "Home",
                 Url = "/",
                 UserId = 1,
             },
             // Shop (Parent)
             new Menu()
             {
                 Id = 2,
                 Order = 2,
                 Title = "Shop",
                 Url = "/Products",
                 UserId = 1,
             },
                // Shop Children
                new Menu()
                {
                    ParentId = 2,
                    Id = 22,
                    Order = 2,
                    Title = "Electronics",
                    Url = "/products/?category=electronics",
                    UserId = 1,
                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 23,
                    Order = 3,
                    Title = "Computers",
                    Url = "/products/?category=computers",
                    UserId = 1,
                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 24,
                    Order = 3,
                    Title = "Laptops",
                    Url = "/products/?category=laptops",
                    UserId = 1,
                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 25,
                    Order = 3,
                    Title = "Mobile Phones",
                    Url = "/products/?category=mobilephones",
                    UserId = 1,
                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 26,
                    Order = 3,
                    Title = "Accessories",
                    Url = "/products/?category=accessories",
                    UserId = 1,
                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 27,
                    Order = 3,
                    Title = "Home Appliances",
                    Url = "/products/?category=home-appliances",
                    UserId = 1,
                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 28,
                    Order = 3,
                    Title = "Kitchen",
                    Url = "/products/?category=home-kitchen",
                    UserId = 1,
                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 29,
                    Order = 3,
                    Title = "TV & Video",
                    Url = "/products/?category=tv-video",
                    UserId = 1,
                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 30,
                    Order = 3,
                    Title = "Audio",
                    Url = "/products/?category=audio",
                    UserId = 1,
                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 31,
                    Order = 3,
                    Title = "Small Appliances",
                    Url = "/products/?category=small-appliances",
                    UserId = 1,
                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 32,
                    Order = 3,
                    Title = "Furniture",
                    Url = "/products/?category=furniture",
                    UserId = 1,
                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 33,
                    Order = 3,
                    Title = "Living Room",
                    Url = "/products/?category=living-room",
                    UserId = 1,
                },
                new Menu()
                {
                    ParentId = 2,
                    Id = 34,
                    Order = 3,
                    Title = "Outdoor & Garden",
                    Url = "/products/?category=outdoor-garden",
                    UserId = 1,
                },

            new Menu()
            {
                Id = 3,
                Order = 3,
                Title = "Discover",
                Url = "/products",
                UserId = 1,
            },
            new Menu()
            {
                Id = 4,
                Order = 4,
                Title = "Deals",
                Url = "/products/?tag=outdoor-garden",
                UserId = 1,
            },
            new Menu()
            {
                Id = 5,
                Order = 5,
                Title = "Contact",
                Url = "/Contact",
                UserId = 1,
            },
            new Menu()
            {
                Id = 6,
                Order = 6,
                Title = "Blog",
                Url = "/Blog",
                UserId = 1,
            });

        }
    }
}

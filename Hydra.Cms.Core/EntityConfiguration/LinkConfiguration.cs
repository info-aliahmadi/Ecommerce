using Hydra.Cms.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hydra.Cms.Core.EntityConfiguration
{
    public class LinkConfiguration : IEntityTypeConfiguration<Link>
    {
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder.ToTable("Link", "Cms");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Title).HasMaxLength(300);
            builder.Property(o => o.Url).HasMaxLength(300);
            builder.Property(o => o.Description).HasMaxLength(300);

            builder.HasOne(x => x.LinkSection).WithMany(x => x.Links).HasForeignKey(x => x.LinkSectionId);

            builder.HasOne(a => a.ImagePreview)
               .WithMany() // Leave empty if FileUpload doesn't have a collection of Articles
               .HasForeignKey(a => a.ImagePreviewId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);


            // seed some public links
            builder.HasData(
                // Shop Section
                new Link()
                {
                    LinkSectionId = 1,
                    Id = 11,
                    Title = "All Products",
                    Url = "/products",
                    Description = "Browse all products",
                    Order = 1,
                    
                    ImagePreviewId = null
                },
                new Link()
                {
                    LinkSectionId = 1,
                    Id = 12,
                    Title = "New Arrivals",
                    Url = "/products?sorting=date-new",
                    Description = "New Products",
                    Order = 2,
                    
                    ImagePreviewId = null
                },
                new Link()
                {
                    LinkSectionId = 1,
                    Id = 13,
                    Title = "Best Sellers",
                    Url = "/products?sorting=orders-high",
                    Description = "Products with most sell",
                    Order = 3,
                    
                    ImagePreviewId = null
                },
                new Link()
                {
                    LinkSectionId = 1,
                    Id = 14,
                    Title = "Deals & Offers",
                    Url = "/products/?sorting=price-lower",
                    Description = "Recommended Products",
                    Order = 4,
                    
                    ImagePreviewId = null
                },
                new Link()
                {
                    LinkSectionId = 1,
                    Id = 15,
                    Title = "Gift Cards",
                    Url = "/products",
                    Description = "Gift Cards",
                    Order = 5,
                    
                    ImagePreviewId = null
                },
                // Support Section
                new Link()
                {
                    LinkSectionId = 2,
                    Id = 21,
                    Title = "Help Center",
                    Url = "/pages/help",
                    Description = "Help Center",
                    Order = 1,
                    
                    ImagePreviewId = null
                },
                new Link()
                {
                    LinkSectionId = 2,
                    Id = 22,
                    Title = "Shipping Info",
                    Url = "/pages/shipping-info",
                    Description = "Shipping Info",
                    Order = 2,
                    
                    ImagePreviewId = null
                },
                new Link()
                {
                    LinkSectionId = 2,
                    Id = 23,
                    Title = "Returns",
                    Url = "/pages/returns",
                    Description = "Returns Rules",
                    Order = 3,
                    
                    ImagePreviewId = null
                },
                new Link()
                {
                    LinkSectionId = 2,
                    Id = 24,
                    Title = "Order Tracking",
                    Url = "/profile",
                    Description = "Order Tracking",
                    Order = 4,
                    
                    ImagePreviewId = null
                },
                new Link()
                {
                    LinkSectionId = 2,
                    Id = 25,
                    Title = "Contact Us",
                    Url = "/contact-us",
                    Description = "Contact Us",
                    Order = 5,
                    
                    ImagePreviewId = null
                },
                // Company Section
                new Link()
                {
                    LinkSectionId = 3,
                    Id = 31,
                    Title = "About Us",
                    Url = "/pages/about-us",
                    Description = "About Us",
                    Order = 1,
                    
                    ImagePreviewId = null
                },
                new Link()
                {
                    LinkSectionId = 3,
                    Id = 32,
                    Title = "Careers",
                    Url = "/pages/about",
                    Description = "About",
                    Order = 2,
                    
                    ImagePreviewId = null
                },
                new Link()
                {
                    LinkSectionId = 3,
                    Id = 33,
                    Title = "Press",
                    Url = "/pages/about",
                    Description = "About",
                    Order = 3,
                    
                    ImagePreviewId = null
                },
                new Link()
                {
                    LinkSectionId = 3,
                    Id = 34,
                    Title = "Privacy Policy",
                    Url = "/pages/privacy-policy",
                    Description = "Privacy Policy",
                    Order = 4,
                    
                    ImagePreviewId = null
                },
                new Link()
                {
                    LinkSectionId = 3,
                    Id = 35,
                    Title = "Terms of Service",
                    Url = "/pages/terms-service",
                    Description = "Terms of Service",
                    Order = 5,
                    
                    ImagePreviewId = null
                }
            );

        }
    }
}

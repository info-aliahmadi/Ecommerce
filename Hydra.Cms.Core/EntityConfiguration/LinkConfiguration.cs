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

            // seed some public links
            builder.HasData(
                new Link()
                {
                    Id = 1,
                    Title = "All Categories",
                    Url = "/categories",
                    Description = "Browse all product categories",
                    LinkSectionId = 1,
                    Order = 1,
                    UserId = 1,
                    ImagePreviewId = null
                },
                new Link()
                {
                    Id = 2,
                    Title = "Latest Posts",
                    Url = "/blog",
                    Description = "Read our latest blog posts",
                    LinkSectionId = 2,
                    Order = 1,
                    UserId = 1,
                    ImagePreviewId = null
                },
                new Link()
                {
                    Id = 3,
                    Title = "Deals & Offers",
                    Url = "/deals",
                    Description = "Check current deals and offers",
                    LinkSectionId = 1,
                    Order = 2,
                    UserId = 1,
                    ImagePreviewId = null
                },
                new Link()
                {
                    Id = 4,
                    Title = "Editor Picks",
                    Url = "/editor-picks",
                    Description = "Recommended reads",
                    LinkSectionId = 2,
                    Order = 2,
                    UserId = 1,
                    ImagePreviewId = null
                }
            );

        }
    }
}

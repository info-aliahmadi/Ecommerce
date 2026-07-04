using Hydra.Cms.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hydra.Cms.Core.EntityConfiguration
{
    public class LinkSectionConfiguration : IEntityTypeConfiguration<LinkSection>
    {
        public void Configure(EntityTypeBuilder<LinkSection> builder)
        {
            builder.ToTable("LinkSection", "Cms");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Title).HasMaxLength(300);

            builder.HasData(new LinkSection()
            {
                Id = 1,
                Title = "Shop",
                Key = "shop",
                IsVisible = true,
            }, new LinkSection()
            {
                Id = 2,
                Title = "Support",
                Key = "support",
                IsVisible = true,
            }, new LinkSection()
            {
                Id = 3,
                Title = "Company",
                Key = "company",
                IsVisible = true,
            });
        }
    }
}

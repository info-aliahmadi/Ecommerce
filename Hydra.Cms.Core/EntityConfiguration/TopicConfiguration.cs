using Hydra.Cms.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hydra.Cms.Core.EntityConfiguration
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable("Topic", "Cms");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Title).HasMaxLength(100);

            // seed some topics with hierarchy
            builder.HasData(
                new Topic()
                {
                    Id = 1,
                    Title = "General",
                    ParentId = null,
                    RegisterDate = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    UserId = 1
                },
                new Topic()
                {
                    Id = 2,
                    Title = "Announcements",
                    ParentId = null,
                    RegisterDate = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    UserId = 1
                },
                new Topic()
                {
                    Id = 3,
                    Title = "Guides",
                    ParentId = null,
                    RegisterDate = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    UserId = 1
                },
                new Topic()
                {
                    Id = 4,
                    Title = "How To",
                    ParentId = null,
                    RegisterDate = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    UserId = 1
                },
                new Topic()
                {
                    Id = 5,
                    Title = "News",
                    ParentId = null,
                    RegisterDate = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    UserId = 1
                },
                new Topic()
                {
                    Id = 6,
                    Title = "Product",
                    ParentId = null,
                    RegisterDate = DateTime.SpecifyKind(DateTime.Parse("2026-4-23"), DateTimeKind.Utc),
                    UserId = 1
                }
            );

        }
    }
}

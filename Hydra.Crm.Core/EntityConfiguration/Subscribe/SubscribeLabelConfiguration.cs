using Hydra.Crm.Core.Domain.Subscribe;
using Hydra.Kernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Crm.Core.EntityConfiguration.Subscribe
{
    public class SubscribeLabelConfiguration : IEntityTypeConfiguration<SubscribeLabel>
    {
        public void Configure(EntityTypeBuilder<SubscribeLabel> entity)
        {
            entity.ToTable(nameof(SubscribeLabel), "Crm");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(100);


            entity.HasData(new SubscribeLabel()
            {
                Id = DefaultSetting.DEFAULT_SUBSCRIBE_LABEL,
                InsertDate = DateTime.SpecifyKind(DateTime.Parse("2026-04-23"), DateTimeKind.Utc),
                Title = "General"
            });
        }
    }
}

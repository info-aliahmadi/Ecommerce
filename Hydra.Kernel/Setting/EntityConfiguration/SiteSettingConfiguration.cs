using Hydra.Auth.Constants;
using Hydra.Kernel.Constants;
using Hydra.Kernel.Setting.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hydra.Kernel.Setting.EntityConfiguration;

public class SiteSettingConfiguration : IEntityTypeConfiguration<SiteSetting>
{
    public void Configure(EntityTypeBuilder<SiteSetting> builder)
    {
        builder.ToTable(InfraTableNames.Setting, DatabaseSchemas.Infra);
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Key).HasMaxLength(150);

    }
}

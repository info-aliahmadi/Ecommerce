using Hydra.Auth.Constants;
using Hydra.Infrastructure.Localization.Domain;
using Hydra.Kernel.Constants;
using Hydra.Kernel.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security;

namespace Hydra.Kernel.Localization.EntityConfiguration
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable(InfraTableNames.Language, DatabaseSchemas.Infra);
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name).HasMaxLength(70);
            builder.HasData(
                new Language()
                {
                    Id = (int)LanguageId.Arabic,
                    Name = "Arabic",
                    CultureInfo = "ar",
                    IsVisible = true
                },
                new Language()
                {
                    Id = (int)LanguageId.Persian,
                    Name = "Persian",
                    CultureInfo = "fa-IR",
                    IsVisible = true
                },
                new Language()
                {
                    Id = (int)LanguageId.English,
                    Name = "English",
                    CultureInfo = "en-US",
                    IsVisible = true
                });

        }
    }
}

using Hydra.Infrastructure.Security;
using Hydra.Kernel;
using Hydra.Kernel.Localization.EntityConfiguration;
using Hydra.Kernel.Setting.Domain;
using Hydra.Kernel.Setting.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Hydra.Infrastructure.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDbContext : IdentityContext //, IDataProtectionKeyContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            #region Configuration Builder


            modelBuilder.ApplyConfiguration(new SiteSettingConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());

            var assembliesList = HydraHelper.GetAssemblies(x => x.StartsWith("Hydra") && x.Contains("Core"));

            foreach (var assembly in assembliesList)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            }

            #endregion
        }
        public DbSet<SiteSetting> Setting { get; set; }


    }
}

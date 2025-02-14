using Hydra.FileStorage.Core.Constants;
using Hydra.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hydra.FileStorage.Core.Seed
{
    public class FileStoragePermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public static int INCREMENTER = 4000;
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(new Permission()
            {
                Id = INCREMENTER + 1,
                Name = FileStoragePermissionTypes.FS_GALLEY_VIEW,
                NormalizedName = FileStoragePermissionTypes.FS_GALLEY_VIEW,
            }, new Permission()
            {
                Id = INCREMENTER + 2,
                Name = FileStoragePermissionTypes.FS_FILE_UPLOAD,
                NormalizedName = FileStoragePermissionTypes.FS_FILE_UPLOAD,
            });
        }
    }
}

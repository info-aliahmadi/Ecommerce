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
                Name = FileStoragePermissionTypes.FS_GET_FILE_INFO,
                NormalizedName = FileStoragePermissionTypes.FS_GET_FILE_INFO,
            }, new Permission()
            {
                Id = INCREMENTER + 2,
                Name = FileStoragePermissionTypes.FS_GET_FILE_INFO_BY_NAME,
                NormalizedName = FileStoragePermissionTypes.FS_GET_FILE_INFO_BY_NAME,
            }, new Permission()
            {
                Id = INCREMENTER + 3,
                Name = FileStoragePermissionTypes.FS_GET_FILES_LIST,
                NormalizedName = FileStoragePermissionTypes.FS_GET_FILES_LIST,
            }, new Permission()
            {
                Id = INCREMENTER + 4,
                Name = FileStoragePermissionTypes.FS_GET_GALLEY_FILES,
                NormalizedName = FileStoragePermissionTypes.FS_GET_GALLEY_FILES,
            }, new Permission()
            {
                Id = INCREMENTER + 5,
                Name = FileStoragePermissionTypes.FS_GET_DIRECTORIES,
                NormalizedName = FileStoragePermissionTypes.FS_GET_DIRECTORIES,
            }, new Permission()
            {
                Id = INCREMENTER + 6,
                Name = FileStoragePermissionTypes.FS_GET_FILES_BY_DIRECTORY,
                NormalizedName = FileStoragePermissionTypes.FS_GET_FILES_BY_DIRECTORY,
            });
        }
    }
}

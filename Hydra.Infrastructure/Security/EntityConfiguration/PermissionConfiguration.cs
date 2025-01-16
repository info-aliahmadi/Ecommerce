using Hydra.Infrastructure.Security.Constants;
using Hydra.Infrastructure.Security.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hydra.Infrastructure.Security.EntityConfiguration
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public static int INCREMENTER = 1000;
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission", "Auth");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name).HasMaxLength(150);
            builder.Property(o => o.NormalizedName).HasMaxLength(150);
            builder.HasMany(e => e.Roles).WithMany(x => x.Permissions);


            builder.HasData(new Permission()
            {
                Id = INCREMENTER + 1,
                Name = AuthPermissionTypes.AUTH_CHANGE_PASSWORD,
                NormalizedName = AuthPermissionTypes.AUTH_CHANGE_PASSWORD,
            }, new Permission()
            {
                Id = INCREMENTER + 2,
                Name = AuthPermissionTypes.AUTH_GET_USER_LIST,
                NormalizedName = AuthPermissionTypes.AUTH_GET_USER_LIST,
            }, new Permission()
            {
                Id = INCREMENTER + 3,
                Name = AuthPermissionTypes.AUTH_GET_USER_BY_ID,
                NormalizedName = AuthPermissionTypes.AUTH_GET_USER_BY_ID,
            }, new Permission()
            {
                Id = INCREMENTER + 4,
                Name = AuthPermissionTypes.AUTH_ADD_USER,
                NormalizedName = AuthPermissionTypes.AUTH_ADD_USER,
            }, new Permission()
            {
                Id = INCREMENTER + 5,
                Name = AuthPermissionTypes.AUTH_UPDATE_USER,
                NormalizedName = AuthPermissionTypes.AUTH_UPDATE_USER,
            }, new Permission()
            {
                Id = INCREMENTER + 6,
                Name = AuthPermissionTypes.AUTH_DELETE_USER,
                NormalizedName = AuthPermissionTypes.AUTH_DELETE_USER,
            }, new Permission()
            {
                Id = INCREMENTER + 7,
                Name = AuthPermissionTypes.AUTH_ASSIGN_PERMISSION_TO_ROLE_BY_ROLE_ID,
                NormalizedName = AuthPermissionTypes.AUTH_ASSIGN_PERMISSION_TO_ROLE_BY_ROLE_ID,
            }, new Permission()
            {
                Id = INCREMENTER + 8,
                Name = AuthPermissionTypes.AUTH_GET_ROLE_LIST,
                NormalizedName = AuthPermissionTypes.AUTH_GET_ROLE_LIST,
            }, new Permission()
            {
                Id = INCREMENTER + 9,
                Name = AuthPermissionTypes.AUTH_GET_ROLE_BY_ID,
                NormalizedName = AuthPermissionTypes.AUTH_GET_ROLE_BY_ID,
            }, new Permission()
            {
                Id = INCREMENTER + 10,
                Name = AuthPermissionTypes.AUTH_ADD_ROLE,
                NormalizedName = AuthPermissionTypes.AUTH_ADD_ROLE,
            }, new Permission()
            {
                Id = INCREMENTER + 11,
                Name = AuthPermissionTypes.AUTH_UPDATE_ROLE,
                NormalizedName = AuthPermissionTypes.AUTH_UPDATE_ROLE,
            }, new Permission()
            {
                Id = INCREMENTER + 12,
                Name = AuthPermissionTypes.AUTH_DELETE_ROLE,
                NormalizedName = AuthPermissionTypes.AUTH_DELETE_ROLE,
            }, new Permission()
            {
                Id = INCREMENTER + 13,
                Name = AuthPermissionTypes.AUTH_GET_PERMISSION_LIST,
                NormalizedName = AuthPermissionTypes.AUTH_GET_PERMISSION_LIST,
            }, new Permission()
            {
                Id = INCREMENTER + 14,
                Name = AuthPermissionTypes.AUTH_GET_PERMISSION_BY_ID,
                NormalizedName = AuthPermissionTypes.AUTH_GET_PERMISSION_BY_ID,
            }, new Permission()
            {
                Id = INCREMENTER + 15,
                Name = AuthPermissionTypes.AUTH_ADD_PERMISSION,
                NormalizedName = AuthPermissionTypes.AUTH_ADD_PERMISSION,
            }, new Permission()
            {
                Id = INCREMENTER + 16,
                Name = AuthPermissionTypes.AUTH_UPDATE_PERMISSION,
                NormalizedName = AuthPermissionTypes.AUTH_UPDATE_PERMISSION,
            }, new Permission()
            {
                Id = INCREMENTER + 17,
                Name = AuthPermissionTypes.AUTH_DELETE_PERMISSION,
                NormalizedName = AuthPermissionTypes.AUTH_DELETE_PERMISSION,
            });

        }
    }
}

﻿// <auto-generated />
using System;
using Hydra.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hydra.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250110131541_dbVersion_01")]
    partial class dbVersion_01
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hydra.Infrastructure.Security.Domain.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Permission", "Auth");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "AUTH.CHANGE_PASSWORD",
                            NormalizedName = "AUTH.CHANGE_PASSWORD"
                        },
                        new
                        {
                            Id = 2,
                            Name = "AUTH.GET_USER_LIST",
                            NormalizedName = "AUTH.GET_USER_LIST"
                        },
                        new
                        {
                            Id = 3,
                            Name = "AUTH.GET_USER_BY_ID",
                            NormalizedName = "AUTH.GET_USER_BY_ID"
                        },
                        new
                        {
                            Id = 4,
                            Name = "AUTH.ADD_USER",
                            NormalizedName = "AUTH.ADD_USER"
                        },
                        new
                        {
                            Id = 5,
                            Name = "AUTH.UPDATE_USER",
                            NormalizedName = "AUTH.UPDATE_USER"
                        },
                        new
                        {
                            Id = 6,
                            Name = "AUTH.DELETE_USER",
                            NormalizedName = "AUTH.DELETE_USER"
                        },
                        new
                        {
                            Id = 7,
                            Name = "AUTH.ASSIGN_PERMISSION_TO_ROLE_BY_ROLE_ID",
                            NormalizedName = "AUTH.ASSIGN_PERMISSION_TO_ROLE_BY_ROLE_ID"
                        },
                        new
                        {
                            Id = 8,
                            Name = "AUTH.GET_ROLE_LIST",
                            NormalizedName = "AUTH.GET_ROLE_LIST"
                        },
                        new
                        {
                            Id = 9,
                            Name = "AUTH.GET_ROLE_BY_ID",
                            NormalizedName = "AUTH.GET_ROLE_BY_ID"
                        },
                        new
                        {
                            Id = 10,
                            Name = "AUTH.ADD_ROLE",
                            NormalizedName = "AUTH.ADD_ROLE"
                        },
                        new
                        {
                            Id = 11,
                            Name = "AUTH.UPDATE_ROLE",
                            NormalizedName = "AUTH.UPDATE_ROLE"
                        },
                        new
                        {
                            Id = 12,
                            Name = "AUTH.DELETE_ROLE",
                            NormalizedName = "AUTH.DELETE_ROLE"
                        },
                        new
                        {
                            Id = 13,
                            Name = "AUTH.GET_PERMISSION_LIST",
                            NormalizedName = "AUTH.GET_PERMISSION_LIST"
                        },
                        new
                        {
                            Id = 14,
                            Name = "AUTH.GET_PERMISSION_BY_ID",
                            NormalizedName = "AUTH.GET_PERMISSION_BY_ID"
                        },
                        new
                        {
                            Id = 15,
                            Name = "AUTH.ADD_PERMISSION",
                            NormalizedName = "AUTH.ADD_PERMISSION"
                        },
                        new
                        {
                            Id = 16,
                            Name = "AUTH.UPDATE_PERMISSION",
                            NormalizedName = "AUTH.UPDATE_PERMISSION"
                        },
                        new
                        {
                            Id = 17,
                            Name = "AUTH.DELETE_PERMISSION",
                            NormalizedName = "AUTH.DELETE_PERMISSION"
                        });
                });

            modelBuilder.Entity("Hydra.Infrastructure.Security.Domain.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role", "Auth");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "SuperAdmin",
                            NormalizedName = "SUPERADMIN"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 3,
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Superviser",
                            NormalizedName = "SUPERVISER"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Guest",
                            NormalizedName = "GUEST"
                        });
                });

            modelBuilder.Entity("Hydra.Infrastructure.Security.Domain.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim", "Auth");
                });

            modelBuilder.Entity("Hydra.Infrastructure.Security.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DefaultLanguage")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("DefaultTheme")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("User", "Auth");
                });

            modelBuilder.Entity("Hydra.Infrastructure.Security.Domain.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim", "Auth");
                });

            modelBuilder.Entity("Hydra.Infrastructure.Security.Domain.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin", "Auth");
                });

            modelBuilder.Entity("Hydra.Infrastructure.Security.Domain.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole", "Auth");
                });

            modelBuilder.Entity("Hydra.Infrastructure.Security.Domain.UserToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken", "Auth");
                });

            modelBuilder.Entity("Hydra.Infrastructure.Setting.Domain.SiteSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ValueType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Setting", "Infra");
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.Property<int>("PermissionsId")
                        .HasColumnType("int");

                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.HasKey("PermissionsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("PermissionRole", "Auth");
                });

            modelBuilder.Entity("Hydra.Infrastructure.Security.Domain.RoleClaim", b =>
                {
                    b.HasOne("Hydra.Infrastructure.Security.Domain.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hydra.Infrastructure.Security.Domain.UserClaim", b =>
                {
                    b.HasOne("Hydra.Infrastructure.Security.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hydra.Infrastructure.Security.Domain.UserLogin", b =>
                {
                    b.HasOne("Hydra.Infrastructure.Security.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hydra.Infrastructure.Security.Domain.UserRole", b =>
                {
                    b.HasOne("Hydra.Infrastructure.Security.Domain.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hydra.Infrastructure.Security.Domain.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Hydra.Infrastructure.Security.Domain.UserToken", b =>
                {
                    b.HasOne("Hydra.Infrastructure.Security.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.HasOne("Hydra.Infrastructure.Security.Domain.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hydra.Infrastructure.Security.Domain.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hydra.Infrastructure.Security.Domain.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Hydra.Infrastructure.Security.Domain.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}

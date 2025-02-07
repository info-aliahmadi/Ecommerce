using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cms");

            migrationBuilder.EnsureSchema(
                name: "Crm");

            migrationBuilder.EnsureSchema(
                name: "FS");

            migrationBuilder.EnsureSchema(
                name: "Auth");

            migrationBuilder.EnsureSchema(
                name: "Infra");

            migrationBuilder.CreateTable(
                name: "EmailInbox",
                schema: "Crm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    IsPin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailInbox", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkSection",
                schema: "Cms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkSection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "Cms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PreviewImageId = table.Column<int>(type: "int", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_Menu_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Cms",
                        principalTable: "Menu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                schema: "Infra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscribeLabel",
                schema: "Cms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscribeLabel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                schema: "Cms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                schema: "Cms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topic_Topic_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Cms",
                        principalTable: "Topic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DefaultLanguage = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    DefaultTheme = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailInboxAttachment",
                schema: "Crm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailInboxId = table.Column<int>(type: "int", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailInboxAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailInboxAttachment_EmailInbox_EmailInboxId",
                        column: x => x.EmailInboxId,
                        principalSchema: "Crm",
                        principalTable: "EmailInbox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailInboxFromAddress",
                schema: "Crm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailInboxId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailInboxFromAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailInboxFromAddress_EmailInbox_EmailInboxId",
                        column: x => x.EmailInboxId,
                        principalSchema: "Crm",
                        principalTable: "EmailInbox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailInboxToAddress",
                schema: "Crm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailInboxId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailInboxToAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailInboxToAddress_EmailInbox_EmailInboxId",
                        column: x => x.EmailInboxId,
                        principalSchema: "Crm",
                        principalTable: "EmailInbox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailOutbox",
                schema: "Crm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReplayToId = table.Column<int>(type: "int", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDraft = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailOutbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailOutbox_EmailInbox_ReplayToId",
                        column: x => x.ReplayToId,
                        principalSchema: "Crm",
                        principalTable: "EmailInbox",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PermissionRole",
                schema: "Auth",
                columns: table => new
                {
                    PermissionsId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRole", x => new { x.PermissionsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_PermissionRole_Permission_PermissionsId",
                        column: x => x.PermissionsId,
                        principalSchema: "Auth",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionRole_Role_RolesId",
                        column: x => x.RolesId,
                        principalSchema: "Auth",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Auth",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscribe",
                schema: "Cms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscribeLabelId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscribe_SubscribeLabel",
                        column: x => x.SubscribeLabelId,
                        principalSchema: "Cms",
                        principalTable: "SubscribeLabel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileUpload",
                schema: "FS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Directory = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Alt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUpload", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileUpload_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Link",
                schema: "Cms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ImagePreviewId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LinkSectionId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Link", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Link_LinkSection_LinkSectionId",
                        column: x => x.LinkSectionId,
                        principalSchema: "Cms",
                        principalTable: "LinkSection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Link_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                schema: "Crm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FromUserId = table.Column<int>(type: "int", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsDraft = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_User_FromUserId",
                        column: x => x.FromUserId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Page",
                schema: "Cms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WriterId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Page_User_EditorId",
                        column: x => x.EditorId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Page_User_WriterId",
                        column: x => x.WriterId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Slideshow",
                schema: "Cms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PreviewImageId = table.Column<int>(type: "int", nullable: true),
                    PreviewImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slideshow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slideshow_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                schema: "Auth",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "Auth",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Auth",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                schema: "Auth",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailOutboxAttachment",
                schema: "Crm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailOutboxId = table.Column<int>(type: "int", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailOutboxAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailOutboxAttachment_EmailOutbox_EmailOutboxId",
                        column: x => x.EmailOutboxId,
                        principalSchema: "Crm",
                        principalTable: "EmailOutbox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailOutboxFromAddress",
                schema: "Crm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailOutboxId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailOutboxFromAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailOutboxFromAddress_EmailOutbox_EmailOutboxId",
                        column: x => x.EmailOutboxId,
                        principalSchema: "Crm",
                        principalTable: "EmailOutbox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailOutboxToAddress",
                schema: "Crm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailOutboxId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailOutboxToAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailOutboxToAddress_EmailOutbox_EmailOutboxId",
                        column: x => x.EmailOutboxId,
                        principalSchema: "Crm",
                        principalTable: "EmailOutbox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                schema: "Cms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviewImageId = table.Column<int>(type: "int", nullable: true),
                    PreviewImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WriterId = table.Column<int>(type: "int", nullable: false),
                    EditorId = table.Column<int>(type: "int", nullable: true),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPinned = table.Column<bool>(type: "bit", nullable: false),
                    IsDraft = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_FileUpload_PreviewImageId",
                        column: x => x.PreviewImageId,
                        principalSchema: "FS",
                        principalTable: "FileUpload",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Article_User_EditorId",
                        column: x => x.EditorId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Article_User_WriterId",
                        column: x => x.WriterId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageAttachment",
                schema: "Crm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageAttachment_Message_MessageId",
                        column: x => x.MessageId,
                        principalSchema: "Crm",
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageUser",
                schema: "Crm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    ToUserId = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageUser_Message_MessageId",
                        column: x => x.MessageId,
                        principalSchema: "Crm",
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageUser_User_ToUserId",
                        column: x => x.ToUserId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageTag",
                schema: "Cms",
                columns: table => new
                {
                    PageId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageTag", x => new { x.TagId, x.PageId });
                    table.ForeignKey(
                        name: "FK_PageTag_Page_PageId",
                        column: x => x.PageId,
                        principalSchema: "Cms",
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageTag_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "Cms",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTag",
                schema: "Cms",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTag", x => new { x.TagId, x.ArticleId });
                    table.ForeignKey(
                        name: "FK_ArticleTag_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalSchema: "Cms",
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleTag_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "Cms",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTopic",
                schema: "Cms",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTopic", x => new { x.TopicId, x.ArticleId });
                    table.ForeignKey(
                        name: "FK_ArticleTopic_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalSchema: "Cms",
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleTopic_Topic_TopicId",
                        column: x => x.TopicId,
                        principalSchema: "Cms",
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Cms",
                table: "LinkSection",
                columns: new[] { "Id", "IsVisible", "Key", "Title" },
                values: new object[,]
                {
                    { 1, true, "Categories", "Categories" },
                    { 2, true, "RecentPosts", "Recent Post" }
                });

            migrationBuilder.InsertData(
                schema: "Cms",
                table: "Menu",
                columns: new[] { "Id", "Order", "ParentId", "PreviewImageId", "Title", "Url", "UserId" },
                values: new object[,]
                {
                    { 1, 0, null, null, "About", "/About", 1 },
                    { 2, 1, null, null, "Service", "/Service", 1 },
                    { 3, 2, null, null, "Pricing", "/Pricing", 1 },
                    { 4, 3, null, null, "Contact", "/Contact", 1 },
                    { 5, 4, null, null, "Blog", "/Blog", 1 }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "AUTH.USER_MANAGEMENT", "AUTH.USER_MANAGEMENT" },
                    { 2, "AUTH.PERMISSION_MANAGEMENT", "AUTH.PERMISSION_MANAGEMENT" },
                    { 2001, "CMS.ADD_OR_UPDATE_SETTINGS", "CMS.ADD_OR_UPDATE_SETTINGS" },
                    { 2002, "CMS.GET_TOPIC_LIST", "CMS.GET_TOPIC_LIST" },
                    { 2003, "CMS.GET_TOPIC_BY_ID", "CMS.GET_TOPIC_BY_ID" },
                    { 2004, "CMS.ADD_TOPIC", "CMS.ADD_TOPIC" },
                    { 2005, "CMS.UPDATE_TOPIC", "CMS.UPDATE_TOPIC" },
                    { 2006, "CMS.DELETE_TOPIC", "CMS.DELETE_TOPIC" },
                    { 2007, "CMS.GET_TAG_LIST", "CMS.GET_TAG_LIST" },
                    { 2008, "CMS.GET_TAG_BY_ID", "CMS.GET_TAG_BY_ID" },
                    { 2009, "CMS.ADD_TAG", "CMS.ADD_TAG" },
                    { 2010, "CMS.UPDATE_TAG", "CMS.UPDATE_TAG" },
                    { 2011, "CMS.DELETE_TAG", "CMS.DELETE_TAG" },
                    { 2012, "CMS.GET_ARTICLE_LIST", "CMS.GET_ARTICLE_LIST" },
                    { 2013, "CMS.GET_TRASH_ARTICLE_LIST", "CMS.GET_TRASH_ARTICLE_LIST" },
                    { 2014, "CMS.GET_ARTICLE_BY_ID", "CMS.GET_ARTICLE_BY_ID" },
                    { 2015, "CMS.CMS_PINNED_ARTICLE", "CMS.CMS_PINNED_ARTICLE" },
                    { 2016, "CMS.ADD_ARTICLE", "CMS.ADD_ARTICLE" },
                    { 2017, "CMS.UPDATE_ARTICLE", "CMS.UPDATE_ARTICLE" },
                    { 2018, "CMS.DELETE_ARTICLE", "CMS.DELETE_ARTICLE" },
                    { 2019, "CMS.RESTORE_ARTICLE", "CMS.RESTORE_ARTICLE" },
                    { 2020, "CMS.REMOVE_ARTICLE", "CMS.REMOVE_ARTICLE" },
                    { 2021, "CMS.GET_PAGE_LIST", "CMS.GET_PAGE_LIST" },
                    { 2022, "CMS.GET_PAGE_BY_ID", "CMS.GET_PAGE_BY_ID" },
                    { 2023, "CMS.ADD_PAGE", "CMS.ADD_PAGE" },
                    { 2024, "CMS.UPDATE_PAGE", "CMS.UPDATE_PAGE" },
                    { 2025, "CMS.DELETE_PAGE", "CMS.DELETE_PAGE" },
                    { 2026, "CMS.GET_MENU_LIST", "CMS.GET_MENU_LIST" },
                    { 2027, "CMS.GET_MENU_BY_ID", "CMS.GET_MENU_BY_ID" },
                    { 2028, "CMS.ADD_MENU", "CMS.ADD_MENU" },
                    { 2029, "CMS.UPDATE_MENU", "CMS.UPDATE_MENU" },
                    { 2030, "CMS.DELETE_MENU", "CMS.DELETE_MENU" },
                    { 2031, "CMS.GET_SLIDESHOW_LIST", "CMS.GET_SLIDESHOW_LIST" },
                    { 2032, "CMS.GET_SLIDESHOW_BY_ID", "CMS.GET_SLIDESHOW_BY_ID" },
                    { 2033, "CMS.ADD_SLIDESHOW", "CMS.ADD_SLIDESHOW" },
                    { 2034, "CMS.UPDATE_SLIDESHOW", "CMS.UPDATE_SLIDESHOW" },
                    { 2035, "CMS.VISIBLE_SLIDESHOW", "CMS.VISIBLE_SLIDESHOW" },
                    { 2036, "CMS.DELETE_SLIDESHOW", "CMS.DELETE_SLIDESHOW" },
                    { 3001, "CRM.GET_SETTINGS", "CRM.GET_SETTINGS" },
                    { 3002, "CRM.ADD_OR_UPDATE_SETTINGS", "CRM.ADD_OR_UPDATE_SETTINGS" },
                    { 3003, "CRM.SEND_PUBLIC_MESSAGE", "CRM.SEND_PUBLIC_MESSAGE" },
                    { 3004, "CRM.SEND_PRIVATE_MESSAGE", "CRM.SEND_PRIVATE_MESSAGE" },
                    { 3005, "CRM.SAVE_DRAFT_MESSAGE", "CRM.SAVE_DRAFT_MESSAGE" },
                    { 3006, "CRM.GET_ALLMESSAGES", "CRM.GET_ALLMESSAGES" },
                    { 3007, "CRM.GET_INBOX_MESSAGES", "CRM.GET_INBOX_MESSAGES" },
                    { 3008, "CRM.GET_SENT_MESSAGES", "CRM.GET_SENT_MESSAGES" },
                    { 3009, "CRM.GET_DRAFT_MESSAGES", "CRM.GET_DRAFT_MESSAGES" },
                    { 3010, "CRM.GET_PUBLIC_INBOX_MESSAGES", "CRM.GET_PUBLIC_INBOX_MESSAGES" },
                    { 3011, "CRM.GET_DELETED_INBOX_MESSAGES", "CRM.GET_DELETED_INBOX_MESSAGES" },
                    { 3012, "CRM.GET_DELETED_SENT_MESSAGES", "CRM.GET_DELETED_SENT_MESSAGES" },
                    { 3013, "CRM.GET_MESSAGE_BY_ID_FOR_PUBLIC", "CRM.GET_MESSAGE_BY_ID_FOR_PUBLIC" },
                    { 3014, "CRM.GET_MESSAGE_BY_ID_FOR_SENDER", "CRM.GET_MESSAGE_BY_ID_FOR_SENDER" },
                    { 3015, "CRM.GET_MESSAGE_BY_ID_FOR_RECEIVER", "CRM.GET_MESSAGE_BY_ID_FOR_RECEIVER" },
                    { 3016, "CRM.DELETE_MESSAGE", "CRM.DELETE_MESSAGE" },
                    { 3017, "CRM.RESTORE_MESSAGE", "CRM.RESTORE_MESSAGE" },
                    { 3018, "CRM.PIN_MESSAGE", "CRM.PIN_MESSAGE" },
                    { 3019, "CRM.READ_MESSAGE", "CRM.READ_MESSAGE" },
                    { 3020, "CRM.DELETE_DRAFT_MESSAGE", "CRM.DELETE_DRAFT_MESSAGE" },
                    { 3021, "CRM.REMOVE_DRAFT_MESSAGE", "CRM.REMOVE_DRAFT_MESSAGE" },
                    { 3022, "CRM.LOAD_EMAIL_INBOX", "CRM.LOAD_EMAIL_INBOX" },
                    { 3023, "CRM.GET_ALL_EMAIL_INBOX", "CRM.GET_ALL_EMAIL_INBOX" },
                    { 3024, "CRM.GET_INBOX_EMAIL_INBOX", "CRM.GET_INBOX_EMAIL_INBOX" },
                    { 3025, "CRM.GET_DELETED_EMAIL_INBOX", "CRM.GET_DELETED_EMAIL_INBOX" },
                    { 3026, "CRM.GET_EMAIL_INBOX_BY_ID", "CRM.GET_EMAIL_INBOX_BY_ID" },
                    { 3027, "CRM.GET_EMAIL_INBOX_BY_ID_FOR_RECEIVER", "CRM.GET_EMAIL_INBOX_BY_ID_FOR_RECEIVER" },
                    { 3028, "CRM.DELETE_EMAIL_INBOX", "CRM.DELETE_EMAIL_INBOX" },
                    { 3029, "CRM.PIN_EMAIL_INBOX", "CRM.PIN_EMAIL_INBOX" },
                    { 3030, "CRM.READ_EMAIL_INBOX", "CRM.READ_EMAIL_INBOX" },
                    { 3031, "CRM.REMOVE_EMAIL_INBOX", "CRM.REMOVE_EMAIL_INBOX" },
                    { 3032, "CRM.SEND_EMAIL_OUTBOX", "CRM.SEND_EMAIL_OUTBOX" },
                    { 3033, "CRM.SAVE_DRAFT_EMAIL_OUTBOX", "CRM.SAVE_DRAFT_EMAIL_OUTBOX" },
                    { 3034, "CRM.GET_ALL_EMAIL_OUTBOX", "CRM.GET_ALL_EMAIL_OUTBOX" },
                    { 3035, "CRM.GET_EMAIL_OUTBOX", "CRM.GET_EMAIL_OUTBOX" },
                    { 3036, "CRM.GET_ADDRESS_FOR_SELECT", "CRM.GET_ADDRESS_FOR_SELECT" },
                    { 3037, "CRM.GET_EMAIL_OUTBOX_BY_ID_FOR_SENDER", "CRM.GET_EMAIL_OUTBOX_BY_ID_FOR_SENDER" },
                    { 3038, "CRM.REMOVE_EMAIL_OUTBOX", "CRM.REMOVE_EMAIL_OUTBOX" },
                    { 4001, "FS.GET_FILE_INFO", "FS.GET_FILE_INFO" },
                    { 4002, "FS.GET_FILE_INFO_BY_NAME", "FS.GET_FILE_INFO_BY_NAME" },
                    { 4003, "FS.GET_FILES_LIST", "FS.GET_FILES_LIST" },
                    { 4004, "FS.GET_GALLEY_FILES", "FS.GET_GALLEY_FILES" },
                    { 4005, "FS.GET_DIRECTORIES", "FS.GET_DIRECTORIES" },
                    { 4006, "FS.GET_FILES_BY_DIRECTORY", "FS.GET_FILES_BY_DIRECTORY" }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "SUPERADMIN", "SUPERADMIN" },
                    { 2, null, "ADMIN", "ADMIN" },
                    { 3, null, "USER", "USER" },
                    { 4, null, "SUPERVISER", "SUPERVISER" },
                    { 5, null, "GUEST", "GUEST" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_EditorId",
                schema: "Cms",
                table: "Article",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_PreviewImageId",
                schema: "Cms",
                table: "Article",
                column: "PreviewImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_WriterId",
                schema: "Cms",
                table: "Article",
                column: "WriterId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTag_ArticleId",
                schema: "Cms",
                table: "ArticleTag",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTopic_ArticleId",
                schema: "Cms",
                table: "ArticleTopic",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailInboxAttachment_EmailInboxId",
                schema: "Crm",
                table: "EmailInboxAttachment",
                column: "EmailInboxId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailInboxFromAddress_EmailInboxId",
                schema: "Crm",
                table: "EmailInboxFromAddress",
                column: "EmailInboxId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailInboxToAddress_EmailInboxId",
                schema: "Crm",
                table: "EmailInboxToAddress",
                column: "EmailInboxId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailOutbox_ReplayToId",
                schema: "Crm",
                table: "EmailOutbox",
                column: "ReplayToId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailOutboxAttachment_EmailOutboxId",
                schema: "Crm",
                table: "EmailOutboxAttachment",
                column: "EmailOutboxId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailOutboxFromAddress_EmailOutboxId",
                schema: "Crm",
                table: "EmailOutboxFromAddress",
                column: "EmailOutboxId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailOutboxToAddress_EmailOutboxId",
                schema: "Crm",
                table: "EmailOutboxToAddress",
                column: "EmailOutboxId");

            migrationBuilder.CreateIndex(
                name: "IX_FileUpload_UserId",
                schema: "FS",
                table: "FileUpload",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Link_LinkSectionId",
                schema: "Cms",
                table: "Link",
                column: "LinkSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Link_UserId",
                schema: "Cms",
                table: "Link",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentId",
                schema: "Cms",
                table: "Menu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_FromUserId",
                schema: "Crm",
                table: "Message",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageAttachment_MessageId",
                schema: "Crm",
                table: "MessageAttachment",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageUser_MessageId",
                schema: "Crm",
                table: "MessageUser",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageUser_ToUserId",
                schema: "Crm",
                table: "MessageUser",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Page_EditorId",
                schema: "Cms",
                table: "Page",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Page_WriterId",
                schema: "Cms",
                table: "Page",
                column: "WriterId");

            migrationBuilder.CreateIndex(
                name: "IX_PageTag_PageId",
                schema: "Cms",
                table: "PageTag",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRole_RolesId",
                schema: "Auth",
                table: "PermissionRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Auth",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                schema: "Auth",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Slideshow_UserId",
                schema: "Cms",
                table: "Slideshow",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribe_SubscribeLabelId",
                schema: "Cms",
                table: "Subscribe",
                column: "SubscribeLabelId");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_ParentId",
                schema: "Cms",
                table: "Topic",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Auth",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Auth",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                schema: "Auth",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                schema: "Auth",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "Auth",
                table: "UserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleTag",
                schema: "Cms");

            migrationBuilder.DropTable(
                name: "ArticleTopic",
                schema: "Cms");

            migrationBuilder.DropTable(
                name: "EmailInboxAttachment",
                schema: "Crm");

            migrationBuilder.DropTable(
                name: "EmailInboxFromAddress",
                schema: "Crm");

            migrationBuilder.DropTable(
                name: "EmailInboxToAddress",
                schema: "Crm");

            migrationBuilder.DropTable(
                name: "EmailOutboxAttachment",
                schema: "Crm");

            migrationBuilder.DropTable(
                name: "EmailOutboxFromAddress",
                schema: "Crm");

            migrationBuilder.DropTable(
                name: "EmailOutboxToAddress",
                schema: "Crm");

            migrationBuilder.DropTable(
                name: "Link",
                schema: "Cms");

            migrationBuilder.DropTable(
                name: "Menu",
                schema: "Cms");

            migrationBuilder.DropTable(
                name: "MessageAttachment",
                schema: "Crm");

            migrationBuilder.DropTable(
                name: "MessageUser",
                schema: "Crm");

            migrationBuilder.DropTable(
                name: "PageTag",
                schema: "Cms");

            migrationBuilder.DropTable(
                name: "PermissionRole",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "RoleClaim",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "Setting",
                schema: "Infra");

            migrationBuilder.DropTable(
                name: "Slideshow",
                schema: "Cms");

            migrationBuilder.DropTable(
                name: "Subscribe",
                schema: "Cms");

            migrationBuilder.DropTable(
                name: "UserClaim",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "UserLogin",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "UserToken",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "Article",
                schema: "Cms");

            migrationBuilder.DropTable(
                name: "Topic",
                schema: "Cms");

            migrationBuilder.DropTable(
                name: "EmailOutbox",
                schema: "Crm");

            migrationBuilder.DropTable(
                name: "LinkSection",
                schema: "Cms");

            migrationBuilder.DropTable(
                name: "Message",
                schema: "Crm");

            migrationBuilder.DropTable(
                name: "Page",
                schema: "Cms");

            migrationBuilder.DropTable(
                name: "Tag",
                schema: "Cms");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "SubscribeLabel",
                schema: "Cms");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "FileUpload",
                schema: "FS");

            migrationBuilder.DropTable(
                name: "EmailInbox",
                schema: "Crm");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Auth");
        }
    }
}

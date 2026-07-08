using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bundle",
                schema: "Sale",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    show_on_homepage = table.Column<bool>(type: "boolean", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp(6) with time zone", precision: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bundle", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ProductBundle",
                schema: "Sale",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bundle_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Bundle_Mapping", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductBundle_Bundle",
                        column: x => x.bundle_id,
                        principalSchema: "Sale",
                        principalTable: "Bundle",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductBundle_Product",
                        column: x => x.product_id,
                        principalSchema: "Sale",
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Sale",
                table: "Bundle",
                columns: new[] { "id", "created_on_utc", "description", "display_order", "name", "show_on_homepage" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Everything you need for the perfect summer", 1, "Summer Essentials Bundle", true },
                    { 2, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Level up your home office setup", 2, "Tech Workspace Bundle", true },
                    { 3, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Treat yourself to a spa experience at home", 3, "Self-Care Ritual Bundlee", true }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "id", "name", "normalized_name" },
                values: new object[] { 5025, "SALE.BUNDLE_MANAGEMENT", "SALE.BUNDLE_MANAGEMENT" });

            migrationBuilder.InsertData(
                schema: "Sale",
                table: "ProductBundle",
                columns: new[] { "id", "bundle_id", "display_order", "product_id" },
                values: new object[,]
                {
                    { 1, 1, 1, 1001 },
                    { 2, 1, 2, 1002 },
                    { 3, 1, 3, 1003 },
                    { 4, 2, 1, 1004 },
                    { 5, 2, 2, 1005 },
                    { 6, 2, 3, 1006 },
                    { 7, 3, 1, 1007 },
                    { 8, 3, 2, 1008 },
                    { 9, 3, 3, 1009 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_bundle_display_order",
                schema: "Sale",
                table: "Bundle",
                column: "display_order");

            migrationBuilder.CreateIndex(
                name: "ix_product_bundle_bundle_id",
                schema: "Sale",
                table: "ProductBundle",
                column: "bundle_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_bundle_bundle_id_product_id",
                schema: "Sale",
                table: "ProductBundle",
                columns: new[] { "bundle_id", "product_id" });

            migrationBuilder.CreateIndex(
                name: "ix_product_bundle_product_id",
                schema: "Sale",
                table: "ProductBundle",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductBundle",
                schema: "Sale");

            migrationBuilder.DropTable(
                name: "Bundle",
                schema: "Sale");

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "id",
                keyValue: 5025);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountCategory",
                schema: "Sale");

            migrationBuilder.DropTable(
                name: "DiscountManufacturer",
                schema: "Sale");

            migrationBuilder.DropTable(
                name: "DiscountProduct",
                schema: "Sale");

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "id",
                keyValue: 5005);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "id",
                keyValue: 5006);

            migrationBuilder.DropColumn(
                name: "paid_date_utc",
                schema: "Sale",
                table: "Order");

            migrationBuilder.AddColumn<decimal>(
                name: "order_total",
                schema: "Sale",
                table: "Discount",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "discount_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    discount_id = table.Column<int>(type: "integer", nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discount_category", x => x.id);
                    table.ForeignKey(
                        name: "fk_discount_category_category_category_id",
                        column: x => x.category_id,
                        principalSchema: "Sale",
                        principalTable: "Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_discount_category_discount_discount_id",
                        column: x => x.discount_id,
                        principalSchema: "Sale",
                        principalTable: "Discount",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "discount_manufacturer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    discount_id = table.Column<int>(type: "integer", nullable: false),
                    manufacturer_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discount_manufacturer", x => x.id);
                    table.ForeignKey(
                        name: "fk_discount_manufacturer_discount_discount_id",
                        column: x => x.discount_id,
                        principalSchema: "Sale",
                        principalTable: "Discount",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_discount_manufacturer_manufacturer_manufacturer_id",
                        column: x => x.manufacturer_id,
                        principalSchema: "Sale",
                        principalTable: "Manufacturer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "discount_product",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    discount_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discount_product", x => x.id);
                    table.ForeignKey(
                        name: "fk_discount_product_discount_discount_id",
                        column: x => x.discount_id,
                        principalSchema: "Sale",
                        principalTable: "Discount",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_discount_product_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "Sale",
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Discount",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "discount_type_id", "order_total" },
                values: new object[] { 4, null });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Discount",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "discount_type_id", "order_total" },
                values: new object[] { 4, null });

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 4,
                column: "url",
                value: "/products/?sort=price-asc");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 22,
                column: "url",
                value: "/products/?categories=electronics");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 23,
                column: "url",
                value: "/products/?categories=fashion");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 24,
                column: "url",
                value: "/products/?categories=home-living");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 25,
                column: "url",
                value: "/products/?categories=sports");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 26,
                column: "url",
                value: "/products/?categories=beauty");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 27,
                column: "url",
                value: "/products/?categories=books");

            migrationBuilder.CreateIndex(
                name: "ix_discount_category_category_id",
                table: "discount_category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_discount_category_discount_id",
                table: "discount_category",
                column: "discount_id");

            migrationBuilder.CreateIndex(
                name: "ix_discount_manufacturer_discount_id",
                table: "discount_manufacturer",
                column: "discount_id");

            migrationBuilder.CreateIndex(
                name: "ix_discount_manufacturer_manufacturer_id",
                table: "discount_manufacturer",
                column: "manufacturer_id");

            migrationBuilder.CreateIndex(
                name: "ix_discount_product_discount_id",
                table: "discount_product",
                column: "discount_id");

            migrationBuilder.CreateIndex(
                name: "ix_discount_product_product_id",
                table: "discount_product",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "discount_category");

            migrationBuilder.DropTable(
                name: "discount_manufacturer");

            migrationBuilder.DropTable(
                name: "discount_product");

            migrationBuilder.DropColumn(
                name: "order_total",
                schema: "Sale",
                table: "Discount");

            migrationBuilder.AddColumn<DateTime>(
                name: "paid_date_utc",
                schema: "Sale",
                table: "Order",
                type: "timestamp(6) with time zone",
                precision: 6,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DiscountCategory",
                schema: "Sale",
                columns: table => new
                {
                    discount_id = table.Column<int>(type: "integer", nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount_AppliedToCategories", x => new { x.discount_id, x.category_id });
                    table.ForeignKey(
                        name: "FK_DiscountCategory_Category",
                        column: x => x.category_id,
                        principalSchema: "Sale",
                        principalTable: "Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountCategory_Discount",
                        column: x => x.discount_id,
                        principalSchema: "Sale",
                        principalTable: "Discount",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiscountManufacturer",
                schema: "Sale",
                columns: table => new
                {
                    discount_id = table.Column<int>(type: "integer", nullable: false),
                    manufacturer_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount_AppliedToManufacturers", x => new { x.discount_id, x.manufacturer_id });
                    table.ForeignKey(
                        name: "FK_DiscountManufacturer_Discount",
                        column: x => x.discount_id,
                        principalSchema: "Sale",
                        principalTable: "Discount",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountManufacturer_Manufacturer",
                        column: x => x.manufacturer_id,
                        principalSchema: "Sale",
                        principalTable: "Manufacturer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiscountProduct",
                schema: "Sale",
                columns: table => new
                {
                    discount_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount_AppliedToProducts", x => new { x.discount_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_DiscountProduct_Discount",
                        column: x => x.discount_id,
                        principalSchema: "Sale",
                        principalTable: "Discount",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountProduct_Product",
                        column: x => x.product_id,
                        principalSchema: "Sale",
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Discount",
                keyColumn: "id",
                keyValue: 1,
                column: "discount_type_id",
                value: 5);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Discount",
                keyColumn: "id",
                keyValue: 2,
                column: "discount_type_id",
                value: 5);

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 4,
                column: "url",
                value: "/products/?sorting=price-lower");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 22,
                column: "url",
                value: "/products/?category=electronics");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 23,
                column: "url",
                value: "/products/?category=fashion");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 24,
                column: "url",
                value: "/products/?category=home-living");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 25,
                column: "url",
                value: "/products/?category=sports");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 26,
                column: "url",
                value: "/products/?category=beauty");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 27,
                column: "url",
                value: "/products/?category=books");

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "id", "name", "normalized_name" },
                values: new object[,]
                {
                    { 5005, "SALE.ORDER_ITEM_MANAGEMENT", "SALE.ORDER_ITEM_MANAGEMENT" },
                    { 5006, "SALE.ORDERNOTE_MANAGEMENT", "SALE.ORDERNOTE_MANAGEMENT" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_discount_category_category_id",
                schema: "Sale",
                table: "DiscountCategory",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_discount_category_discount_id",
                schema: "Sale",
                table: "DiscountCategory",
                column: "discount_id");

            migrationBuilder.CreateIndex(
                name: "ix_discount_manufacturer_discount_id",
                schema: "Sale",
                table: "DiscountManufacturer",
                column: "discount_id");

            migrationBuilder.CreateIndex(
                name: "ix_discount_manufacturer_manufacturer_id",
                schema: "Sale",
                table: "DiscountManufacturer",
                column: "manufacturer_id");

            migrationBuilder.CreateIndex(
                name: "ix_discount_product_discount_id",
                schema: "Sale",
                table: "DiscountProduct",
                column: "discount_id");

            migrationBuilder.CreateIndex(
                name: "ix_discount_product_product_id",
                schema: "Sale",
                table: "DiscountProduct",
                column: "product_id");
        }
    }
}

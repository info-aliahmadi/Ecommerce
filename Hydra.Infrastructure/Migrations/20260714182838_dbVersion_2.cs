using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_variant_product_product_id1",
                schema: "Sale",
                table: "ProductVariant");

            migrationBuilder.DropForeignKey(
                name: "fk_product_variant_attribute_product_attribute_attribute_id",
                schema: "Sale",
                table: "ProductVariantAttribute");

            migrationBuilder.DropForeignKey(
                name: "fk_product_variant_attribute_product_attribute_product_attribute_",
                schema: "Sale",
                table: "ProductVariantAttribute");

            migrationBuilder.DropIndex(
                name: "ix_product_variant_attribute_product_attribute_id",
                schema: "Sale",
                table: "ProductVariantAttribute");

            migrationBuilder.DropIndex(
                name: "ix_product_variant_product_id1",
                schema: "Sale",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "product_attribute_id",
                schema: "Sale",
                table: "ProductVariantAttribute");

            migrationBuilder.DropColumn(
                name: "product_id1",
                schema: "Sale",
                table: "ProductVariant");

            migrationBuilder.AddForeignKey(
                name: "fk_product_variant_attribute_product_attribute_attribute_id",
                schema: "Sale",
                table: "ProductVariantAttribute",
                column: "attribute_id",
                principalSchema: "Sale",
                principalTable: "ProductAttribute",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_variant_attribute_product_attribute_attribute_id",
                schema: "Sale",
                table: "ProductVariantAttribute");

            migrationBuilder.AddColumn<int>(
                name: "product_attribute_id",
                schema: "Sale",
                table: "ProductVariantAttribute",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "product_id1",
                schema: "Sale",
                table: "ProductVariant",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2000,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2001,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2002,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2003,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2004,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2005,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2006,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2007,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2008,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2009,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2010,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2011,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2012,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2013,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2014,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2015,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariant",
                keyColumn: "id",
                keyValue: 2016,
                column: "product_id1",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariantAttribute",
                keyColumn: "id",
                keyValue: 4000,
                column: "product_attribute_id",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariantAttribute",
                keyColumn: "id",
                keyValue: 4001,
                column: "product_attribute_id",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariantAttribute",
                keyColumn: "id",
                keyValue: 4002,
                column: "product_attribute_id",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariantAttribute",
                keyColumn: "id",
                keyValue: 4003,
                column: "product_attribute_id",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariantAttribute",
                keyColumn: "id",
                keyValue: 4004,
                column: "product_attribute_id",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariantAttribute",
                keyColumn: "id",
                keyValue: 4005,
                column: "product_attribute_id",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariantAttribute",
                keyColumn: "id",
                keyValue: 4006,
                column: "product_attribute_id",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariantAttribute",
                keyColumn: "id",
                keyValue: 4007,
                column: "product_attribute_id",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariantAttribute",
                keyColumn: "id",
                keyValue: 4008,
                column: "product_attribute_id",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductVariantAttribute",
                keyColumn: "id",
                keyValue: 4009,
                column: "product_attribute_id",
                value: null);

            migrationBuilder.CreateIndex(
                name: "ix_product_variant_attribute_product_attribute_id",
                schema: "Sale",
                table: "ProductVariantAttribute",
                column: "product_attribute_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_variant_product_id1",
                schema: "Sale",
                table: "ProductVariant",
                column: "product_id1");

            migrationBuilder.AddForeignKey(
                name: "fk_product_variant_product_product_id1",
                schema: "Sale",
                table: "ProductVariant",
                column: "product_id1",
                principalSchema: "Sale",
                principalTable: "Product",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_variant_attribute_product_attribute_attribute_id",
                schema: "Sale",
                table: "ProductVariantAttribute",
                column: "attribute_id",
                principalSchema: "Sale",
                principalTable: "ProductAttribute",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_product_variant_attribute_product_attribute_product_attribute_",
                schema: "Sale",
                table: "ProductVariantAttribute",
                column: "product_attribute_id",
                principalSchema: "Sale",
                principalTable: "ProductAttribute",
                principalColumn: "id");
        }
    }
}

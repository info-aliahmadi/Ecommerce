using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_product_variant_sku",
                schema: "Sale",
                table: "ProductVariant");

            migrationBuilder.DropIndex(
                name: "ix_product_tag_name",
                schema: "Sale",
                table: "ProductTag");

            migrationBuilder.DropIndex(
                name: "ix_product_product_tag_product_tag_id_product_id",
                schema: "Sale",
                table: "ProductProductTag");

            migrationBuilder.DropIndex(
                name: "ix_product_product_attribute_attribute_id_product_id",
                schema: "Sale",
                table: "ProductProductAttribute");

            migrationBuilder.DropIndex(
                name: "ix_product_manufacturer_manufacturer_id_product_id",
                schema: "Sale",
                table: "ProductManufacturer");

            migrationBuilder.DropIndex(
                name: "ix_product_inventory_variant_id1",
                schema: "Sale",
                table: "ProductInventory");

            migrationBuilder.DropIndex(
                name: "ix_product_category_category_id_product_id",
                schema: "Sale",
                table: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "ix_product_bundle_bundle_id_product_id",
                schema: "Sale",
                table: "ProductBundle");

            migrationBuilder.CreateIndex(
                name: "ix_related_product_product_id1_product_id2",
                schema: "Sale",
                table: "RelatedProduct",
                columns: new[] { "product_id1", "product_id2" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_variant_sku",
                schema: "Sale",
                table: "ProductVariant",
                column: "sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_tag_name",
                schema: "Sale",
                table: "ProductTag",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_product_tag_product_tag_id_product_id",
                schema: "Sale",
                table: "ProductProductTag",
                columns: new[] { "product_tag_id", "product_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_product_attribute_attribute_id_product_id",
                schema: "Sale",
                table: "ProductProductAttribute",
                columns: new[] { "attribute_id", "product_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_manufacturer_manufacturer_id_product_id",
                schema: "Sale",
                table: "ProductManufacturer",
                columns: new[] { "manufacturer_id", "product_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_image_image_id_product_id",
                schema: "Sale",
                table: "ProductImage",
                columns: new[] { "image_id", "product_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_category_category_id_product_id",
                schema: "Sale",
                table: "ProductCategory",
                columns: new[] { "category_id", "product_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_bundle_bundle_id_product_id",
                schema: "Sale",
                table: "ProductBundle",
                columns: new[] { "bundle_id", "product_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_attribute_key",
                schema: "Sale",
                table: "ProductAttribute",
                column: "key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_name",
                schema: "Sale",
                table: "Product",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_sku",
                schema: "Sale",
                table: "Product",
                column: "sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_bundle_name",
                schema: "Sale",
                table: "Bundle",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_related_product_product_id1_product_id2",
                schema: "Sale",
                table: "RelatedProduct");

            migrationBuilder.DropIndex(
                name: "ix_product_variant_sku",
                schema: "Sale",
                table: "ProductVariant");

            migrationBuilder.DropIndex(
                name: "ix_product_tag_name",
                schema: "Sale",
                table: "ProductTag");

            migrationBuilder.DropIndex(
                name: "ix_product_product_tag_product_tag_id_product_id",
                schema: "Sale",
                table: "ProductProductTag");

            migrationBuilder.DropIndex(
                name: "ix_product_product_attribute_attribute_id_product_id",
                schema: "Sale",
                table: "ProductProductAttribute");

            migrationBuilder.DropIndex(
                name: "ix_product_manufacturer_manufacturer_id_product_id",
                schema: "Sale",
                table: "ProductManufacturer");

            migrationBuilder.DropIndex(
                name: "ix_product_image_image_id_product_id",
                schema: "Sale",
                table: "ProductImage");

            migrationBuilder.DropIndex(
                name: "ix_product_category_category_id_product_id",
                schema: "Sale",
                table: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "ix_product_bundle_bundle_id_product_id",
                schema: "Sale",
                table: "ProductBundle");

            migrationBuilder.DropIndex(
                name: "ix_product_attribute_key",
                schema: "Sale",
                table: "ProductAttribute");

            migrationBuilder.DropIndex(
                name: "ix_product_name",
                schema: "Sale",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "ix_product_sku",
                schema: "Sale",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "ix_bundle_name",
                schema: "Sale",
                table: "Bundle");

            migrationBuilder.CreateIndex(
                name: "ix_product_variant_sku",
                schema: "Sale",
                table: "ProductVariant",
                column: "sku");

            migrationBuilder.CreateIndex(
                name: "ix_product_tag_name",
                schema: "Sale",
                table: "ProductTag",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_product_product_tag_product_tag_id_product_id",
                schema: "Sale",
                table: "ProductProductTag",
                columns: new[] { "product_tag_id", "product_id" });

            migrationBuilder.CreateIndex(
                name: "ix_product_product_attribute_attribute_id_product_id",
                schema: "Sale",
                table: "ProductProductAttribute",
                columns: new[] { "attribute_id", "product_id" });

            migrationBuilder.CreateIndex(
                name: "ix_product_manufacturer_manufacturer_id_product_id",
                schema: "Sale",
                table: "ProductManufacturer",
                columns: new[] { "manufacturer_id", "product_id" });

            migrationBuilder.CreateIndex(
                name: "ix_product_inventory_variant_id1",
                schema: "Sale",
                table: "ProductInventory",
                column: "variant_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_category_category_id_product_id",
                schema: "Sale",
                table: "ProductCategory",
                columns: new[] { "category_id", "product_id" });

            migrationBuilder.CreateIndex(
                name: "ix_product_bundle_bundle_id_product_id",
                schema: "Sale",
                table: "ProductBundle",
                columns: new[] { "bundle_id", "product_id" });
        }
    }
}

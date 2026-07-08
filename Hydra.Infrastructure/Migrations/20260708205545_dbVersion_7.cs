using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_article_file_upload_preview_image_id",
                schema: "Cms",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "fk_category_file_upload_image_preview_id",
                schema: "Sale",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "fk_link_file_upload_image_preview_id",
                schema: "Cms",
                table: "Link");

            migrationBuilder.DropForeignKey(
                name: "fk_manufacturer_file_upload_image_preview_id",
                schema: "Sale",
                table: "Manufacturer");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ImagePreview",
                schema: "Sale",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "fk_product_attribute_file_upload_image_preview_id",
                schema: "Sale",
                table: "ProductAttribute");

            migrationBuilder.DropForeignKey(
                name: "fk_product_image_file_upload_image_id",
                schema: "Sale",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "fk_slideshow_file_upload_preview_image_id",
                schema: "Cms",
                table: "Slideshow");

            migrationBuilder.AddForeignKey(
                name: "fk_article_file_upload_preview_image_id",
                schema: "Cms",
                table: "Article",
                column: "preview_image_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_category_file_upload_image_preview_id",
                schema: "Sale",
                table: "Category",
                column: "image_preview_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_link_file_upload_image_preview_id",
                schema: "Cms",
                table: "Link",
                column: "image_preview_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_manufacturer_file_upload_image_preview_id",
                schema: "Sale",
                table: "Manufacturer",
                column: "image_preview_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ImagePreview",
                schema: "Sale",
                table: "Product",
                column: "image_preview_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_product_attribute_file_upload_image_preview_id",
                schema: "Sale",
                table: "ProductAttribute",
                column: "image_preview_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_product_image_file_upload_image_id",
                schema: "Sale",
                table: "ProductImage",
                column: "image_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_slideshow_file_upload_preview_image_id",
                schema: "Cms",
                table: "Slideshow",
                column: "preview_image_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_article_file_upload_preview_image_id",
                schema: "Cms",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "fk_category_file_upload_image_preview_id",
                schema: "Sale",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "fk_link_file_upload_image_preview_id",
                schema: "Cms",
                table: "Link");

            migrationBuilder.DropForeignKey(
                name: "fk_manufacturer_file_upload_image_preview_id",
                schema: "Sale",
                table: "Manufacturer");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ImagePreview",
                schema: "Sale",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "fk_product_attribute_file_upload_image_preview_id",
                schema: "Sale",
                table: "ProductAttribute");

            migrationBuilder.DropForeignKey(
                name: "fk_product_image_file_upload_image_id",
                schema: "Sale",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "fk_slideshow_file_upload_preview_image_id",
                schema: "Cms",
                table: "Slideshow");

            migrationBuilder.AddForeignKey(
                name: "fk_article_file_upload_preview_image_id",
                schema: "Cms",
                table: "Article",
                column: "preview_image_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_category_file_upload_image_preview_id",
                schema: "Sale",
                table: "Category",
                column: "image_preview_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_link_file_upload_image_preview_id",
                schema: "Cms",
                table: "Link",
                column: "image_preview_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_manufacturer_file_upload_image_preview_id",
                schema: "Sale",
                table: "Manufacturer",
                column: "image_preview_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ImagePreview",
                schema: "Sale",
                table: "Product",
                column: "image_preview_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_product_attribute_file_upload_image_preview_id",
                schema: "Sale",
                table: "ProductAttribute",
                column: "image_preview_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_image_file_upload_image_id",
                schema: "Sale",
                table: "ProductImage",
                column: "image_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_slideshow_file_upload_preview_image_id",
                schema: "Cms",
                table: "Slideshow",
                column: "preview_image_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id");
        }
    }
}

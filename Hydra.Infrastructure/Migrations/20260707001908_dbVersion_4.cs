using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                schema: "Sale",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "sku",
                schema: "Sale",
                table: "Product",
                type: "character varying(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1000,
                column: "sku",
                value: "BOOK-1000");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1001,
                column: "sku",
                value: "ELECTRONIK-1001");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1002,
                column: "sku",
                value: "ELECTRONIK-1002");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1003,
                column: "sku",
                value: "ELECTRONIK-1003");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1004,
                column: "sku",
                value: "FASHION-1004");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1005,
                column: "sku",
                value: "FASION-1005");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1006,
                column: "sku",
                value: "HOME-1006");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1007,
                column: "sku",
                value: "HOME-1007");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1008,
                column: "sku",
                value: "HOME-1008");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1009,
                column: "sku",
                value: "SPORT-1009");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1010,
                column: "sku",
                value: "BEAUTY-1010");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1011,
                column: "sku",
                value: "BOOK-1011");

            migrationBuilder.CreateIndex(
                name: "ix_slideshow_preview_image_id",
                schema: "Cms",
                table: "Slideshow",
                column: "preview_image_id");

            migrationBuilder.CreateIndex(
                name: "ix_link_image_preview_id",
                schema: "Cms",
                table: "Link",
                column: "image_preview_id");

            migrationBuilder.AddForeignKey(
                name: "fk_link_file_upload_image_preview_id",
                schema: "Cms",
                table: "Link",
                column: "image_preview_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_slideshow_file_upload_preview_image_id",
                schema: "Cms",
                table: "Slideshow",
                column: "preview_image_id",
                principalSchema: "FS",
                principalTable: "FileUpload",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_link_file_upload_image_preview_id",
                schema: "Cms",
                table: "Link");

            migrationBuilder.DropForeignKey(
                name: "fk_slideshow_file_upload_preview_image_id",
                schema: "Cms",
                table: "Slideshow");

            migrationBuilder.DropIndex(
                name: "ix_slideshow_preview_image_id",
                schema: "Cms",
                table: "Slideshow");

            migrationBuilder.DropIndex(
                name: "ix_link_image_preview_id",
                schema: "Cms",
                table: "Link");

            migrationBuilder.DropColumn(
                name: "sku",
                schema: "Sale",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "code",
                schema: "Sale",
                table: "Product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1000,
                column: "code",
                value: 1000);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1001,
                column: "code",
                value: 1001);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1002,
                column: "code",
                value: 1002);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1003,
                column: "code",
                value: 1003);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1004,
                column: "code",
                value: 1004);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1005,
                column: "code",
                value: 1005);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1006,
                column: "code",
                value: 1006);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1007,
                column: "code",
                value: 1007);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1008,
                column: "code",
                value: 1008);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1009,
                column: "code",
                value: 1009);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1010,
                column: "code",
                value: 1010);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1011,
                column: "code",
                value: 1011);
        }
    }
}

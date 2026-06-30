using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "picture_preview_id",
                schema: "Sale",
                table: "Product",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_picture_preview_id",
                schema: "Sale",
                table: "Product",
                column: "picture_preview_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_PicturePreview",
                schema: "Sale",
                table: "Product",
                column: "picture_preview_id",
                principalSchema: "Sale",
                principalTable: "ProductPicture",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_PicturePreview",
                schema: "Sale",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "ix_product_picture_preview_id",
                schema: "Sale",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "picture_preview_id",
                schema: "Sale",
                table: "Product");
        }
    }
}

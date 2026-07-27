using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItem_ProductVariant",
                schema: "Sale",
                table: "ShoppingCartItem");

            migrationBuilder.DropIndex(
                name: "ix_shopping_cart_item_product_id",
                schema: "Sale",
                table: "ShoppingCartItem");

            migrationBuilder.DropColumn(
                name: "product_id",
                schema: "Sale",
                table: "ShoppingCartItem");

            migrationBuilder.CreateIndex(
                name: "ix_shopping_cart_item_product_variant_id",
                schema: "Sale",
                table: "ShoppingCartItem",
                column: "product_variant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItem_ProductVariant",
                schema: "Sale",
                table: "ShoppingCartItem",
                column: "product_variant_id",
                principalSchema: "Sale",
                principalTable: "ProductVariant",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItem_ProductVariant",
                schema: "Sale",
                table: "ShoppingCartItem");

            migrationBuilder.DropIndex(
                name: "ix_shopping_cart_item_product_variant_id",
                schema: "Sale",
                table: "ShoppingCartItem");

            migrationBuilder.AddColumn<int>(
                name: "product_id",
                schema: "Sale",
                table: "ShoppingCartItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_shopping_cart_item_product_id",
                schema: "Sale",
                table: "ShoppingCartItem",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItem_ProductVariant",
                schema: "Sale",
                table: "ShoppingCartItem",
                column: "product_id",
                principalSchema: "Sale",
                principalTable: "ProductVariant",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

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
            migrationBuilder.DropForeignKey(
                name: "fk_address_user_user_id",
                schema: "Sale",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_ProductId_Product_Id",
                schema: "Sale",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReview_Product",
                schema: "Sale",
                table: "ProductReview");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReview_User",
                schema: "Sale",
                table: "ProductReview");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviewHelpfulness_ProductReview",
                schema: "Sale",
                table: "ProductReviewHelpfulness");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviewHelpfulness_User",
                schema: "Sale",
                table: "ProductReviewHelpfulness");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItem_Product",
                schema: "Sale",
                table: "ShoppingCartItem");

            migrationBuilder.RenameColumn(
                name: "product_id",
                schema: "Sale",
                table: "OrderItem",
                newName: "product_variant_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_product_id",
                schema: "Sale",
                table: "OrderItem",
                newName: "ix_order_item_product_variant_id");

            migrationBuilder.AddColumn<int>(
                name: "product_variant_id",
                schema: "Sale",
                table: "ShoppingCartItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "payment_status_id",
                schema: "Sale",
                table: "Order",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AddForeignKey(
                name: "fk_address_user_user_id",
                schema: "Sale",
                table: "Address",
                column: "user_id",
                principalSchema: "Auth",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_ProductId_Product_Id",
                schema: "Sale",
                table: "OrderItem",
                column: "product_variant_id",
                principalSchema: "Sale",
                principalTable: "ProductVariant",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReview_Product",
                schema: "Sale",
                table: "ProductReview",
                column: "product_id",
                principalSchema: "Sale",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReview_User",
                schema: "Sale",
                table: "ProductReview",
                column: "user_id",
                principalSchema: "Auth",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviewHelpfulness_ProductReview",
                schema: "Sale",
                table: "ProductReviewHelpfulness",
                column: "product_review_id",
                principalSchema: "Sale",
                principalTable: "ProductReview",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviewHelpfulness_User",
                schema: "Sale",
                table: "ProductReviewHelpfulness",
                column: "user_id",
                principalSchema: "Auth",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_address_user_user_id",
                schema: "Sale",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_ProductId_Product_Id",
                schema: "Sale",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReview_Product",
                schema: "Sale",
                table: "ProductReview");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReview_User",
                schema: "Sale",
                table: "ProductReview");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviewHelpfulness_ProductReview",
                schema: "Sale",
                table: "ProductReviewHelpfulness");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviewHelpfulness_User",
                schema: "Sale",
                table: "ProductReviewHelpfulness");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItem_ProductVariant",
                schema: "Sale",
                table: "ShoppingCartItem");

            migrationBuilder.DropColumn(
                name: "product_variant_id",
                schema: "Sale",
                table: "ShoppingCartItem");

            migrationBuilder.RenameColumn(
                name: "product_variant_id",
                schema: "Sale",
                table: "OrderItem",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_product_variant_id",
                schema: "Sale",
                table: "OrderItem",
                newName: "ix_order_item_product_id");

            migrationBuilder.AlterColumn<byte>(
                name: "payment_status_id",
                schema: "Sale",
                table: "Order",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_address_user_user_id",
                schema: "Sale",
                table: "Address",
                column: "user_id",
                principalSchema: "Auth",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_ProductId_Product_Id",
                schema: "Sale",
                table: "OrderItem",
                column: "product_id",
                principalSchema: "Sale",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReview_Product",
                schema: "Sale",
                table: "ProductReview",
                column: "product_id",
                principalSchema: "Sale",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReview_User",
                schema: "Sale",
                table: "ProductReview",
                column: "user_id",
                principalSchema: "Auth",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviewHelpfulness_ProductReview",
                schema: "Sale",
                table: "ProductReviewHelpfulness",
                column: "product_review_id",
                principalSchema: "Sale",
                principalTable: "ProductReview",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviewHelpfulness_User",
                schema: "Sale",
                table: "ProductReviewHelpfulness",
                column: "user_id",
                principalSchema: "Auth",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItem_Product",
                schema: "Sale",
                table: "ShoppingCartItem",
                column: "product_id",
                principalSchema: "Sale",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

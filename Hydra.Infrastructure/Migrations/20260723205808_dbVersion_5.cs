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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_address_user_user_id",
                schema: "Sale",
                table: "Address");

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
        }
    }
}

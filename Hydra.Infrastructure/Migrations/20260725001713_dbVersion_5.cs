using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                name: "FK_Order_ShippingMethod",
                schema: "Sale",
                table: "Order");

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

            migrationBuilder.DropTable(
                name: "ShippingMethod",
                schema: "Sale");

            migrationBuilder.DropIndex(
                name: "ix_order_shipping_method_id",
                schema: "Sale",
                table: "Order");

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "id",
                keyValue: 5025);

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

            migrationBuilder.Sql(@"UPDATE ""Auth"".""User"" SET ""default_theme"" = CASE ""default_theme"" WHEN 'Light' THEN '0' WHEN 'Dark' THEN '1' WHEN 'System' THEN '2' ELSE NULL END WHERE ""default_theme"" IS NOT NULL");
            migrationBuilder.AlterColumn<byte>(
                name: "default_theme",
                schema: "Auth",
                table: "User",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.Sql(@"UPDATE ""Auth"".""User"" SET ""default_language"" = CASE ""default_language"" WHEN 'Arabic' THEN '1' WHEN 'Persian' THEN '2' WHEN 'English' THEN '3' ELSE NULL END WHERE ""default_language"" IS NOT NULL");
            migrationBuilder.AlterColumn<byte>(
                name: "default_language",
                schema: "Auth",
                table: "User",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(6)",
                oldMaxLength: 6,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "product_variant_id",
                schema: "Sale",
                table: "ShoppingCartItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "email",
                schema: "Sale",
                table: "Shipment",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                schema: "Sale",
                table: "Shipment",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "recipient_name",
                schema: "Sale",
                table: "Shipment",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "shipping_address_snapshot",
                schema: "Sale",
                table: "Shipment",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "shipping_status_id",
                schema: "Sale",
                table: "Order",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AlterColumn<byte>(
                name: "shipping_method_id",
                schema: "Sale",
                table: "Order",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "payment_status_id",
                schema: "Sale",
                table: "Order",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "order_status_id",
                schema: "Sale",
                table: "Order",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AddColumn<string>(
                name: "address_snapshot",
                schema: "Sale",
                table: "Order",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "geo_location",
                schema: "Sale",
                table: "Address",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "county",
                schema: "Sale",
                table: "Address",
                type: "character varying(70)",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(70)",
                oldMaxLength: 70);

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "id",
                keyValue: 5020,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "SALE.COUNTRY_MANAGEMENT", "SALE.COUNTRY_MANAGEMENT" });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "id",
                keyValue: 5021,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "SALE.STATE_PROVINCE_MANAGEMENT", "SALE.STATE_PROVINCE_MANAGEMENT" });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "id",
                keyValue: 5022,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "SALE.ADDRESS_MANAGEMENT", "SALE.ADDRESS_MANAGEMENT" });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "id",
                keyValue: 5023,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "SALE.TAX_MANAGEMENT", "SALE.TAX_MANAGEMENT" });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "id",
                keyValue: 5024,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "SALE.BUNDLE_MANAGEMENT", "SALE.BUNDLE_MANAGEMENT" });

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

            migrationBuilder.DropColumn(
                name: "email",
                schema: "Sale",
                table: "Shipment");

            migrationBuilder.DropColumn(
                name: "phone_number",
                schema: "Sale",
                table: "Shipment");

            migrationBuilder.DropColumn(
                name: "recipient_name",
                schema: "Sale",
                table: "Shipment");

            migrationBuilder.DropColumn(
                name: "shipping_address_snapshot",
                schema: "Sale",
                table: "Shipment");

            migrationBuilder.DropColumn(
                name: "address_snapshot",
                schema: "Sale",
                table: "Order");

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

            migrationBuilder.AlterColumn<string>(
                name: "default_theme",
                schema: "Auth",
                table: "User",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "default_language",
                schema: "Auth",
                table: "User",
                type: "character varying(6)",
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "shipping_status_id",
                schema: "Sale",
                table: "Order",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "shipping_method_id",
                schema: "Sale",
                table: "Order",
                type: "integer",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "payment_status_id",
                schema: "Sale",
                table: "Order",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<byte>(
                name: "order_status_id",
                schema: "Sale",
                table: "Order",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "geo_location",
                schema: "Sale",
                table: "Address",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "county",
                schema: "Sale",
                table: "Address",
                type: "character varying(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(70)",
                oldMaxLength: 70,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ShippingMethod",
                schema: "Sale",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shipping_method", x => x.id);
                });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "id",
                keyValue: 5020,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "SALE.SHIPMENT_METHOD_MANAGEMENT", "SALE.SHIPMENT_METHOD_MANAGEMENT" });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "id",
                keyValue: 5021,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "SALE.COUNTRY_MANAGEMENT", "SALE.COUNTRY_MANAGEMENT" });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "id",
                keyValue: 5022,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "SALE.STATE_PROVINCE_MANAGEMENT", "SALE.STATE_PROVINCE_MANAGEMENT" });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "id",
                keyValue: 5023,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "SALE.ADDRESS_MANAGEMENT", "SALE.ADDRESS_MANAGEMENT" });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "id",
                keyValue: 5024,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "SALE.TAX_MANAGEMENT", "SALE.TAX_MANAGEMENT" });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "id", "name", "normalized_name" },
                values: new object[] { 5025, "SALE.BUNDLE_MANAGEMENT", "SALE.BUNDLE_MANAGEMENT" });

            migrationBuilder.InsertData(
                schema: "Sale",
                table: "ShippingMethod",
                columns: new[] { "id", "description", "display_order", "name" },
                values: new object[,]
                {
                    { 1, "Shipping by land transport", 1, "Ground" },
                    { 2, "The one day air shipping", 2, "Next Day Air" },
                    { 3, "The two day air shipping", 3, "2nd Day Air" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_order_shipping_method_id",
                schema: "Sale",
                table: "Order",
                column: "shipping_method_id");

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
                name: "FK_Order_ShippingMethod",
                schema: "Sale",
                table: "Order",
                column: "shipping_method_id",
                principalSchema: "Sale",
                principalTable: "ShippingMethod",
                principalColumn: "id");

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

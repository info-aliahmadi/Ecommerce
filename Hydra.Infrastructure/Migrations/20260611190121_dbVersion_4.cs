using System;
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
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Country",
                schema: "Sale",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_StateProvince",
                schema: "Sale",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_User",
                schema: "Sale",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category",
                schema: "Sale",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventory_Attribute",
                schema: "Sale",
                table: "ProductInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventory_Product",
                schema: "Sale",
                table: "ProductInventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductWarehouseInventory",
                schema: "Sale",
                table: "ProductInventory");

            migrationBuilder.DropColumn(
                name: "stock_type",
                schema: "Sale",
                table: "ProductInventory");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_datetime",
                schema: "Sale",
                table: "ProductInventory",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "start_datetime",
                schema: "Sale",
                table: "ProductInventory",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "code",
                schema: "Sale",
                table: "Product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "stock_type",
                schema: "Sale",
                table: "Product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "pk_product_inventory",
                schema: "Sale",
                table: "ProductInventory",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_address_country_country_id",
                schema: "Sale",
                table: "Address",
                column: "country_id",
                principalSchema: "Sale",
                principalTable: "Country",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_address_state_province_state_province_id",
                schema: "Sale",
                table: "Address",
                column: "state_province_id",
                principalSchema: "Sale",
                principalTable: "StateProvince",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

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
                name: "fk_category_category_parent_category_id",
                schema: "Sale",
                table: "Category",
                column: "parent_category_id",
                principalSchema: "Sale",
                principalTable: "Category",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_inventory_product_attribute_attribute_id",
                schema: "Sale",
                table: "ProductInventory",
                column: "attribute_id",
                principalSchema: "Sale",
                principalTable: "ProductAttribute",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_product_inventory_product_product_id",
                schema: "Sale",
                table: "ProductInventory",
                column: "product_id",
                principalSchema: "Sale",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_address_country_country_id",
                schema: "Sale",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "fk_address_state_province_state_province_id",
                schema: "Sale",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "fk_address_user_user_id",
                schema: "Sale",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "fk_category_category_parent_category_id",
                schema: "Sale",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "fk_product_inventory_product_attribute_attribute_id",
                schema: "Sale",
                table: "ProductInventory");

            migrationBuilder.DropForeignKey(
                name: "fk_product_inventory_product_product_id",
                schema: "Sale",
                table: "ProductInventory");

            migrationBuilder.DropPrimaryKey(
                name: "pk_product_inventory",
                schema: "Sale",
                table: "ProductInventory");

            migrationBuilder.DropColumn(
                name: "created_datetime",
                schema: "Sale",
                table: "ProductInventory");

            migrationBuilder.DropColumn(
                name: "start_datetime",
                schema: "Sale",
                table: "ProductInventory");

            migrationBuilder.DropColumn(
                name: "code",
                schema: "Sale",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "stock_type",
                schema: "Sale",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "stock_type",
                schema: "Sale",
                table: "ProductInventory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductWarehouseInventory",
                schema: "Sale",
                table: "ProductInventory",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Country",
                schema: "Sale",
                table: "Address",
                column: "country_id",
                principalSchema: "Sale",
                principalTable: "Country",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_StateProvince",
                schema: "Sale",
                table: "Address",
                column: "state_province_id",
                principalSchema: "Sale",
                principalTable: "StateProvince",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_User",
                schema: "Sale",
                table: "Address",
                column: "user_id",
                principalSchema: "Auth",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category",
                schema: "Sale",
                table: "Category",
                column: "parent_category_id",
                principalSchema: "Sale",
                principalTable: "Category",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventory_Attribute",
                schema: "Sale",
                table: "ProductInventory",
                column: "attribute_id",
                principalSchema: "Sale",
                principalTable: "ProductAttribute",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventory_Product",
                schema: "Sale",
                table: "ProductInventory",
                column: "product_id",
                principalSchema: "Sale",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

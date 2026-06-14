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
            migrationBuilder.RenameColumn(
                name: "price",
                schema: "Sale",
                table: "Product",
                newName: "sell_unit_price");

            migrationBuilder.RenameColumn(
                name: "old_price",
                schema: "Sale",
                table: "Product",
                newName: "old_sell_unit_price");

            migrationBuilder.AlterColumn<decimal>(
                name: "stock_quantity",
                schema: "Sale",
                table: "ProductInventory",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "reserved_quantity",
                schema: "Sale",
                table: "ProductInventory",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<decimal>(
                name: "buy_unit_price",
                schema: "Sale",
                table: "ProductInventory",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "measure_type",
                schema: "Sale",
                table: "Product",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "buy_unit_price",
                schema: "Sale",
                table: "ProductInventory");

            migrationBuilder.DropColumn(
                name: "measure_type",
                schema: "Sale",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "sell_unit_price",
                schema: "Sale",
                table: "Product",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "old_sell_unit_price",
                schema: "Sale",
                table: "Product",
                newName: "old_price");

            migrationBuilder.AlterColumn<int>(
                name: "stock_quantity",
                schema: "Sale",
                table: "ProductInventory",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "reserved_quantity",
                schema: "Sale",
                table: "ProductInventory",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}

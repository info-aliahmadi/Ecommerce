using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxRate_StateProvince",
                schema: "Sale",
                table: "TaxRate");

            migrationBuilder.DropIndex(
                name: "ix_tax_rate_state_province_id",
                schema: "Sale",
                table: "TaxRate");

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "TaxCategory",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "state_province_id",
                schema: "Sale",
                table: "TaxRate");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Country",
                keyColumn: "id",
                keyValue: 100,
                columns: new[] { "name", "numeric_iso_code", "three_letter_iso_code", "two_letter_iso_code" },
                values: new object[] { "Iran", 364, "IRN", "IR" });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Country",
                keyColumn: "id",
                keyValue: 106,
                columns: new[] { "name", "numeric_iso_code", "three_letter_iso_code", "two_letter_iso_code" },
                values: new object[] { "Honduras", 340, "HND", "HN" });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "id",
                keyValue: 5016,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "SALE.BASE_INFORMATION_MANAGEMENT", "SALE.BASE_INFORMATION_MANAGEMENT" });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "TaxCategory",
                keyColumn: "id",
                keyValue: 1,
                column: "name",
                value: "0% Tax");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "TaxCategory",
                keyColumn: "id",
                keyValue: 2,
                column: "name",
                value: "2% Tax");

            migrationBuilder.InsertData(
                schema: "Sale",
                table: "TaxCategory",
                columns: new[] { "id", "display_order", "name" },
                values: new object[,]
                {
                    { 5, 3, "5% Tax" },
                    { 9, 4, "9% Tax" },
                    { 20, 5, "20% Tax" }
                });

            migrationBuilder.InsertData(
                schema: "Sale",
                table: "TaxRate",
                columns: new[] { "id", "country_id", "percentage", "tax_category_id" },
                values: new object[,]
                {
                    { 1, 100, 0m, 1 },
                    { 2, 100, 2m, 2 },
                    { 3, 100, 5m, 5 },
                    { 4, 100, 9m, 9 },
                    { 5, 100, 20m, 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "TaxRate",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "TaxRate",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "TaxRate",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "TaxRate",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "TaxRate",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "TaxCategory",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "TaxCategory",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "TaxCategory",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.AddColumn<int>(
                name: "state_province_id",
                schema: "Sale",
                table: "TaxRate",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Country",
                keyColumn: "id",
                keyValue: 100,
                columns: new[] { "name", "numeric_iso_code", "three_letter_iso_code", "two_letter_iso_code" },
                values: new object[] { "Honduras", 340, "HND", "HN" });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Country",
                keyColumn: "id",
                keyValue: 106,
                columns: new[] { "name", "numeric_iso_code", "three_letter_iso_code", "two_letter_iso_code" },
                values: new object[] { "Iran (Islamic Republic of)", 364, "IRN", "IR" });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "id",
                keyValue: 5016,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "SALE.CURRENCY_MANAGEMENT", "SALE.CURRENCY_MANAGEMENT" });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "TaxCategory",
                keyColumn: "id",
                keyValue: 1,
                column: "name",
                value: "5% Tax");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "TaxCategory",
                keyColumn: "id",
                keyValue: 2,
                column: "name",
                value: "9% Tax");

            migrationBuilder.InsertData(
                schema: "Sale",
                table: "TaxCategory",
                columns: new[] { "id", "display_order", "name" },
                values: new object[] { 3, 3, "20% Tax" });

            migrationBuilder.CreateIndex(
                name: "ix_tax_rate_state_province_id",
                schema: "Sale",
                table: "TaxRate",
                column: "state_province_id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxRate_StateProvince",
                schema: "Sale",
                table: "TaxRate",
                column: "state_province_id",
                principalSchema: "Sale",
                principalTable: "StateProvince",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

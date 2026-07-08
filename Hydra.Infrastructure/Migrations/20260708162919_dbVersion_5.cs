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
            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "ProductTag",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductTag",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "Popular", "popular" });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductTag",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "Sale", "sale" });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductTag",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "Sustainable", "sustainable" });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductTag",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "Trending", "trending" });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductTag",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "Featured", "Featured" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductTag",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "New", "new" });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductTag",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "Popular", "popular" });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductTag",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "Sale", "sale" });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductTag",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "Sustainable", "sustainable" });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "ProductTag",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "name", "normalized_name" },
                values: new object[] { "Trending", "trending" });

            migrationBuilder.InsertData(
                schema: "Sale",
                table: "ProductTag",
                columns: new[] { "id", "name", "normalized_name" },
                values: new object[] { 7, "Featured", "Featured" });
        }
    }
}

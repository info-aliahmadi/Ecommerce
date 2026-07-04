using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "color",
                schema: "Cms",
                table: "Menu",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "color",
                schema: "Sale",
                table: "Category",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 3,
                column: "color",
                value: "#6A5ACD");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 4,
                column: "color",
                value: "#E63946");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 5,
                column: "color",
                value: "#20B2AA");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 6,
                column: "color",
                value: "#FFC107");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 7,
                column: "color",
                value: "#FF69B4");

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 8,
                column: "color",
                value: "#10B981");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 1,
                column: "color",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 2,
                column: "color",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 3,
                column: "color",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 4,
                column: "color",
                value: null);

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 22,
                column: "color",
                value: "#6A5ACD");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 23,
                column: "color",
                value: "#E63946");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 24,
                column: "color",
                value: "#20B2AA");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 25,
                column: "color",
                value: "#FFC107");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 26,
                column: "color",
                value: "#FF69B4");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 27,
                column: "color",
                value: "#10B981");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "color",
                schema: "Cms",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "color",
                schema: "Sale",
                table: "Category");
        }
    }
}

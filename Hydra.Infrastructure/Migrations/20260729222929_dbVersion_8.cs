using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "not_approved_rating_sum",
                schema: "Sale",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "not_approved_total_reviews",
                schema: "Sale",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "reply_text",
                schema: "Sale",
                table: "ProductReview",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1000,
                column: "approved_total_reviews",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1001,
                column: "approved_total_reviews",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1002,
                column: "approved_total_reviews",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1003,
                column: "approved_total_reviews",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1004,
                column: "approved_total_reviews",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1005,
                column: "approved_total_reviews",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1006,
                column: "approved_total_reviews",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1007,
                column: "approved_total_reviews",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1008,
                column: "approved_total_reviews",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1009,
                column: "approved_total_reviews",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1010,
                column: "approved_total_reviews",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1011,
                column: "approved_total_reviews",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "reply_text",
                schema: "Sale",
                table: "ProductReview",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "not_approved_rating_sum",
                schema: "Sale",
                table: "Product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "not_approved_total_reviews",
                schema: "Sale",
                table: "Product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1000,
                columns: new[] { "approved_total_reviews", "not_approved_rating_sum", "not_approved_total_reviews" },
                values: new object[] { 324, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1001,
                columns: new[] { "approved_total_reviews", "not_approved_rating_sum", "not_approved_total_reviews" },
                values: new object[] { 218, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1002,
                columns: new[] { "approved_total_reviews", "not_approved_rating_sum", "not_approved_total_reviews" },
                values: new object[] { 156, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1003,
                columns: new[] { "approved_total_reviews", "not_approved_rating_sum", "not_approved_total_reviews" },
                values: new object[] { 189, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1004,
                columns: new[] { "approved_total_reviews", "not_approved_rating_sum", "not_approved_total_reviews" },
                values: new object[] { 412, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1005,
                columns: new[] { "approved_total_reviews", "not_approved_rating_sum", "not_approved_total_reviews" },
                values: new object[] { 287, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1006,
                columns: new[] { "approved_total_reviews", "not_approved_rating_sum", "not_approved_total_reviews" },
                values: new object[] { 165, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1007,
                columns: new[] { "approved_total_reviews", "not_approved_rating_sum", "not_approved_total_reviews" },
                values: new object[] { 143, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1008,
                columns: new[] { "approved_total_reviews", "not_approved_rating_sum", "not_approved_total_reviews" },
                values: new object[] { 211, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1009,
                columns: new[] { "approved_total_reviews", "not_approved_rating_sum", "not_approved_total_reviews" },
                values: new object[] { 234, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1010,
                columns: new[] { "approved_total_reviews", "not_approved_rating_sum", "not_approved_total_reviews" },
                values: new object[] { 267, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1011,
                columns: new[] { "approved_total_reviews", "not_approved_rating_sum", "not_approved_total_reviews" },
                values: new object[] { 178, 0, 0 });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SubscribeLabel",
                schema: "Cms",
                newName: "SubscribeLabel",
                newSchema: "Crm");

            migrationBuilder.RenameTable(
                name: "Subscribe",
                schema: "Cms",
                newName: "Subscribe",
                newSchema: "Crm");

            migrationBuilder.AlterColumn<string>(
                name: "transaction_tracking_code",
                schema: "Sale",
                table: "Payment",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "payment_tracking_code",
                schema: "Sale",
                table: "Payment",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "masked_credit_card_number",
                schema: "Sale",
                table: "Payment",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "card_type",
                schema: "Sale",
                table: "Payment",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "card_number",
                schema: "Sale",
                table: "Payment",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "card_name",
                schema: "Sale",
                table: "Payment",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "card_expiration_year",
                schema: "Sale",
                table: "Payment",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "card_expiration_month",
                schema: "Sale",
                table: "Payment",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "card_cvv2",
                schema: "Sale",
                table: "Payment",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 12,
                column: "url",
                value: "/products?sort=newest");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 13,
                column: "url",
                value: "/products?sort=popular");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 14,
                column: "url",
                value: "/products?sort=price-asc");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 32,
                column: "url",
                value: "/pages/careers");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 33,
                column: "url",
                value: "/pages/press");

            migrationBuilder.InsertData(
                schema: "Crm",
                table: "SubscribeLabel",
                columns: new[] { "id", "insert_date", "title" },
                values: new object[] { 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), "General" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Crm",
                table: "SubscribeLabel",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "SubscribeLabel",
                schema: "Crm",
                newName: "SubscribeLabel",
                newSchema: "Cms");

            migrationBuilder.RenameTable(
                name: "Subscribe",
                schema: "Crm",
                newName: "Subscribe",
                newSchema: "Cms");

            migrationBuilder.AlterColumn<string>(
                name: "transaction_tracking_code",
                schema: "Sale",
                table: "Payment",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "payment_tracking_code",
                schema: "Sale",
                table: "Payment",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "masked_credit_card_number",
                schema: "Sale",
                table: "Payment",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "card_type",
                schema: "Sale",
                table: "Payment",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "card_number",
                schema: "Sale",
                table: "Payment",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "card_name",
                schema: "Sale",
                table: "Payment",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "card_expiration_year",
                schema: "Sale",
                table: "Payment",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "card_expiration_month",
                schema: "Sale",
                table: "Payment",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "card_cvv2",
                schema: "Sale",
                table: "Payment",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 12,
                column: "url",
                value: "/products?sorting=date-new");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 13,
                column: "url",
                value: "/products?sorting=orders-high");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 14,
                column: "url",
                value: "/products/?sorting=price-lower");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 32,
                column: "url",
                value: "/pages/about");

            migrationBuilder.UpdateData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 33,
                column: "url",
                value: "/pages/about");
        }
    }
}

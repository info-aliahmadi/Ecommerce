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
            migrationBuilder.DropColumn(
                name: "address2",
                schema: "Sale",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "company",
                schema: "Sale",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "email",
                schema: "Sale",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "fax_number",
                schema: "Sale",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "first_name",
                schema: "Sale",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "last_name",
                schema: "Sale",
                table: "Address");

            migrationBuilder.AddColumn<string>(
                name: "geo_location",
                schema: "Sale",
                table: "Address",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_default",
                schema: "Sale",
                table: "Address",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "title",
                schema: "Sale",
                table: "Address",
                type: "character varying(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "geo_location",
                schema: "Sale",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "is_default",
                schema: "Sale",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "title",
                schema: "Sale",
                table: "Address");

            migrationBuilder.AddColumn<string>(
                name: "address2",
                schema: "Sale",
                table: "Address",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "company",
                schema: "Sale",
                table: "Address",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email",
                schema: "Sale",
                table: "Address",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "fax_number",
                schema: "Sale",
                table: "Address",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                schema: "Sale",
                table: "Address",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                schema: "Sale",
                table: "Address",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_featured",
                schema: "Sale",
                table: "ProductAttribute",
                newName: "show_on_homepage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "show_on_homepage",
                schema: "Sale",
                table: "ProductAttribute",
                newName: "is_featured");
        }
    }
}

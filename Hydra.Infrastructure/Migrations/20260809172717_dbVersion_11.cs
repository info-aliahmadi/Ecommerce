using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Cms",
                table: "LinkSection",
                columns: new[] { "id", "is_visible", "key", "title" },
                values: new object[,]
                {
                    { 4, true, "promobar", "Promo Bar" },
                    { 5, true, "dealticker", "Deal Ticker" }
                });

            migrationBuilder.InsertData(
                schema: "Cms",
                table: "Link",
                columns: new[] { "id", "description", "image_preview_id", "link_section_id", "order", "title", "url", "user_id" },
                values: new object[,]
                {
                    { 40, null, null, 4, 5, "✨ New Arrivals Just Dropped!", "#", null },
                    { 41, null, null, 4, 5, "🔥 Summer Sale — Up to 60% OFF!", "#", null },
                    { 42, null, null, 4, 5, "🎁 Use code WELCOME15 — 15% off!", "#", null },
                    { 43, null, null, 4, 5, "🎁 Use code WELCOME15 for 15% off your first order", "#", null },
                    { 51, null, null, 5, 5, "🔥 Flash Sale: Up to 60% OFF Electronics", "#", null },
                    { 52, null, null, 5, 5, "🚚 Free Shipping on Orders Over $50", "#", null },
                    { 53, null, null, 5, 5, "🎁 Use Code WELCOME15 for 15% Off", "#", null },
                    { 54, null, null, 5, 5, "⚡ New Arrivals Just Dropped — Shop Now", "#", null },
                    { 55, null, null, 5, 5, "💎 Premium Collection — Exclusive Deals", "#", null },
                    { 56, null, null, 5, 5, "🔄 Easy 30-Day Returns on All Orders", "#", null },
                    { 57, null, null, 5, 5, "⭐ 50K+ Happy Customers Worldwide", "#", null },
                    { 58, null, null, 5, 5, "🔒 100% Secure Checkout — SSL Encrypted", "#", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "LinkSection",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "LinkSection",
                keyColumn: "id",
                keyValue: 5);
        }
    }
}

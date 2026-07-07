using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedProduct_Product",
                schema: "Sale",
                table: "RelatedProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedProduct_Product1",
                schema: "Sale",
                table: "RelatedProduct");

            migrationBuilder.DropPrimaryKey(
                name: "pk_product_product_tag",
                schema: "Sale",
                table: "ProductProductTag");

            migrationBuilder.DropIndex(
                name: "ix_product_product_tag_product_tag_id",
                schema: "Sale",
                table: "ProductProductTag");

            migrationBuilder.AddColumn<int>(
                name: "id",
                schema: "Sale",
                table: "ProductProductTag",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_product_product_tag",
                schema: "Sale",
                table: "ProductProductTag",
                column: "id");

            migrationBuilder.InsertData(
                schema: "Sale",
                table: "Product",
                columns: new[] { "id", "admin_comment", "allow_customer_reviews", "allowed_quantities", "approved_rating_sum", "approved_total_reviews", "available_end_date_time_utc", "available_for_pre_order", "available_start_date_time_utc", "call_for_price", "code", "create_user_id", "created_on_utc", "currency_type", "deleted", "delivery_date_type", "disable_buy_button", "disable_wishlist_button", "display_order", "display_stock_quantity", "full_description", "has_discounts_applied", "image_preview_id", "is_free_shipping", "is_tax_exempt", "mark_as_new", "mark_as_new_end_date_time_utc", "mark_as_new_start_date_time_utc", "measure_type", "meta_description", "meta_keywords", "meta_title", "min_stock_quantity", "name", "not_approved_rating_sum", "not_approved_total_reviews", "not_returnable", "notify_admin_for_quantity_below", "old_sell_unit_price", "order_maximum_quantity", "order_minimum_quantity", "published", "sell_unit_price", "short_description", "show_on_homepage", "stock_quantity", "stock_type", "tax_category_id", "update_user_id", "updated_on_utc" },
                values: new object[,]
                {
                    { 1000, null, true, false, 0, 324, null, false, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, 1000, 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), 4, false, 2, false, false, 1, false, "Premium over-ear headphones with active noise cancellation, 30-hour battery life, and crystal-clear audio quality. Perfect for music lovers and professionals.", false, null, false, false, false, null, null, 2, "ANC Headphones • 30hr Battery", "bestseller,trending", "Wireless Noise-Cancelling Headphones", 0, "Wireless Noise-Cancelling Headphones", 0, 0, false, false, 349.99m, 1000, 1, true, 249.99m, "ANC Headphones • 30hr Battery", true, 45m, 0, 1, null, null },
                    { 1001, null, true, false, 0, 218, null, false, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, 1001, 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), 4, false, 2, false, false, 2, false, "Advanced smartwatch with health monitoring, GPS tracking, and a stunning AMOLED display. Water resistant to 50m.", false, null, false, false, false, null, null, 2, "AMOLED Display • GPS • Health", "new,popular", "Smart Watch Pro", 0, "Smart Watch Pro", 0, 0, false, false, 399.99m, 1000, 1, true, 299.99m, "AMOLED Display • GPS • Health", true, 32m, 0, 1, null, null },
                    { 1002, null, true, false, 0, 156, null, false, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, 1002, 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), 4, false, 2, false, false, 3, false, "Compact, waterproof speaker with 360-degree sound. 12-hour battery life and built-in microphone.", false, null, false, false, false, null, null, 2, "Waterproof • 360° Sound", "sale", "Portable Bluetooth Speaker", 0, "Portable Bluetooth Speaker", 0, 0, false, false, 119.99m, 1000, 1, true, 79.99m, "Waterproof • 360° Sound", false, 78m, 0, 1, null, null },
                    { 1003, null, true, false, 0, 189, null, false, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, 1003, 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), 4, false, 2, false, false, 4, false, "Full-size mechanical keyboard with customizable RGB lighting, hot-swappable switches, and premium build quality.", false, null, false, false, false, null, null, 2, "Hot-Swap • RGB • Aluminum", "popular", "Mechanical Keyboard RGB", 0, "Mechanical Keyboard RGB", 0, 0, false, false, 189.99m, 1000, 1, true, 149.99m, "Hot-Swap • RGB • Aluminum", false, 23m, 0, 1, null, null },
                    { 1004, null, true, false, 0, 412, null, false, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, 1004, 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), 4, false, 2, false, false, 5, false, "Timeless genuine leather jacket with premium lining. A wardrobe essential that pairs with everything.", false, null, false, false, true, null, null, 2, "Genuine Leather • Slim Fit", "bestseller,trending", "Classic Leather Jacket", 0, "Classic Leather Jacket", 0, 0, false, false, 279.99m, 1000, 1, true, 189.99m, "Genuine Leather • Slim Fit", true, 15m, 0, 1, null, null },
                    { 1005, null, true, false, 0, 287, null, false, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, 1005, 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), 4, false, 2, false, false, 6, false, "Ultra-soft 100% organic cotton t-shirt. Comfortable, breathable, and sustainably made.", false, null, false, false, false, null, null, 2, "Organic Cotton • Relaxed Fit", "sale,sustainable", "Premium Cotton T-Shirt", 0, "Premium Cotton T-Shirt", 0, 0, false, false, 59.99m, 1000, 1, true, 39.99m, "Organic Cotton • Relaxed Fit", false, 120m, 0, 1, null, null },
                    { 1006, null, true, false, 0, 165, null, false, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, 1006, 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), 4, false, 2, false, false, 7, false, "UV400 protection with polarized lenses. Lightweight titanium frame for all-day comfort.", false, null, false, false, true, null, null, 2, "Polarized • UV400 • Titanium", "popular,new", "Designer Sunglasses", 0, "Designer Sunglasses", 0, 0, false, false, 179.99m, 1000, 1, true, 129.99m, "Polarized • UV400 • Titanium", true, 56m, 0, 1, null, null },
                    { 1007, null, true, false, 0, 143, null, false, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, 1007, 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), 4, false, 2, false, false, 8, false, "LED desk lamp with adjustable color temperature, touch controls, and wireless charging base.", false, null, false, false, true, null, null, 2, "LED • Touch Control • Wireless Charging", "trending", "Minimalist Desk Lamp", 0, "Minimalist Desk Lamp", 0, 0, false, false, 129.99m, 1000, 1, true, 89.99m, "LED • Touch Control • Wireless Charging", true, 34m, 0, 1, null, null },
                    { 1008, null, true, false, 0, 211, null, false, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, 1008, 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), 4, false, 2, false, false, 9, false, "Set of 3 handcrafted ceramic plant pots in matte finish. Perfect for succulents and small plants.", false, null, false, false, false, null, null, 2, "Set of 3 • Matte Finish • Handmade", "popular,new", "Ceramic Plant Pot Set", 0, "Ceramic Plant Pot Set", 0, 0, false, false, 64.99m, 1000, 1, true, 44.99m, "Set of 3 • Matte Finish • Handmade", false, 67m, 0, 1, null, null },
                    { 1009, null, true, false, 0, 234, null, false, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, 1009, 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), 4, false, 2, false, false, 10, false, "Extra thick non-slip yoga mat with alignment guides. Eco-friendly TPE material.", false, null, false, false, false, null, null, 2, "6mm Thick • Non-Slip • Eco-TPE", "bestseller", "Yoga Mat Premium", 0, "Yoga Mat Premium", 0, 0, false, false, 69.99m, 1000, 1, true, 49.99m, "6mm Thick • Non-Slip • Eco-TPE", true, 45m, 0, 1, null, null },
                    { 1010, null, true, false, 0, 267, null, false, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, 1010, 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), 4, false, 2, false, false, 11, false, "Complete skincare routine with cleanser, toner, serum, and moisturizer. For all skin types.", false, null, false, false, true, null, null, 2, "4-Piece Set • All Skin Types", "bestseller,new", "Skincare Essential Kit", 0, "Skincare Essential Kit", 0, 0, false, false, 129.99m, 1000, 1, true, 89.99m, "4-Piece Set • All Skin Types", true, 38m, 0, 1, null, null },
                    { 1011, null, true, false, 0, 178, null, false, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, 1011, 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), 4, false, 2, false, false, 12, false, "A comprehensive guide to interior design, minimalism, and creating spaces that inspire.", false, null, false, false, false, null, null, 2, "Hardcover • 320 Pages", "bestseller", "The Art of Modern Living", 0, "The Art of Modern Living", 0, 0, false, false, 49.99m, 1000, 1, true, 34.99m, "Hardcover • 320 Pages", false, 65m, 0, 1, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_product_tag_product_id",
                schema: "Sale",
                table: "ProductProductTag",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_product_tag_product_tag_id_product_id",
                schema: "Sale",
                table: "ProductProductTag",
                columns: new[] { "product_tag_id", "product_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedProduct1Navigation",
                schema: "Sale",
                table: "RelatedProduct",
                column: "product_id1",
                principalSchema: "Sale",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedProduct2Navigation",
                schema: "Sale",
                table: "RelatedProduct",
                column: "product_id2",
                principalSchema: "Sale",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedProduct1Navigation",
                schema: "Sale",
                table: "RelatedProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedProduct2Navigation",
                schema: "Sale",
                table: "RelatedProduct");

            migrationBuilder.DropPrimaryKey(
                name: "pk_product_product_tag",
                schema: "Sale",
                table: "ProductProductTag");

            migrationBuilder.DropIndex(
                name: "ix_product_product_tag_product_id",
                schema: "Sale",
                table: "ProductProductTag");

            migrationBuilder.DropIndex(
                name: "ix_product_product_tag_product_tag_id_product_id",
                schema: "Sale",
                table: "ProductProductTag");

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1000);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Product",
                keyColumn: "id",
                keyValue: 1011);

            migrationBuilder.DropColumn(
                name: "id",
                schema: "Sale",
                table: "ProductProductTag");

            migrationBuilder.AddPrimaryKey(
                name: "pk_product_product_tag",
                schema: "Sale",
                table: "ProductProductTag",
                columns: new[] { "product_id", "product_tag_id" });

            migrationBuilder.CreateIndex(
                name: "ix_product_product_tag_product_tag_id",
                schema: "Sale",
                table: "ProductProductTag",
                column: "product_tag_id");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedProduct_Product",
                schema: "Sale",
                table: "RelatedProduct",
                column: "product_id1",
                principalSchema: "Sale",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedProduct_Product1",
                schema: "Sale",
                table: "RelatedProduct",
                column: "product_id2",
                principalSchema: "Sale",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

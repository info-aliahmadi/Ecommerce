using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Manufacturer",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Manufacturer",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.InsertData(
                schema: "Sale",
                table: "Category",
                columns: new[] { "id", "created_on_utc", "deleted", "description", "display_order", "meta_description", "meta_keywords", "meta_title", "name", "parent_category_id", "picture_id", "published", "show_on_homepage", "updated_on_utc" },
                values: new object[,]
                {
                    { 3, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Electronic products category", 3, "Electronic devices and accessories", "electronics, devices, gadgets", "Electronics", "Electronics", null, null, true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Home and kitchen appliances", 8, "Appliances for home use", "home, appliances, kitchen", "Home Appliances", "Home Appliances", null, null, true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Furniture for home and office", 14, "Home and office furniture", "furniture, sofa, table", "Furniture", "Furniture", null, null, true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Outdoor and garden supplies", 16, "Garden tools and outdoor equipment", "garden, outdoor, tools", "Outdoor & Garden", "Outdoor & Garden", null, null, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                schema: "Cms",
                table: "Link",
                columns: new[] { "id", "description", "image_preview_id", "link_section_id", "order", "title", "url", "user_id" },
                values: new object[,]
                {
                    { 1, "Browse all product categories", null, 1, 1, "All Categories", "/categories", 1 },
                    { 2, "Read our latest blog posts", null, 2, 1, "Latest Posts", "/blog", 1 },
                    { 3, "Check current deals and offers", null, 1, 2, "Deals & Offers", "/deals", 1 },
                    { 4, "Recommended reads", null, 2, 2, "Editor Picks", "/editor-picks", 1 }
                });

            migrationBuilder.InsertData(
                schema: "Cms",
                table: "LinkSection",
                columns: new[] { "id", "is_visible", "key", "title" },
                values: new object[] { 3, true, "Footer", "Footer" });

            migrationBuilder.InsertData(
                schema: "Sale",
                table: "Manufacturer",
                columns: new[] { "id", "created_on_utc", "deleted", "description", "display_order", "meta_description", "meta_keywords", "meta_title", "name", "picture_id", "published", "updated_on_utc" },
                values: new object[,]
                {
                    { 3, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Samsung Electronics", 3, "Samsung electronics", "samsung, electronics", "Samsung", "Samsung", null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Apple Inc.", 4, "Apple products", "apple, iphone, mac", "Apple", "Apple", null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "LG Electronics", 5, "LG Electronics", "lg, electronics", "LG", "LG", null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Sony Corporation", 6, "Sony electronics", "sony, audio, tv", "Sony", "Sony", null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Bosch Home Appliances", 7, "Bosch home appliances", "bosch, appliances", "Bosch", "Bosch", null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "IKEA", 8, "IKEA furniture", "ikea, furniture", "Ikea", "Ikea", null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                schema: "Cms",
                table: "Menu",
                columns: new[] { "id", "order", "parent_id", "preview_image_id", "title", "url", "user_id" },
                values: new object[] { 6, -1, null, null, "Home", "/", 1 });

            migrationBuilder.InsertData(
                schema: "Sale",
                table: "ProductAttribute",
                columns: new[] { "id", "attribute_type", "description", "display_order", "name", "picture_id", "value" },
                values: new object[,]
                {
                    { 8, 0, null, 8, "Green", null, "green" },
                    { 9, 0, null, 9, "Yellow", null, "yellow" },
                    { 10, 0, null, 10, "Purple", null, "purple" },
                    { 11, 1, "Extra Small size", 11, "Extra Small", null, "XS" },
                    { 12, 1, "Extra Large size", 12, "Extra Large", null, "XL" }
                });

            migrationBuilder.InsertData(
                schema: "Cms",
                table: "Topic",
                columns: new[] { "id", "parent_id", "register_date", "title", "user_id" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), "General", 1 },
                    { 2, null, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Announcements", 1 },
                    { 3, null, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Guides", 1 },
                    { 4, null, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), "How To", 1 },
                    { 5, null, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), "News", 1 },
                    { 6, null, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Product", 1 }
                });

            migrationBuilder.InsertData(
                schema: "Sale",
                table: "Category",
                columns: new[] { "id", "created_on_utc", "deleted", "description", "display_order", "meta_description", "meta_keywords", "meta_title", "name", "parent_category_id", "picture_id", "published", "show_on_homepage", "updated_on_utc" },
                values: new object[,]
                {
                    { 4, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Computers and related products", 4, "Desktops, components and accessories", "computers, desktops, components", "Computers", "Computers", 3, null, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Mobile phones and accessories", 6, "Smartphones and mobile devices", "phones, smartphones, mobiles", "Mobile Phones", "Mobile Phones", 3, null, true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Accessories for electronic devices", 7, "Electronics accessories", "accessories, chargers, cases", "Accessories", "Accessories", 3, null, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Kitchen related appliances", 9, "Kitchen appliances and tools", "kitchen, appliances, cookware", "Kitchen", "Kitchen", 8, null, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Televisions, projectors and video accessories", 10, "Televisions and video equipment", "tv, video, televisions", "TV & Video", "TV & Video", 3, null, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Audio equipment and accessories", 11, "Speakers, headphones and audio devices", "audio, speakers, headphones", "Audio", "Audio", 3, null, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Wearable devices and accessories", 12, "Smartwatches and fitness trackers", "wearables, smartwatches, trackers", "Wearables", "Wearables", 3, null, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Small home and kitchen appliances", 13, "Small kitchen and home appliances", "small appliances, blenders, toasters", "Small Appliances", "Small Appliances", 8, null, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Living room furniture", 15, "Sofas, coffee tables and more", "living room, sofa, coffee table", "Living Room", "Living Room", 14, null, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Laptops and portable computers", 5, "Portable computers and notebooks", "laptops, notebooks, ultrabooks", "Laptops", "Laptops", 4, null, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Link",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "LinkSection",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Manufacturer",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Manufacturer",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Manufacturer",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Manufacturer",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Manufacturer",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Manufacturer",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Menu",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "ProductAttribute",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "ProductAttribute",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "ProductAttribute",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "ProductAttribute",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "ProductAttribute",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Topic",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Topic",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Topic",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Topic",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Topic",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Cms",
                table: "Topic",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "Sale",
                table: "Category",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.InsertData(
                schema: "Sale",
                table: "Category",
                columns: new[] { "id", "created_on_utc", "deleted", "description", "display_order", "meta_description", "meta_keywords", "meta_title", "name", "parent_category_id", "picture_id", "published", "show_on_homepage", "updated_on_utc" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Description of Category 1", 1, "MetaDescription", "MetaKeywords", "Title", "Category 1", null, null, true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Description of Category 2", 2, "MetaDescription", "MetaKeywords", "Title", "Category 2", null, null, true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                schema: "Sale",
                table: "Manufacturer",
                columns: new[] { "id", "created_on_utc", "deleted", "description", "display_order", "meta_description", "meta_keywords", "meta_title", "name", "picture_id", "published", "updated_on_utc" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Description of Category 1", 1, "MetaDescription", "MetaKeywords", "Title", "Manufacturer 1", null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), false, "Description of Category 2", 2, "MetaDescription", "MetaKeywords", "Title", "Manufacturer 2", null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }
    }
}

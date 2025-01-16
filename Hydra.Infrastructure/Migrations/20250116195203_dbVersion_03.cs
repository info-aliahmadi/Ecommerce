using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hydra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbVersion_03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1001, "AUTH.CHANGE_PASSWORD", "AUTH.CHANGE_PASSWORD" },
                    { 1002, "AUTH.GET_USER_LIST", "AUTH.GET_USER_LIST" },
                    { 1003, "AUTH.GET_USER_BY_ID", "AUTH.GET_USER_BY_ID" },
                    { 1004, "AUTH.ADD_USER", "AUTH.ADD_USER" },
                    { 1005, "AUTH.UPDATE_USER", "AUTH.UPDATE_USER" },
                    { 1006, "AUTH.DELETE_USER", "AUTH.DELETE_USER" },
                    { 1007, "AUTH.ASSIGN_PERMISSION_TO_ROLE_BY_ROLE_ID", "AUTH.ASSIGN_PERMISSION_TO_ROLE_BY_ROLE_ID" },
                    { 1008, "AUTH.GET_ROLE_LIST", "AUTH.GET_ROLE_LIST" },
                    { 1009, "AUTH.GET_ROLE_BY_ID", "AUTH.GET_ROLE_BY_ID" },
                    { 1010, "AUTH.ADD_ROLE", "AUTH.ADD_ROLE" },
                    { 1011, "AUTH.UPDATE_ROLE", "AUTH.UPDATE_ROLE" },
                    { 1012, "AUTH.DELETE_ROLE", "AUTH.DELETE_ROLE" },
                    { 1013, "AUTH.GET_PERMISSION_LIST", "AUTH.GET_PERMISSION_LIST" },
                    { 1014, "AUTH.GET_PERMISSION_BY_ID", "AUTH.GET_PERMISSION_BY_ID" },
                    { 1015, "AUTH.ADD_PERMISSION", "AUTH.ADD_PERMISSION" },
                    { 1016, "AUTH.UPDATE_PERMISSION", "AUTH.UPDATE_PERMISSION" },
                    { 1017, "AUTH.DELETE_PERMISSION", "AUTH.DELETE_PERMISSION" },
                    { 2001, "CMS.ADD_OR_UPDATE_SETTINGS", "CMS.ADD_OR_UPDATE_SETTINGS" },
                    { 2002, "CMS.GET_TOPIC_LIST", "CMS.GET_TOPIC_LIST" },
                    { 2003, "CMS.GET_TOPIC_BY_ID", "CMS.GET_TOPIC_BY_ID" },
                    { 2004, "CMS.ADD_TOPIC", "CMS.ADD_TOPIC" },
                    { 2005, "CMS.UPDATE_TOPIC", "CMS.UPDATE_TOPIC" },
                    { 2006, "CMS.DELETE_TOPIC", "CMS.DELETE_TOPIC" },
                    { 2007, "CMS.GET_TAG_LIST", "CMS.GET_TAG_LIST" },
                    { 2008, "CMS.GET_TAG_BY_ID", "CMS.GET_TAG_BY_ID" },
                    { 2009, "CMS.ADD_TAG", "CMS.ADD_TAG" },
                    { 2010, "CMS.UPDATE_TAG", "CMS.UPDATE_TAG" },
                    { 2011, "CMS.DELETE_TAG", "CMS.DELETE_TAG" },
                    { 2012, "CMS.GET_ARTICLE_LIST", "CMS.GET_ARTICLE_LIST" },
                    { 2013, "CMS.GET_TRASH_ARTICLE_LIST", "CMS.GET_TRASH_ARTICLE_LIST" },
                    { 2014, "CMS.GET_ARTICLE_BY_ID", "CMS.GET_ARTICLE_BY_ID" },
                    { 2015, "CMS.CMS_PINNED_ARTICLE", "CMS.CMS_PINNED_ARTICLE" },
                    { 2016, "CMS.ADD_ARTICLE", "CMS.ADD_ARTICLE" },
                    { 2017, "CMS.UPDATE_ARTICLE", "CMS.UPDATE_ARTICLE" },
                    { 2018, "CMS.DELETE_ARTICLE", "CMS.DELETE_ARTICLE" },
                    { 2019, "CMS.RESTORE_ARTICLE", "CMS.RESTORE_ARTICLE" },
                    { 2020, "CMS.REMOVE_ARTICLE", "CMS.REMOVE_ARTICLE" },
                    { 2021, "CMS.GET_PAGE_LIST", "CMS.GET_PAGE_LIST" },
                    { 2022, "CMS.GET_PAGE_BY_ID", "CMS.GET_PAGE_BY_ID" },
                    { 2023, "CMS.ADD_PAGE", "CMS.ADD_PAGE" },
                    { 2024, "CMS.UPDATE_PAGE", "CMS.UPDATE_PAGE" },
                    { 2025, "CMS.DELETE_PAGE", "CMS.DELETE_PAGE" },
                    { 2026, "CMS.GET_MENU_LIST", "CMS.GET_MENU_LIST" },
                    { 2027, "CMS.GET_MENU_BY_ID", "CMS.GET_MENU_BY_ID" },
                    { 2028, "CMS.ADD_MENU", "CMS.ADD_MENU" },
                    { 2029, "CMS.UPDATE_MENU", "CMS.UPDATE_MENU" },
                    { 2030, "CMS.DELETE_MENU", "CMS.DELETE_MENU" },
                    { 2031, "CMS.GET_SLIDESHOW_LIST", "CMS.GET_SLIDESHOW_LIST" },
                    { 2032, "CMS.GET_SLIDESHOW_BY_ID", "CMS.GET_SLIDESHOW_BY_ID" },
                    { 2033, "CMS.ADD_SLIDESHOW", "CMS.ADD_SLIDESHOW" },
                    { 2034, "CMS.UPDATE_SLIDESHOW", "CMS.UPDATE_SLIDESHOW" },
                    { 2035, "CMS.VISIBLE_SLIDESHOW", "CMS.VISIBLE_SLIDESHOW" },
                    { 2036, "CMS.DELETE_SLIDESHOW", "CMS.DELETE_SLIDESHOW" },
                    { 3001, "CRM.GET_SETTINGS", "CRM.GET_SETTINGS" },
                    { 3002, "CRM.ADD_OR_UPDATE_SETTINGS", "CRM.ADD_OR_UPDATE_SETTINGS" },
                    { 3003, "CRM.SEND_PUBLIC_MESSAGE", "CRM.SEND_PUBLIC_MESSAGE" },
                    { 3004, "CRM.SEND_PRIVATE_MESSAGE", "CRM.SEND_PRIVATE_MESSAGE" },
                    { 3005, "CRM.SAVE_DRAFT_MESSAGE", "CRM.SAVE_DRAFT_MESSAGE" },
                    { 3006, "CRM.GET_ALLMESSAGES", "CRM.GET_ALLMESSAGES" },
                    { 3007, "CRM.GET_INBOX_MESSAGES", "CRM.GET_INBOX_MESSAGES" },
                    { 3008, "CRM.GET_SENT_MESSAGES", "CRM.GET_SENT_MESSAGES" },
                    { 3009, "CRM.GET_DRAFT_MESSAGES", "CRM.GET_DRAFT_MESSAGES" },
                    { 3010, "CRM.GET_PUBLIC_INBOX_MESSAGES", "CRM.GET_PUBLIC_INBOX_MESSAGES" },
                    { 3011, "CRM.GET_DELETED_INBOX_MESSAGES", "CRM.GET_DELETED_INBOX_MESSAGES" },
                    { 3012, "CRM.GET_DELETED_SENT_MESSAGES", "CRM.GET_DELETED_SENT_MESSAGES" },
                    { 3013, "CRM.GET_MESSAGE_BY_ID_FOR_PUBLIC", "CRM.GET_MESSAGE_BY_ID_FOR_PUBLIC" },
                    { 3014, "CRM.GET_MESSAGE_BY_ID_FOR_SENDER", "CRM.GET_MESSAGE_BY_ID_FOR_SENDER" },
                    { 3015, "CRM.GET_MESSAGE_BY_ID_FOR_RECEIVER", "CRM.GET_MESSAGE_BY_ID_FOR_RECEIVER" },
                    { 3016, "CRM.DELETE_MESSAGE", "CRM.DELETE_MESSAGE" },
                    { 3017, "CRM.RESTORE_MESSAGE", "CRM.RESTORE_MESSAGE" },
                    { 3018, "CRM.PIN_MESSAGE", "CRM.PIN_MESSAGE" },
                    { 3019, "CRM.READ_MESSAGE", "CRM.READ_MESSAGE" },
                    { 3020, "CRM.DELETE_DRAFT_MESSAGE", "CRM.DELETE_DRAFT_MESSAGE" },
                    { 3021, "CRM.REMOVE_DRAFT_MESSAGE", "CRM.REMOVE_DRAFT_MESSAGE" },
                    { 3022, "CRM.LOAD_EMAIL_INBOX", "CRM.LOAD_EMAIL_INBOX" },
                    { 3023, "CRM.GET_ALL_EMAIL_INBOX", "CRM.GET_ALL_EMAIL_INBOX" },
                    { 3024, "CRM.GET_INBOX_EMAIL_INBOX", "CRM.GET_INBOX_EMAIL_INBOX" },
                    { 3025, "CRM.GET_DELETED_EMAIL_INBOX", "CRM.GET_DELETED_EMAIL_INBOX" },
                    { 3026, "CRM.GET_EMAIL_INBOX_BY_ID", "CRM.GET_EMAIL_INBOX_BY_ID" },
                    { 3027, "CRM.GET_EMAIL_INBOX_BY_ID_FOR_RECEIVER", "CRM.GET_EMAIL_INBOX_BY_ID_FOR_RECEIVER" },
                    { 3028, "CRM.DELETE_EMAIL_INBOX", "CRM.DELETE_EMAIL_INBOX" },
                    { 3029, "CRM.PIN_EMAIL_INBOX", "CRM.PIN_EMAIL_INBOX" },
                    { 3030, "CRM.READ_EMAIL_INBOX", "CRM.READ_EMAIL_INBOX" },
                    { 3031, "CRM.REMOVE_EMAIL_INBOX", "CRM.REMOVE_EMAIL_INBOX" },
                    { 3032, "CRM.SEND_EMAIL_OUTBOX", "CRM.SEND_EMAIL_OUTBOX" },
                    { 3033, "CRM.SAVE_DRAFT_EMAIL_OUTBOX", "CRM.SAVE_DRAFT_EMAIL_OUTBOX" },
                    { 3034, "CRM.GET_ALL_EMAIL_OUTBOX", "CRM.GET_ALL_EMAIL_OUTBOX" },
                    { 3035, "CRM.GET_EMAIL_OUTBOX", "CRM.GET_EMAIL_OUTBOX" },
                    { 3036, "CRM.GET_ADDRESS_FOR_SELECT", "CRM.GET_ADDRESS_FOR_SELECT" },
                    { 3037, "CRM.GET_EMAIL_OUTBOX_BY_ID_FOR_SENDER", "CRM.GET_EMAIL_OUTBOX_BY_ID_FOR_SENDER" },
                    { 3038, "CRM.REMOVE_EMAIL_OUTBOX", "CRM.REMOVE_EMAIL_OUTBOX" }
                });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "SUPERADMIN");

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "ADMIN");

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "USER");

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "SUPERVISER");

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "GUEST");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1016);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2001);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2002);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2003);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2004);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2005);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2006);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2007);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2008);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2009);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2010);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2011);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2012);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2013);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2014);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2015);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2016);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2017);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2018);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2019);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2020);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2021);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2022);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2023);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2024);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2025);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2026);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2027);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2028);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2029);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2030);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2031);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2032);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2033);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2034);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2035);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2036);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3001);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3002);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3003);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3004);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3005);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3006);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3007);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3008);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3009);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3010);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3011);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3012);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3013);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3014);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3015);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3016);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3017);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3018);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3019);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3020);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3021);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3022);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3023);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3024);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3025);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3026);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3027);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3028);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3029);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3030);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3031);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3032);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3033);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3034);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3035);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3036);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3037);

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Permission",
                keyColumn: "Id",
                keyValue: 3038);

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permission",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "AUTH.CHANGE_PASSWORD", "AUTH.CHANGE_PASSWORD" },
                    { 2, "AUTH.GET_USER_LIST", "AUTH.GET_USER_LIST" },
                    { 3, "AUTH.GET_USER_BY_ID", "AUTH.GET_USER_BY_ID" },
                    { 4, "AUTH.ADD_USER", "AUTH.ADD_USER" },
                    { 5, "AUTH.UPDATE_USER", "AUTH.UPDATE_USER" },
                    { 6, "AUTH.DELETE_USER", "AUTH.DELETE_USER" },
                    { 7, "AUTH.ASSIGN_PERMISSION_TO_ROLE_BY_ROLE_ID", "AUTH.ASSIGN_PERMISSION_TO_ROLE_BY_ROLE_ID" },
                    { 8, "AUTH.GET_ROLE_LIST", "AUTH.GET_ROLE_LIST" },
                    { 9, "AUTH.GET_ROLE_BY_ID", "AUTH.GET_ROLE_BY_ID" },
                    { 10, "AUTH.ADD_ROLE", "AUTH.ADD_ROLE" },
                    { 11, "AUTH.UPDATE_ROLE", "AUTH.UPDATE_ROLE" },
                    { 12, "AUTH.DELETE_ROLE", "AUTH.DELETE_ROLE" },
                    { 13, "AUTH.GET_PERMISSION_LIST", "AUTH.GET_PERMISSION_LIST" },
                    { 14, "AUTH.GET_PERMISSION_BY_ID", "AUTH.GET_PERMISSION_BY_ID" },
                    { 15, "AUTH.ADD_PERMISSION", "AUTH.ADD_PERMISSION" },
                    { 16, "AUTH.UPDATE_PERMISSION", "AUTH.UPDATE_PERMISSION" },
                    { 17, "AUTH.DELETE_PERMISSION", "AUTH.DELETE_PERMISSION" }
                });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "SuperAdmin");

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Admin");

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "User");

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Superviser");

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Guest");
        }
    }
}
